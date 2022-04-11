# AwaitableCoroutine.FSharp

Extension package for F#.

**Table of contents**

- [AwaitableCoroutine.FSharp](#awaitablecoroutinefsharp)
  - [Computation Expressions](#computation-expressions)
    - [awaitableCoroutine](#awaitablecoroutine)
    - [ICoroutineRunner.Do](#icoroutinerunnerdo)

## Computation Expressions

The optimized computation expression is provided by referring to [TaskBuilder.fs](https://github.com/rspeele/TaskBuilder.fs).

[CoroutineBuilder.fs](../../src/AwaitableCoroutine.FSharp/CoroutineBuilder.fs)

### awaitableCoroutine

Sample code.

```fsharp
let runner = CoroutineRunner()

let coroutine = runner.Create(fun () ->
  coroutine {
    printfn "Hello"
    do! AwaitableCoroutnie.Yield()
    printfn "Awaitable"
    do! AwaitableCoroutnie.Yield()
    printfn "Coroutine"
  })

runner.Update()
runner.Update()
runner.Update()
```

### ICoroutineRunner.Do

Sample code.

```fsharp
let runner = CoroutineRunner()

let coroutine = runner.Do {
  printfn "Hello"
  do! AwaitableCoroutnie.Yield()
  printfn "Awaitable"
  do! AwaitableCoroutnie.Yield()
  printfn "Coroutine"
}

runner.Update()
runner.Update()
runner.Update()
```
