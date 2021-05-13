# AwaitableCoroutine
[`AwaitableCoroutine`](../../src/AwaitableCoroutine/AwaitableCoroutine.cs)
は、待機可能かつasyncメソッドを使って作成可能なコルーチンです。

ジェネリック版の`AwaitableCoroutine<T>`は結果の値を返します。

**目次**

- [AwaitableCoroutine](#awaitablecoroutine)
  - [メンバー](#メンバー)
  - [モジュール](#モジュール)
    - [Yield](#yield)
    - [While](#while)
    - [DelayCount](#delaycount)
    - [WaitAll](#waitall)
    - [WaitAny](#waitany)
    - [FromEnumerator](#fromenumerator)
    - [Select, SelectTo](#select-selectto)
    - [AndThen](#andthen)
    - [UntilCompleted](#untilcompleted)
    - [CanceledException](#canceledexception)

## メンバー

| Name | Desc |
| --- | --- |
| `IsCompleted` | コルーチンが完了したかどうかを取得 |
| `IsCanceled` | コルーチンがキャンセルされたかどうかを取得 |
| `Cancel()` | コルーチンをキャンセルする |

## モジュール

### Yield
[`Yield`](../../src/AwaitableCoroutine/Internal/YieldAwaitable.cs.cs)
メソッドは、`async`メソッド内で一度だけ`await`するためだけの構造体を生成します。

以下のように静的メソッドとして呼び出します。

```csharp
await AwaitableCoroutine.Yield();
```

通常のコルーチンと同じように利用することはできませんが、最適化のために構造体による実装をしています。

一度だけ`await`したい場合はこちらを利用してください。（`DelayCount(0)`などを利用すると余計なオーバーヘッドが生じます)

例

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
メソッドは、指定した条件が真の間待機するコルーチンを生成します。

**引数**
* `predicate`: `Func<bool>`

以下のように静的メソッドとして呼び出します。

```csharp
AwaitableCoroutine.While(() => true)
```

### DelayCount
[`DelayCount`](../../src/AwaitableCoroutine/Modules/DelayCountCoroutine.cs)
メソッドは、指定したカウントの回数だけ待機するコルーチンを生成します。

**引数**
* `count`: `int`

以下のように静的メソッドとして呼び出します。

```csharp
AwaitableCoroutine.DelayCount(5)
```

### WaitAll
[`WaitAll`](../../src/AwaitableCoroutine/Modules/WaitAllCoroutine.cs)
メソッドは、指定したコルーチンが全て終了するまで待機するコルーチンを生成します。

**引数**
タプル版では2〜7個のコルーチンを渡すオーバーロードが用意してあります。
それ以上の場合は`Span<AwaitableCoroutineBase>`または`Span<AwaitableCoroutine<T>>`を引数として渡すことができます。

以下のように静的メソッドとして呼び出します。

```csharp
AwaitableCoroutine.WaitAll(coroutine1, coroutine2)
```

### WaitAny
[`WaitAny(c1, c2, ...)`](../../src/AwaitableCoroutine/Modules/WaitAnyCoroutine.cs)
メソッドは、指定したコルーチンのうちどれか一つが終了するまで待機するコルーチンを生成します。

**引数**
タプル版では2〜7個のコルーチンを渡すオーバーロードが用意してあります。
それ以上の場合は`Span<AwaitableCoroutineBase>`または`Span<AwaitableCoroutine<T>>`を引数として渡すことができます。

以下のように静的メソッドとして呼び出します。

```csharp
AwaitableCoroutine.WaitAny(coroutine1, coroutine2)
```

### FromEnumerator
[`FromEnumerator(IEnumerator)`](../../src/AwaitableCoroutine/Modules/EnumeratorCoroutine.cs)
メソッドは、`IEnumerator`を元にコルーチンを生成します。

**引数**
* `enumerator`: `IEnumerator`

`IEnumerator`の拡張メソッドとして`ToAwaitable()`も提供しています。

### Select, SelectTo

[`Select`, `SelectTo`](../../src/AwaitableCoroutine/Modules/SelectCoroutine.cs)
メソッドは、コルーチンの結果の値を変換した新たなコルーチンを生成します。

以下のように静的メソッドとして呼び出します。

```csharp
var res = await someCoroutine.Select(x => x * x);
```

**引数**
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
メソッドは、コルーチンの継続のコルーチンを生成し待機するコルーチンを生成します。

以下のように拡張メソッドとして呼び出します。

```csharp
runner.Create(() =>
    someCoroutine.AndThen(async x => {
        await AwaitableCoroutine.Yield();
        return x * x;
    })
);
```

**引数**
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
メソッドは、コルーチンが未完了の間、特定の処理を繰り返し実行する新たなコルーチンを生成します。

以下のように拡張メソッドとして呼び出します。

```csharp
AwaitableCoroutine.DelayCount(10)
    .UntilCompleted(async () => {
        Console.WriteLine("Hello");
        await AwaitableCoroutine.DelayCount(3);
    });
```

この例では、10回のカウントが終わるまでの間、3回のカウント毎に`Hello`という文字列を出力するコルーチンを作成しています。

**引数**
* `UntilCompleted`（Action版）
  * `AwaitableCoroutineBase coroutine`
  * `Action action`
* `UntilCompleted`（`AwaitableCoroutine`を実行する）
  * `AwaitableCoroutineBase coroutine`
  * `Func<AwaitableCoroutine> action`
* `UntilCompleted<T>`（`AwaitableCoroutine`を実行する）
  * `AwaitableCoroutineBase coroutine`
  * `Func<AwaitableCoroutine<T>> action`

### CanceledException

`CanceledException`は、非同期メソッドによって作成されるコルーチンの実行をそのメソッド内からキャンセルしたいときに利用するための例外です。

それ以外の場合でコルーチンをキャンセルしたい場合、`AwaitableCoroutine.Cancel()`メソッドを利用してください。

| Name | Desc |
| --- | --- |
| [`CanceledException`](../../src/AwaitableCoroutine/CalceledException.cs) | コルーチンのキャンセルを表す例外 |
| `ChildCanceledException` | 待機しているコルーチンがキャンセルされたことによるコルーチンのキャンセルを表す例外 |
| `ChildrenCanceledException` | 待機している複数のコルーチンがキャンセルされたことによるコルーチンのキャンセルを表す例外 |

`AwaitableCoroutine`クラスの静的メソッドとして、以下の補助メソッドを提供しています。

| Name | Desc |
| --- | --- |
| [`ThrowCancel()`](../../src/AwaitableCoroutine/CalceledException.cs) | `CanceledException`をスロー |
| `ThrowChildCancel()` | `CanceledChildException`をスロー |
| `ThrowChildrenCancel()` | `CanceledChildrenException`をスロー |
