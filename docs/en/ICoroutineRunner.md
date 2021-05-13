# ICoroutineRunner

[`ICoroutineRunner`](../../src/AwaitableCoroutine/ICoroutineRunner.cs)
は、コルーチンを登録・実行するオブジェクトを表すインターフェースです。

基本的には標準の`CoroutineRunner`の利用が推奨されますが、インターフェースを実装することでランナーを自作することもできます。

**Table of contents**
- [ICoroutineRunner](#icoroutinerunner)
  - [Methods and extension methods](#methods-and-extension-methods)
    - [Update](#update)
    - [Create](#create)
    - [Context](#context)
  - [Static method](#static-method)

## Methods and extension methods

### Update
The `Update` extension method moves registered coroutines to the next step. If executed coroutines throw exceptions, the exception will be thrown after all coroutines have been updated.

### Create
Create an `AwaitableCoroutine` or `AwaitableCoroutine<T>` by executing the callback method inside the runner set to the context.

**Arguments**
* `Create`
  * `init`: `Func<AwaitableCoroutine>`
* `Create<T>`
  * `init`: `Func<AwaitableCoroutine<T>>`

```csharp
var co = runner.Create(async () => {
  Console.WriteLine("Hello");
  await AwaitableCoroutine.Yield();
  Console.WriteLine("AwaitableCoroutine!");
});
```

### Context
Execute the callback method with the runner set to the context.

**Arguments**
* Overload 1
  * `action`: `Action`
* Overload 2
  * `init`: `Func<T>`

```csharp
var (co1, co2, waitAll) = runner.Context(() => {
  var co1 = MyCoroutine1();
  var co2 = MyCoroutine2();
  return (co1, co2, AwaitableCoroutine.WaitAll(co1, co2));
});
```


## Static method
| Name | Desc |
| --- | --- |
| `GetContext()` | Get an instance of `ICoroutineRunner` set to the current context |
