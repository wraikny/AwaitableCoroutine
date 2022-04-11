# ICoroutineRunner

[`ICoroutineRunner`](../../src/AwaitableCoroutine/ICoroutineRunner.cs)
は、コルーチンを登録・実行するオブジェクトを表すインターフェースです。

基本的には標準の`CoroutineRunner`の利用が推奨されますが、インターフェースを実装することでランナーを自作することもできます。

**目次**
- [ICoroutineRunner](#icoroutinerunner)
  - [メソッド・拡張メソッド](#メソッド拡張メソッド)
    - [Update](#update)
    - [Create](#create)
    - [Context](#context)
  - [静的メンバー](#静的メンバー)

## メソッド・拡張メソッド

### Update
`Update`メソッドは、登録済みのコルーチンを次に進めます。 実行されたコルーチンが例外を吐いた場合、全てのコルーチンの更新後に例外がスローされます。

### Create
ランナーをコンテキストにセットした内側でコールバックメソッドを実行して、`Coroutine`または`Coroutine<T>`を作成します。

**引数**
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
ランナーをコンテキストにセットした中でコールバックメソッドを実行します。

**引数**
* オーバーロード1
  * `action`: `Action`
* オーバーロード2
  * `init`: `Func<T>`

```csharp
var (co1, co2, waitAll) = runner.Context(() => {
  var co1 = MyCoroutine1();
  var co2 = MyCoroutine2();
  return (co1, co2, AwaitableCoroutine.WaitAll(co1, co2));
});
```


## 静的メンバー
| Name | Desc |
| --- | --- |
| `GetContext()` | 現在のコンテキストにセットされた`ICoroutineRunner`のインスタンスを取得します |
