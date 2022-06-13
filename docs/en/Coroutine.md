# Coroutine
[`Coroutine`](../../src/AwaitableCoroutine/AwaitableCoroutine.cs) 
is a coroutine that is both awaitable and can be created using the async method.

The generic version, `Coroutine<T>`, returns the result value.

**Table of contents**
- [Coroutine](#coroutine)
  - [Method](#method)
  - [Extension method](#extension-method)
  - [Exception and cancellation](#exception-and-cancellation)
    - [CanceledException](#canceledexception)
    - [例外](#例外)
  - [Modules](#modules)
    - [Yield](#yield)
    - [While](#while)
    - [DelayCount](#delaycount)
    - [WaitAll](#waitall)
    - [WaitAny](#waitany)
    - [Select, SelectTo](#select-selectto)
    - [AndThen](#andthen)
    - [UntilCompleted](#untilcompleted)
    - [FromEnumerator](#fromenumerator)
    - [AwaitTask](#awaittask)
    - [AwaitObservable](#awaitobservable)
    - [AwaitObservableCompleted](#awaitobservablecompleted)

## Method

| Name | Desc
| --- | --- | `IsCompletedSuccessfully
| `IsCompletedSuccessfully` | Get whether the coroutine is completed successfully or not |
| `IsCanceled` | Get whether the coroutine is canceled or not |
| `Cancel()` | Cancel the coroutine |

## Extension method

| Name | Desc |
| --- | --- |
| `OnCompleted (Action)` | Register the process to be executed when the coroutine is completed |
| `OnUpdating (Action)` | Register the process to be executed before updating the coroutine |
| `OnCanceled (Action)` | Register the process to be executed when the coroutine is canceled |

## Exception and cancellation

### CanceledException

`CanceledException` is an exception to use when you want to cancel the execution of a coroutine created by an asynchronous method from within that method.

Use the `Cancel` method if you want to cancel the coroutine in any other case.

| Name | Desc |
| --- | --- |
| [`CanceledException`](../../src/AwaitableCoroutine/CalceledException.cs) | Exception for canceling a coroutine |
| `ChildCanceledException` | Exception for canceling a coroutine due to the cancellation of a waiting coroutine |
| `ChildrenCanceledException` | Exception for canceling a coroutine due to multiple waiting coroutines being canceled |


### 例外

Exceptions thrown by a coroutine are propagated to the awaiting coroutine. 
However, it will be wrapped by `ChildCanceledException`.


```csharp
var co = runner.Create(async () => {
    try
    {
      await Hoge(); // throw exception
    }
    catch(ChildCanceledException e)
    {
      var hogeException = e.InnerException;
      // do something
    }
    finally
    {
      // do something
    }
});
```

## Modules

### Yield
[`Yield`](../../src/AwaitableCoroutine/Internal/YieldAwaitable.cs.cs) 
method creates a struct just for `await` once in the `async` method.

Call it as a static method, as below.

```csharp
await Coroutine.Yield();
```

It cannot be used in the same way as `Coroutine` class, because it is implemented by a struct for optimization purposes.

If you want to `await` only once, use this method. (Using `DelayCount(0)` cause extra overhead).

Example

```csharp
private async Coroutine CreateCoroutine()
{
  for (int i = 0; i < 10; i++)
  {
      // do something
      await Coroutine.Yield();
  }
}
```

### While
[`While`](../../src/AwaitableCoroutine/Modules/WhileCoroutine.cs) 
method creates a coroutine that waits while the predicate returns true.

**Argument**
* `predicate`: `Func<bool>`

Call it as a static method as below.

```csharp
AwaitableCoroutine.While(() => flag)
```

### DelayCount
[`DelayCount`](../../src/AwaitableCoroutine/Modules/DelayCountCoroutine.cs) 
method creates a coroutine that waits for the specified number of counts.

**Argument**
* `count`: `int`
* * `Action<int> onUpdating = null`

Call it as a static method as below.

```csharp
Coroutine.DelayCount(5)
```

### WaitAll
[`WaitAll`](../../src/AwaitableCoroutine/Modules/WaitAllCoroutine.cs) 
method creats a coroutine that waits until all specified coroutines are completed.

**Arguments**
The tuple version provides an overload that passes 2 to 7 coroutines.
For more than that, you can pass `Span<CoroutineBase>` or `Span<Coroutine<T>>` as arguments.

Call it as a static method as below.

```csharp
AwaitableCoroutine.WaitAll(coroutine1, coroutine2)
```

### WaitAny
[`WaitAny(c1, c2, ...)`](../../src/AwaitableCoroutine/Modules/WaitAnyCoroutine.cs)
method creates a coroutine that waits until one of the specified coroutines is finished.

**Arguments**
The tuple version provides an overload that passes 2 to 7 coroutines.
For more than that, you can pass `Span<CoroutineBase>` or `Span<Coroutine<T>>` as arguments.

Call it as a static method as below.

```csharp
AwaitableCoroutine.WaitAny(coroutine1, coroutine2)
```

### Select, SelectTo

[`Select`, `SelectTo`](../../src/AwaitableCoroutine/Modules/SelectCoroutine.cs) 
methods create a coroutine that converts the resultant value of the original coroutine.

Call them as extension methods as below.

```csharp
var res = await someCoroutine.Select(x => x * x);
```

**Argument**.
* `SelectTo<T>`
  * `coroutine`: `CoroutineBase`
  * `result`: `T`
* `Select<T>`
  * `coroutine`: `Coroutine`
  * `thunk`: `Func<T>`
* `Select<T, U>`
  * `coroutine`: `Coroutine<T>`
  * `thunk`: `Func<T, U>`


### AndThen

[`AndThen`](../../src/AwaitableCoroutine/Modules/AndThenCoroutine.cs) 
method create a coroutine that waits for the coroutine and then creates the continue coroutine.

Call it as an extension method as below.

```csharp
runner.Create(() =>
    someCoroutine.AndThen(async x => {
        await Coroutine.Yield();
        return x * x;
    })
);
```

**Argument**.
* `AwaitableCoroutine.AndThen`
  * `coroutine`: `Coroutine`
  * `thunk`: `Func<Coroutine>`
* `AwaitableCoroutine.AndThen<T>`
  * `coroutine`: `Coroutine`
  * `thunk`: `Func<Coroutine<T>>`
* `Coroutine<T>.AndThen`
  * `coroutine`: `Coroutine<T>`
  * `thunk`: `Func<T, AwaitableCoroutine>`
* `Coroutine<T>.AndThen<U>`
  * `coroutine`: `Coroutine<T>`
  * `thunk`: `Func<T< Coroutine<U>>`

### UntilCompleted

[`UntilCompleted`](../../src/AwaitableCoroutine/Modules/UntilCompletedCoroutine.cs) 
method creates a coroutine that repeats a specific operation while the original coroutine is incomplete.

Call it as an extension method as below.

```csharp
Coroutine.DelayCount(10)
    .UntilCompleted(async () => {
        Console.WriteLine("Hello");
        await Coroutine.DelayCount(3);
    });
```

In this example, a coroutine is created to output the string `Hello` every three counts until the end of the ten counts.

**Arguments**
* `UntilCompleted`（Action版）
  * `CoroutineBase coroutine`
  * `Action action`
* `UntilCompleted`（`Coroutine`を実行する）
  * `CoroutineBase coroutine`
  * `Func<Coroutine> action`
* `UntilCompleted<T>`（`Coroutine`を実行する）
  * `CoroutineBase coroutine`
  * `Func<Coroutine<T>> action`

### FromEnumerator
[`FromEnumerator(IEnumerator)`](../../src/AwaitableCoroutine/Modules/EnumeratorCoroutine.cs) 
method creates a coroutine based on the `IEnumerator`.

**Argument**.
* `enumerator`: `IEnumerator`

### AwaitTask
[`AwaitTask(Task)`](../../src/AwaitableCoroutine/Modules/AwaitTask.cs) 
method creates a coroutine that waits for the `Task` to complete.

**Argument**
* `task`:` Task` or `Task <T>` or `ValueTask` or` ValueTask <T> `

### AwaitObservable
[`AwaitObservable(IObservable <T>)`](../../src/AwaitableCoroutine/Modules/AwaitObservable.cs) 
method creates a coroutine that waits for the next value of `IObservable<T>`.

**Argument**
* `observable`: `IObservable <T>`

### AwaitObservableCompleted
[`AwaitObservableCompleted(IObservable<T>)`](../../src/AwaitableCoroutine/Modules/AwaitObservable.cs) 
method creates a coroutine that waits for the `IObservable <T>` to complete.

**Argument**
* `observable`: `IObservable <T>`
