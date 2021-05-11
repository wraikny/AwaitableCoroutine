module AwaitableCoroutine.FSharp

open System
open System.Runtime.CompilerServices

open AwaitableCoroutine


// TaskBuilder.fs (cc0)
/// Represents the state of a computation:
/// either awaiting something with a continuation,
/// or completed with a return value.
type Step<'a> =
  | Await of INotifyCompletion * (unit -> Step<'a>)
  | Return of 'a
  /// We model tail calls explicitly, but still can't run them without O(n) memory usage.
  | ReturnFrom of 'a AwaitableCoroutine

module private Helper =
  [<Sealed>]
  type ReturnCoroutine<'a>(x) =
    inherit AwaitableCoroutine<'a>()
    override this.OnMoveNext() = this.Complete(x)

  let fromResult x = ReturnCoroutine(x) :> AwaitableCoroutine<_>

  // Implements the machinery of running a `Step<'m, 'm>` as a awaitablecoroutine returning a continuation awaitablecoroutine.
  [<Sealed>]
  type StepStateMachine<'a>(firstStep: unit -> Step<'a>) as this =
    let methodBuilder = Internal.AwaitableCoroutineMethodBuilder<'a>.Create()
    /// The continuation we left off awaiting on our last MoveNext().
    let mutable continuation = firstStep
    /// Returns next pending awaitable or null if exiting (including tail call).
    let nextAwaitable() =
      try
        match continuation() with
        | Return r ->
          methodBuilder.SetResult (r)
          null
        | ReturnFrom a ->
          methodBuilder.SetResult(a.Result)
          null
        | Await (await, next) ->
          continuation <- next
          await
      with
      | exn ->
        methodBuilder.SetException(exn)
        null

    let mutable self = this

    member __.Run() =
      methodBuilder.Start(&self)
      methodBuilder.Task

    interface IAsyncStateMachine with
      member __.MoveNext() =
        let mutable await = nextAwaitable()
        if not (isNull await) then
          methodBuilder.AwaitOnCompleted(&await, &self)

      member __.SetStateMachine(_) = ()

  /// Used to represent no-ops like the implicit empty "else" branch of an "if" expression.
  let zero = Return ()

  /// Used to return a value.
  let ret (x : 'a) = Return x
  
  let inline private isCompleted (awaiter: ^awt): _ when ^awt :> INotifyCompletion =
    (^awt: (member get_IsCompleted : unit -> bool) awaiter)

  let inline private getResult (awaiter: ^awt): _ when ^awt :> INotifyCompletion =
    (^awt: (member GetResult : unit -> ^inp) awaiter)

  let inline private getAwaiter (awaitable: ^abl): _ =
    (^abl: (member GetAwaiter : unit -> ^ablt) awaitable)


  let inline genericAwait(abl : ^abl, continuation : ^inp -> 'out Step): 'out Step =
    let awt = getAwaiter abl // get an awaiter from the awaitable
    if isCompleted awt then // shortcut to continue immediately
      continuation (getResult awt)
    else
      Await (awt, fun () -> continuation (getResult (getAwaiter abl)))

  /// Chains together a step with its following step.
  /// Note that this requires that the first step has no result.
  /// This prevents constructs like `awaitableCoroutine { return 1; return 2; }`.
  let rec combine (step : Step<unit>) (continuation : unit -> Step<'b>) =
    match step with
    | Return _ -> continuation()
    | ReturnFrom t ->
      Await (t.GetAwaiter() :> INotifyCompletion, continuation)
    | Await (awaitable, next) ->
      Await (awaitable, fun () -> combine (next()) continuation)

  /// Builds a step that executes the body while the condition predicate is true.
  let whileLoop (cond : unit -> bool) (body : unit -> Step<unit>) =
    if cond() then
      // Create a self-referencing closure to test whether to repeat the loop on future iterations.
      let rec repeat () =
        if cond() then
          let body = body()
          match body with
          | Return _ -> repeat()
          | ReturnFrom t -> Await(t.GetAwaiter(), repeat)
          | Await (awaitable, next) ->
              Await (awaitable, fun () -> combine (next()) repeat)
        else zero
      // Run the body the first time and chain it to the repeat logic.
      combine (body()) repeat
    else zero

  /// Wraps a step in a try/with. This catches exceptions both in the evaluation of the function
  /// to retrieve the step, and in the continuation of the step (if any).
  let rec tryWith(step : unit -> Step<'a>) (catch : exn -> Step<'a>) =
    try
      match step() with
      | Return _ as i -> i
      | ReturnFrom t ->
        let awaitable = t.GetAwaiter()
        Await(awaitable, fun () ->
          try
            awaitable.GetResult() |> Return
          with
          | exn -> catch exn)
      | Await (awaitable, next) -> Await (awaitable, fun () -> tryWith next catch)
    with
    | exn -> catch exn

  /// Wraps a step in a try/finally. This catches exceptions both in the evaluation of the function
  /// to retrieve the step, and in the continuation of the step (if any).
  let rec tryFinally (step : unit -> Step<'a>) fin =
    let step =
      try step()
      // Important point: we use a try/with, not a try/finally, to implement tryFinally.
      // The reason for this is that if we're just building a continuation, we definitely *shouldn't*
      // execute the `fin()` part yet -- the actual execution of the asynchronous code hasn't completed!
      with
      | _ ->
        fin()
        reraise()
    match step with
    | Return _ as i ->
      fin()
      i
    | ReturnFrom t ->
      let awaitable = t.GetAwaiter()
      Await(awaitable, fun () ->
        let result =
          try
            awaitable.GetResult() |> Return
          with
          | _ ->
            fin()
            reraise()
        fin() // if we got here we haven't run fin(), because we would've reraised after doing so
        result)
    | Await (awaitable, next) ->
        Await (awaitable, fun () -> tryFinally next fin)

  /// Implements a using statement that disposes `disp` after `body` has completed.
  let using (disp : #IDisposable) (body : _ -> Step<'a>) =
    // A using statement is just a try/finally with the finally block disposing if non-null.
    tryFinally
      (fun () -> body disp)
      (fun () -> if not (isNull (box disp)) then disp.Dispose())

  /// Implements a loop that runs `body` for each element in `sequence`.
  let forLoop (sequence : 'a seq) (body : 'a -> Step<unit>) =
    // A for loop is just a using statement on the sequence's enumerator...
    using (sequence.GetEnumerator())
      // ... and its body is a while loop that advances the enumerator and runs the body on each element.
      (fun e -> whileLoop e.MoveNext (fun () -> body e.Current))

  /// Runs a step as a task -- with a short-circuit for immediately completed steps.
  let run (firstStep : unit -> Step<'a>) =
    StepStateMachine<'a>(firstStep).Run()

open Helper

[<Struct>]
type AwaitableCoroutineBuilder =
  member __.Delay(f : unit -> Step<_>) = f
  member __.Run(f : unit -> Step<'m>) = run f
  member __.Zero() = zero
  member __.Return(x) = ret x
  member __.Combine(step : unit Step, continuation) = combine step continuation
  member __.While(condition : unit -> bool, body : unit -> unit Step) = whileLoop condition body
  member __.For(sequence : _ seq, body : _ -> unit Step) = forLoop sequence body
  member __.TryWith(body : unit -> _ Step, catch : exn -> _ Step) = tryWith body catch
  member __.TryFinally(body : unit -> _ Step, fin : unit -> unit) = tryFinally body fin
  member __.Using(disp : #IDisposable, body : #IDisposable -> _ Step) = using disp body
  member inline __.ReturnFrom a : _ Step = ReturnFrom a
  member inline __.Bind(abl, continuation) = genericAwait (abl, continuation)


let awaitableCoroutine = AwaitableCoroutineBuilder()
