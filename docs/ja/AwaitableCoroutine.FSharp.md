# AwaitableCoroutine.FSharp

F#向けの拡張パッケージです。

**目次**

- [AwaitableCoroutine.FSharp](#awaitablecoroutinefsharp)
  - [コンピューテーション式](#コンピューテーション式)
    - [awaitableCoroutine](#awaitablecoroutine)
    - [ICoroutineRunner.Do](#icoroutinerunnerdo)

## コンピューテーション式

[TaskBuilder.fs](https://github.com/rspeele/TaskBuilder.fs)
を参考にして、最適化されたコンピューテーション式を提供しています。

[CoroutineBuilder.fs](../../src/AwaitableCoroutine.FSharp/CoroutineBuilder.fs)

### awaitableCoroutine

サンプルコード

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

サンプルコード

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