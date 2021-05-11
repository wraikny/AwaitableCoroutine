module FSharpTest.Tests

open System
open Xunit
open Xunit.Abstractions

open AwaitableCoroutine
open AwaitableCoroutine.FSharp

type AwaitTest(outputHelper: ITestOutputHelper) =
  let log = outputHelper.WriteLine

  do
    Internal.Logger.SetLogger(fun (s) -> try log s with _ -> ())

  [<Fact>]
  member __.``Yield Test`` () =
    let runner = CoroutineRunner()

    let counter = new Counter()

    let ac = runner.Context(fun () ->
        awaitableCoroutine {
          counter.Inc() // 1
          do! AwaitableCoroutine.Yield()
          counter.Inc() // 2
        }
    )

    Assert.False(ac.IsCompleted)
    Assert.Equal(0, counter.Count)

    runner.Update() // 1
    Assert.Equal(1, counter.Count)

    runner.Update() // 2
    Assert.True(ac.IsCompleted)
    Assert.Equal(2, counter.Count)

  [<Fact>]
  member __.``Await AwaitableCoroutine Test`` () =
    let runner = CoroutineRunner()

    let counter = new Counter()

    let ac = runner.Context(fun () ->
        awaitableCoroutine {
          counter.Inc()
          do! AwaitableCoroutine.DelayCount(3)
          counter.Inc()
        }
    )

    Assert.False(ac.IsCompleted)
    Assert.Equal(0, counter.Count)

    runner.Update()
    Assert.Equal(1, counter.Count)

    for i in 1..3 do runner.Update()

    runner.Update()

    Assert.True(ac.IsCompleted)
    Assert.Equal(2, counter.Count)

  [<Fact>]
  member __.``ForLoop Test`` () =
    let runner = CoroutineRunner()

    let counter = new Counter()

    let ac = runner.Context(fun () ->
        awaitableCoroutine {
          counter.Inc()
          for i=1 to 3 do
            do! AwaitableCoroutine.Yield()
          counter.Inc()
        }
    )

    Assert.False(ac.IsCompleted)
    Assert.Equal(0, counter.Count)

    runner.Update()
    Assert.Equal(1, counter.Count)

    for i in 1..3 do runner.Update()

    Assert.True(ac.IsCompleted)
    Assert.Equal(2, counter.Count)

  [<Fact>]
  member __.``WhileLoop Test`` () =
    let runner = CoroutineRunner()

    let counter = new Counter()

    let ac = runner.Context(fun () ->
        awaitableCoroutine {
          counter.Inc()
          let mutable i = 0
          while i < 3 do
            do! AwaitableCoroutine.Yield()
            i <- i + 1
          counter.Inc()
        }
    )

    Assert.False(ac.IsCompleted)
    Assert.Equal(0, counter.Count)

    runner.Update()
    Assert.Equal(1, counter.Count)

    for i in 1..3 do runner.Update()

    Assert.True(ac.IsCompleted)
    Assert.Equal(2, counter.Count)

  [<Theory; InlineData(false); InlineData(true)>]
  member __.``If Test`` (cond: bool) =
    let runner = CoroutineRunner()
    let counter = new Counter()
    let ac = runner.Context(fun () ->
      awaitableCoroutine {
        counter.Inc()
        do! AwaitableCoroutine.Yield()
      
        if cond then
          do! AwaitableCoroutine.Yield()

        counter.Inc()
      }
    )

    Assert.False(ac.IsCompleted)
    Assert.Equal(0, counter.Count)

    runner.Update()
    Assert.False(ac.IsCompleted)
    Assert.Equal(1, counter.Count)

    if cond then
      runner.Update()
      Assert.False(ac.IsCompleted)
      Assert.Equal(1, counter.Count)
    
    runner.Update()
    Assert.True(ac.IsCompleted)
    Assert.Equal(2, counter.Count)

  [<Fact>]
  member __.``TryWith Test`` () =
    let runner = CoroutineRunner()

    let counter = new Counter()

    let ac = runner.Context(fun () ->
        awaitableCoroutine {
          counter.Inc()
          do! AwaitableCoroutine.Yield()

          try
            counter.Inc()
            do! AwaitableCoroutine.Yield()
            raise <|System.Exception()
          with _ ->
            counter.Inc()
            do! AwaitableCoroutine.Yield()
          // finally
          //   counter.Inc()
          //   do! AwaitableCoroutine.Yield()

          counter.Inc()
        }
    )

    Assert.False(ac.IsCompleted)

    for i in 0..4 do
      Assert.Equal(i, counter.Count)
      runner.Update()

    Assert.True(ac.IsCompleted)
    Assert.Equal(4, counter.Count)


  [<Fact>]
  member __.``TryFinally Test`` () =
    let runner = CoroutineRunner()

    let counter = new Counter()

    let ac = runner.Context(fun () ->
        awaitableCoroutine {
          counter.Inc()
          do! AwaitableCoroutine.Yield()

          try
            counter.Inc()
            do! AwaitableCoroutine.Yield()
            raise <| Exception()
          finally
            counter.Inc()
        }
    )

    Assert.False(ac.IsCompleted)
    Assert.Equal(0, counter.Count)

    runner.Update()
    Assert.Equal(1, counter.Count)

    runner.Update()
    Assert.Equal(2, counter.Count)

    try
      runner.Update()
    with _ -> ()

    Assert.Equal(3, counter.Count)

    runner.Update()
    Assert.True(ac.IsCompleted)
