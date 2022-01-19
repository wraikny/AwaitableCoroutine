# AwaitableCoroutine
[`AwaitableCoroutine`](../../src/AwaitableCoroutine/AwaitableCoroutine.cs) 
is a coroutine that is both awaitable and can be created using the async method.

The generic version, `AwaitableCoroutine<T>`, returns the result value.

**Table of contents**
- [AwaitableCoroutine](#awaitablecoroutine)
  - [Method](#method)
  - [Extension method](#extension-method)
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
    - [CanceledException](#canceledexception)

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

## Modules

### Yield
[`Yield`](../../src/AwaitableCoroutine/Internal/YieldAwaitable.cs.cs) 
method creates a struct just for `await` once in the `async` method.

Call it as a static method, as below.

```csharp
await AwaitableCoroutine.Yield();
```

It cannot be used in the same way as `AwaitableCoroutine` class, because it is implemented by a struct for optimization purposes.

If you want to `await` only once, use this method. (Using `DelayCount(0)` cause extra overhead).

Example

```csharp
private async AwaitableCoroutine CreateCoroutine()
{
  for (int i = 0; i < 10; i++)
  {
      // do something
      await AwaitableCoroutine.Yield();
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
AwaitableCoroutine.DelayCount(5)
```

### WaitAll
[`WaitAll`](../../src/AwaitableCoroutine/Modules/WaitAllCoroutine.cs) 
method creats a coroutine that waits until all specified coroutines are completed.

**Arguments**
The tuple version provides an overload that passes 2 to 7 coroutines.
For more than that, you can pass `Span<AwaitableCoroutineBase>` or `Span<AwaitableCoroutine<T>>` as arguments.

Call it as a static method as below.

```csharp
AwaitableCoroutine.WaitAll(coroutine1, coroutine2)
```

### WaitAny
[`WaitAny(c1, c2, ...)`](../../src/AwaitableCoroutine/Modules/WaitAnyCoroutine.cs)
method creates a coroutine that waits until one of the specified coroutines is finished.

**Arguments**
The tuple version provides an overload that passes 2 to 7 coroutines.
For more than that, you can pass `Span<AwaitableCoroutineBase>` or `Span<AwaitableCoroutine<T>>` as arguments.

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
  * `coroutine`: `AwaitableCoroutineBase`
  * `result`: `T`
* `Select<T>`
  * `coroutine`: `AwaitableCoroutine`
  * `thunk`: `Func<T>`
* `Select<T, U>`
  * `coroutine`: `AwaitableCoroutine<T>`
  * `thunk`: `Func<T, U>`


### AndThen

[`AndThen`](../../src/AwaitableCoroutine/Modules/AndThenCoroutine.cs) 
method create a coroutine that waits for the coroutine and then creates the continue coroutine.

Call it as an extension method as below.

```csharp
runner.Create(() =>
    someCoroutine.AndThen(async x => {
        await AwaitableCoroutine.Yield();
        return x * x;
    })
);
```

**Argument**.
* `AwaitableCoroutine.AndThen`
  * `coroutine`: `AwaitableCoroutine`
  * `thunk`: `Func<AwaitableCoroutine>`
* `AwaitableCoroutine.AndThen<T>`
  * `coroutine`: `AwaitableCoroutine`
  * `thunk`: `Func<AwaitableCoroutine<T>>`
* `AwaitableCoroutine<T>.AndThen`
  * `coroutine`: `AwaitableCoroutine<T>`
  * `thunk`: `Func<T, AwaitableCoroutine>`
* `AwaitableCoroutine<T>.AndThen<U>`
  * `coroutine`: `AwaitableCoroutine<T>`
  * `thunk`: `Func<T< AwaitableCoroutine<U>>`

### UntilCompleted

[`UntilCompleted`](../../src/AwaitableCoroutine/Modules/UntilCompletedCoroutine.cs) 
method creates a coroutine that repeats a specific operation while the original coroutine is incomplete.

Call it as an extension method as below.

```csharp
AwaitableCoroutine.DelayCount(10)
    .UntilCompleted(async () => {
        Console.WriteLine("Hello");
        await AwaitableCoroutine.DelayCount(3);
    });
```

In this example, a coroutine is created to output the string `Hello` every three counts until the end of the ten counts.

**Arguments**
* `UntilCompleted`（Action版）
  * `AwaitableCoroutineBase coroutine`
  * `Action action`
* `UntilCompleted`（`AwaitableCoroutine`を実行する）
  * `AwaitableCoroutineBase coroutine`
  * `Func<AwaitableCoroutine> action`
* `UntilCompleted<T>`（`AwaitableCoroutine`を実行する）
  * `AwaitableCoroutineBase coroutine`
  * `Func<AwaitableCoroutine<T>> action`

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
method creates a coroutine that waits for the `IObservable<T>.MoveNext`.

**Argument**
* `observable`: `IObservable <T>`

### AwaitObservableCompleted
[`AwaitObservableCompleted(IObservable<T>)`](../../src/AwaitableCoroutine/Modules/AwaitObservable.cs)
The method creates a coroutine that waits for the `IObservable <T>` to complete.

**Argument**
* `observable`: `IObservable <T>`

### CanceledException

`CanceledException`は、非同期メソッドによって作成されるコルーチンの実行をそのメソッド内からキャンセルしたいときに利用するための例外です。

Use the `Cancel` method if you want to cancel the coroutine in any other case.

| Name | Desc |
| --- | --- |
| [`CanceledException`](../../src/AwaitableCoroutine/CalceledException.cs) | Exception for canceling a coroutine |
| `ChildCanceledException` | Exception for canceling a coroutine due to the cancellation of a waiting coroutine |
| `ChildrenCanceledException` | Exception for canceling a coroutine due to multiple waiting coroutines being canceled |
