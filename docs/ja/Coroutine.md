# AwaitableCoroutine
[`Coroutine`](../../src/AwaitableCoroutine/Coroutine.cs)
クラスは、待機可能かつasyncメソッドを使って作成可能なコルーチンです。

ジェネリック版の`Coroutine<T>`は結果の値を返します。

**目次**

- [AwaitableCoroutine](#awaitablecoroutine)
  - [メソッド](#メソッド)
  - [拡張メソッド](#拡張メソッド)
  - [モジュール](#モジュール)
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

## メソッド

| Name | Desc |
| --- | --- |
| `IsCompleted` | コルーチンが完了したかどうかを取得 |
| `IsCanceled` | コルーチンがキャンセルされたかどうかを取得 |
| `Cancel()` | コルーチンをキャンセル |

## 拡張メソッド

| Name | Desc |
| --- | --- |
| `OnCompleted(Action)` | コルーチンが完了したときに実行される処理を登録 |
| `OnUpdating(Action)` | コルーチン更新直前に実行される処理を登録 |
| `OnCanceled(Action)` | コルーチンがキャンセルされたときに実行される処理を登録 |

## モジュール

### Yield
[`Yield`](../../src/AwaitableCoroutine/Internal/YieldAwaitable.cs.cs)
メソッドは、`async`メソッド内で一度だけ`await`するためだけの構造体を生成します。

以下のように静的メソッドとして呼び出します。

```csharp
await Coroutine.Yield();
```

通常のコルーチンと同じように利用することはできませんが、最適化のために構造体による実装をしています。

一度だけ`await`したい場合はこちらを利用してください。（`DelayCount(0)`などを利用すると余計なオーバーヘッドが生じます)

例

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
メソッドは、指定した条件が真の間待機するコルーチンを生成します。

**引数**
* `predicate`: `Func<bool>`

以下のように静的メソッドとして呼び出します。

```csharp
Coroutine.While(() => true)
```

### DelayCount
[`DelayCount`](../../src/AwaitableCoroutine/Modules/DelayCountCoroutine.cs)
メソッドは、指定したカウントの回数だけ待機するコルーチンを生成します。

**引数**
* `count`: `int`
* `Action<int> onUpdating = null`

以下のように静的メソッドとして呼び出します。

```csharp
Coroutine.DelayCount(5)
```

### WaitAll
[`WaitAll`](../../src/AwaitableCoroutine/Modules/WaitAllCoroutine.cs)
メソッドは、指定したコルーチンが全て終了するまで待機するコルーチンを生成します。

**引数**
タプル版では2〜7個のコルーチンを渡すオーバーロードが用意してあります。
それ以上の場合は`Span<CoroutineBase>`または`Span<Coroutine<T>>`を引数として渡すことができます。

以下のように静的メソッドとして呼び出します。

```csharp
Coroutine.WaitAll(coroutine1, coroutine2)
```

### WaitAny
[`WaitAny(c1, c2, ...)`](../../src/AwaitableCoroutine/Modules/WaitAnyCoroutine.cs)
メソッドは、指定したコルーチンのうちどれか一つが終了するまで待機するコルーチンを生成します。

**引数**
タプル版では2〜7個のコルーチンを渡すオーバーロードが用意してあります。
それ以上の場合は`Span<CoroutineBase>`または`Span<Coroutine<T>>`を引数として渡すことができます。

以下のように静的メソッドとして呼び出します。

```csharp
Coroutine.WaitAny(coroutine1, coroutine2)
```

### Select, SelectTo

[`Select`, `SelectTo`](../../src/AwaitableCoroutine/Modules/SelectCoroutine.cs)
メソッドは、コルーチンの結果の値を変換した新たなコルーチンを生成します。

以下のように静的メソッドとして呼び出します。

```csharp
var res = await someCoroutine.Select(x => x * x);
```

**引数**
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
メソッドは、コルーチンの継続のコルーチンを生成し待機するコルーチンを生成します。

以下のように拡張メソッドとして呼び出します。

```csharp
runner.Create(() =>
    someCoroutine.AndThen(async x => {
        await Coroutine.Yield();
        return x * x;
    })
);
```

**引数**
* `Coroutine.AndThen`
  * `coroutine`: `Coroutine`
  * `thunk`: `Func<Coroutine>`
* `Coroutine.AndThen<T>`
  * `coroutine`: `Coroutine`
  * `thunk`: `Func<Coroutine<T>>`
* `Coroutine<T>.AndThen`
  * `coroutine`: `Coroutine<T>`
  * `thunk`: `Func<T, Coroutine>`
* `Coroutine<T>.AndThen<U>`
  * `coroutine`: `Coroutine<T>`
  * `thunk`: `Func<T< Coroutine<U>>`


### UntilCompleted

[`UntilCompleted`](../../src/AwaitableCoroutine/Modules/UntilCompletedCoroutine.cs)
メソッドは、コルーチンが未完了の間、特定の処理を繰り返し実行する新たなコルーチンを生成します。

以下のように拡張メソッドとして呼び出します。

```csharp
Coroutine.DelayCount(10)
    .UntilCompleted(async () => {
        Console.WriteLine("Hello");
        await Coroutine.DelayCount(3);
    });
```

この例では、10回のカウントが終わるまでの間、3回のカウント毎に`Hello`という文字列を出力するコルーチンを作成しています。

**引数**
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
メソッドは、`IEnumerator`を元にコルーチンを生成します。

**引数**
* `enumerator`: `IEnumerator`

### AwaitTask
[`AwaitTask(Task)`](../../src/AwaitableCoroutine/Modules/AwaitTask.cs)
メソッドは、`Task`の完了を待機するコルーチンを生成します。

**引数**
* `task`: `Task` または `Task<T>` または `ValueTask` または `ValueTask<T>`

### AwaitObservable
[`AwaitObservable(IObservable<T>)`](../../src/AwaitableCoroutine/Modules/AwaitObservable.cs)
メソッドは、`IObservable<T>`の次の値を待機するコルーチンを生成します。

**引数**
* `observable`: `IObservable<T>`

### AwaitObservableCompleted
[`AwaitObservableCompleted(IObservable<T>)`](../../src/AwaitableCoroutine/Modules/AwaitObservable.cs)
メソッドは、`IObservable<T>`の完了を待機するコルーチンを生成します。

**引数**
* `observable`: `IObservable<T>`

### CanceledException

`CanceledException`は、非同期メソッドによって作成されるコルーチンの実行をそのメソッド内からキャンセルしたいときに利用するための例外です。

それ以外の場合でコルーチンをキャンセルしたい場合、`Coroutine.Cancel()`メソッドを利用してください。

| Name | Desc |
| --- | --- |
| [`CanceledException`](../../src/AwaitableCoroutine/CalceledException.cs) | コルーチンのキャンセルを表す例外 |
| `ChildCanceledException` | 待機しているコルーチンがキャンセルされたことによるコルーチンのキャンセルを表す例外 |
| `ChildrenCanceledException` | 待機している複数のコルーチンがキャンセルされたことによるコルーチンのキャンセルを表す例外 |

`Coroutine`クラスの静的メソッドとして、以下の補助メソッドを提供しています。

| Name | Desc |
| --- | --- |
| [`ThrowCancel()`](../../src/AwaitableCoroutine/CalceledException.cs) | `CanceledException`をスロー |
| `ThrowChildCancel()` | `CanceledChildException`をスロー |
| `ThrowChildrenCancel()` | `CanceledChildrenException`をスロー |
