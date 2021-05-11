# AwaitableCoroutine ドキュメント

**目次**

- [AwaitableCoroutine ドキュメント](#awaitablecoroutine-ドキュメント)
  - [AwaitableCoroutineとは](#awaitablecoroutineとは)
  - [使い方](#使い方)
  - [注意事項](#注意事項)
  - [AwaitableCoroutine](#awaitablecoroutine)
    - [AwaitableCoroutine](#awaitablecoroutine-1)
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
    - [例外](#例外)
    - [ICoroutineRunner](#icoroutinerunner)
      - [メソッド・拡張メソッド](#メソッド拡張メソッド)
      - [静的メンバー](#静的メンバー)
  - [AwaitableCoroutine.FSharp](#awaitablecoroutinefsharp)
    - [コンピューテーション式](#コンピューテーション式)
      - [awaitableCoroutine](#awaitablecoroutine-2)
      - [ICoroutineRunner.Do](#icoroutinerunnerdo)
  - [AwaitableCoroutine.Altseed2](#awaitablecoroutinealtseed2)
    - [モジュール](#モジュール-1)
      - [DelaySecond](#delaysecond)
    - [ノード](#ノード)
      - [CoroutineNode](#coroutinenode)

## AwaitableCoroutineとは

## 使い方

例えば以下のようにコルーチンを定義します。

```csharp
private static async AwaitableCoroutine CreateCoroutine()
{
    while (true)
    {
        for (var i = 0; i < 5; i++)
        {
            Console.WriteLine($"Hello {i}");
            await AwaitableCoroutine.Yield();
        }

        Console.WriteLine("Start delay");

        // 指定したカウントだけ実行するコルーチンを生成
        await AwaitableCoroutine.DelayCount(10);
    }
}
```

そして、次のように呼び出します。

```csharp
public static void Main(string[] _)
{
    // Runnerのインスタンスを作成します
    var runner = new CoroutineRunner();

    // 注意: AwaitableCoroutineはContextメソッドに渡すコールバック内で作成する必要があります
    var co = runner.Context(CreateCoroutine);

    // メインループ
    while(!co.IsCompleted)
    {
        // Runner の Update メソッドを呼び出すことで、登録されているコルーチンが次に進めます
        runner.Update();
    }
}
```

非同期ラムダ式を利用してコルーチンの生成を行うこともできます。

```csharp
public static void Main(string[] _)
{
    var runner = new CoroutineRunner();

    // 注意: 非同期ラムダ式を利用する場合、ジェネリックパラメータの明示的な宣言が必要です
    var co = runner.Context<AwaitableCoroutine>(async () => {
        for (var i = 0; i < 5; i++)
        {
            Console.WriteLine($"Hello with lambda {i}");
            await AwaitableCoroutine.Yield();
        }
    });

    while(!co.IsCompleted)
    {
        runner.Update();
    }
}
```

## 注意事項

`AwaitableCoroutine`, `AwaitableCoroutine<T>`は、 `Context` メソッドのコールバック関数の中で生成する点に注意してください。

非同期ラムダ式を利用する場合、ジェネリックパラメータの明示的な宣言が必要です。

コルーチンをどの `ICoroutineRunner` に登録するかの情報を与えるために必要になります。

コルーチンの作成に引数を与えたい場合は以下のようにします。

```csharp
var coroutine = runner.Context(() => FooBarCoroutine(arg1, arg2));
```

## AwaitableCoroutine

### AwaitableCoroutine
[`AwaitableCoroutine`](../src/AwaitableCoroutine/AwaitableCoroutine.cs)
は、待機可能かつasyncメソッドを利用可能なコルーチンです。

ジェネリック版の`AwaitableCoroutine<T>`では結果の値を受け取ることができます。

#### メンバー

```csharp
coroutine.IsCompleted
```

などとして呼び出します。

| Name | Desc |
| --- | --- |
| `IsCompleted` | コルーチンが完了したかどうかを取得 |
| `IsCanceled` | コルーチンがキャンセルされたかどうかを取得 |
| `Cancel()` | コルーチンをキャンセルする |

#### モジュール

##### Yield
[`Yield`](../src/AwaitableCoroutine/Internal/YieldAwaitable.cs.cs)
は、`async`メソッド内で一度だけ`await`するためだけの構造体を生成します。

`AwaitableCoroutine.Yield()`と静的メソッドとして呼び出します。

通常のコルーチンと同じように利用することはできませんが、最適化のために構造体による実装をしています。

一度だけ`await`したい場合は、基本的にこちらを利用してください。（`DelayCount(0)`などを利用すると余計なオーバーヘッドが生じます)

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

##### While
[`While`](../src/AwaitableCoroutine/Modules/WhileCoroutine.cs)
は、指定した条件が真の間待機するコルーチンを生成します。

**引数**
* `Func<bool> predicate`

```csharp
AwaitableCoroutine.While(() => true)
```

などと静的メソッドとして呼び出します。

##### DelayCount
[`DelayCount`](../src/AwaitableCoroutine/Modules/DelayCountCoroutine.cs)
は、指定したカウントの回数だけ待機するコルーチンを生成します。

**引数**
* `int count`

```csharp
AwaitableCoroutine.DelayCount(5)
```

などと静的メソッドとして呼び出します。

##### WaitAll
[`WaitAll`](../src/AwaitableCoroutine/Modules/WaitAllCoroutine.cs)
は、指定したコルーチンが全て終了するまで待機するコルーチンを生成します。

**引数**
タプル版では2〜7個のコルーチンを渡すオーバーロードが用意してあります。
それ以上の場合は`Span<AwaitableCoroutineBase>`または`Span<AwaitableCoroutine<T>>`を引数として渡すことができます。

```csharp
AwaitableCoroutine.WaitAll(coroutine1, coroutine2)
```

などと静的メソッドとして呼び出します。

##### WaitAny
[`WaitAny(c1, c2, ...)`](../src/AwaitableCoroutine/Modules/WaitAnyCoroutine.cs)
は、指定したコルーチンのうちどれか一つが終了するまで待機するコルーチンを生成します。

**引数**
タプル版では2〜7個のコルーチンを渡すオーバーロードが用意してあります。
それ以上の場合は`Span<AwaitableCoroutineBase>`または`Span<AwaitableCoroutine<T>>`を引数として渡すことができます。

```csharp
AwaitableCoroutine.WaitAny(coroutine1, coroutine2)
```

などと静的メソッドとして呼び出します。

##### FromEnumerator
[`FromEnumerator(IEnumerator)`](../src/AwaitableCoroutine/Modules/EnumeratorCoroutine.cs)
は、`IEnumerator`を元にコルーチンを生成します。

**引数**
* `IEnumerator enumerator`

`IEnumerator`の拡張メソッドとして`ToAwaitable()`も提供しています。

##### Select, SelectTo

[`Select`, `SelectTo`](../src/AwaitableCoroutine/Modules/SelectCoroutine.cs)
は、コルーチンの結果の値を変換した新たなコルーチンを生成します。

```csharp
coroutine.Select(x => x * x)
```

などと、拡張メソッドとして呼び出します。

**引数**
* `SelectTo`
  * `AwaitableCoroutineBase coroutine`
  * `T result`
* `Select<T>`
  * `AwaitableCoroutine coroutine`
  * `Func<T> thunk`
* `Select<T, U>`
  * `AwaitableCoroutine<T> coroutine`
  * `Func<T, U> thunk`

##### AndThen

[`AndThen`](../src/AwaitableCoroutine/Modules/AndThenCoroutine.cs)
は、コルーチンの継続のコルーチンを生成し待機するコルーチンを生成します。

```csharp
coroutine.AndThen(() => AwaitableCoroutine.DelayCount(5))
```

や

```csharp
coroutine.AndThen(async x => {
  await AwaitableCoroutine.Yield();
  return x * x;
})
```

などと、拡張メソッドとして呼び出します。

**引数**
* オーバーロード1
  * `AwaitableCoroutine coroutine`
  * `Func<AwaitableCoroutine> thunk`
* オーバーロード2
  * `AwaitableCoroutine coroutine`
  * `Func<AwaitableCoroutine<T>> thunk`
* オーバーロード3
  * `AwaitableCoroutine<T> coroutine`
  * `Func<T, AwaitableCoroutine> thunk`
* オーバーロード4
  * `AwaitableCoroutine<T> coroutine`
  * `Func<T< AwaitableCoroutine<U>> thunk`


##### UntilCompleted

[`UntilCompleted`](../src/AwaitableCoroutine/Modules/UntilCompletedCoroutine.cs)
は、コルーチンが未完了の間`Action`を実行する新たなコルーチンを生成

```csharp
coroutine.UntilCompleted(() => Console.WriteLine("Hello"))
```

や

```csharp
coroutine.UntilCompleted(async () => {
  Console.WriteLine("Hello");
  await AwaitableCoroutine.DelayCount(3);
});
```

などと、拡張メソッドとして呼び出します。

**引数**
* `UntilCompleted`（単に実行する）
  * `AwaitableCoroutineBase coroutine`
  * `Action action`
* `UntilCompleted`（`AwaitableCoroutine`を待機する）
  * `AwaitableCoroutineBase coroutine`
  * `Func<AwaitableCoroutine> action`
* `UntilCompleted<T>`（`AwaitableCoroutine`を待機する）
  * `AwaitableCoroutineBase coroutine`
  * `Func<AwaitableCoroutine<T>> action`

### 例外

非同期メソッドで作成するコルーチンを非同期メソッド内からキャンセルしたいときに利用します。

それ以外の場合でコルーチンをキャンセルしたい場合、`AwaitableCoroutine.Cancel()`メソッドを利用してください。

| Name | Desc |
| --- | --- |
| [`CanceledException`](../src/AwaitableCoroutine/CalceledException.cs) | コルーチンのキャンセルを表す例外 |
| `ChildCanceledException` | 待機しているコルーチンがキャンセルされたことによるコルーチンのキャンセルを表す例外 |
| `ChildrenCanceledException` | 待機している複数のコルーチンがキャンセルされたことによるコルーチンのキャンセルを表す例外 |

`AwaitableCoroutine`クラスの静的メソッドとして、以下の補助メソッドを提供しています。

| Name | Desc |
| --- | --- |
| [`ThrowCancel()`](../src/AwaitableCoroutine/CalceledException.cs) | `CanceledException`をスロー |
| `ThrowChildCancel()` | `CanceledChildException`をスロー |
| `ThrowChildrenCancel()` | `CanceledChildrenException`をスロー |

### ICoroutineRunner

[`ICoroutineRunner`](../src/AwaitableCoroutine/ICoroutineRunner.cs)
は、コルーチンを登録・実行するオブジェクトを表すインターフェースです。

基本的には標準の`CoroutineRunner`の利用が推奨されますが、インターフェースを実装することでランナーを自作することもできます。

#### メソッド・拡張メソッド
| Name | Desc |
| --- | --- |
| `Update()` | 登録済みのコルーチンを次に進めます。 実行されたコルーチンの例外がスローされる場合があります。|
| `Context` | ランナーをコンテキストにセットした中でコールバックメソッドを実行します。 |

#### 静的メンバー
| Name | Desc |
| --- | --- |
| `GetContext()` | 現在のコンテキストにセットされた`ICoroutineRunner`のインスタンスを取得します。 |

## AwaitableCoroutine.FSharp

F#向けの拡張パッケージです。

### コンピューテーション式

#### awaitableCoroutine

サンプルコード

```fsharp
let runner = CoroutineRunner()

let coroutine = runner.Context(fun () ->
  awaitableCoroutine {
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

#### ICoroutineRunner.Do

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

## AwaitableCoroutine.Altseed2

ゲームエンジン[Altseed2](https://altseed.github.io)向けの拡張パッケージです。

### モジュール

#### DelaySecond
[`DelaySecond(float)`](../src/AwaitableCoroutine.Altseed2/Modules.cs#L11)
は、指定した秒数待機するコルーチンを生成します。

`AwaitableCoroutine.DelaySecond(5.0f)`など、静的メソッドとして呼び出します。

Altseed2の提供する`Engine.DeltaSecond`を利用して秒数のカウントを行います。

**引数**
* `float second`

### ノード
#### [CoroutineNode](../src/AwaitableCoroutine.Altseed2/CoroutineNode.cs)
コルーチンを登録・更新するノード。

**継承**
* `Altseed2.Node`
* `AwaitableCoroutine.ICoroutineRunner`
