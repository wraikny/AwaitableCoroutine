# ICoroutineRunner

[`ICoroutineRunner`](../../src/AwaitableCoroutine/ICoroutineRunner.cs) 
is an interface that represents an object that registers and executes coroutines.

Basically, it is recommended to use the standard `CoroutineRunner`, but you can also create your own runner by implementing the interface.

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
Create an `Coroutine` or `Coroutine<T>` by executing the callback method inside the runner set to the context.

**Arguments**
* `Create`
  * `init`: `Func<Coroutine>`
* `Create<T>`
  * `init`: `Func<Coroutine<T>>`

```csharp
var co = runner.Create(async () => {
  Console.WriteLine("Hello");
  await Coroutine.Yield();
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
  return (co1, co2, Coroutine.WaitAll(co1, co2));
});
```


## Static method
| Name | Desc |
| --- | --- |
| `GetContext()` | Get an instance of `ICoroutineRunner` set to the current context |
