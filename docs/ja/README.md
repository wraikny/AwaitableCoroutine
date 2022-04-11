# AwaitableCoroutine ドキュメント


**目次**
- [AwaitableCoroutine ドキュメント](#awaitablecoroutine-ドキュメント)
  - [AwaitableCoroutineとは](#awaitablecoroutineとは)
    - [嬉しいところ](#嬉しいところ)
  - [使い方](#使い方)
  - [注意事項](#注意事項)
  - [Coroutine, Coroutine<T> クラス](#coroutine-coroutinet-クラス)
  - [ICoroutineRunner インターフェース](#icoroutinerunner-インターフェース)
  - [AwaitableCoroutine.FSharp パッケージ](#awaitablecoroutinefsharp-パッケージ)
  - [AwaitableCoroutine.Altseed2 パッケージ](#awaitablecoroutinealtseed2-パッケージ)


## AwaitableCoroutineとは

C#でasync/await構文を利用可能なコルーチン`Coroutine`を提供するパッケージです。

### 嬉しいところ

* C#でasync/await構文を使ってコルーチンを記述できる。
* コルーチンはランナーの`Update`を明示的に呼ぶことで実行されるため、簡単に更新タイミングを制御できる。
* 実行したコルーチンを待機することができる。
* 非同期ラムダ式を使ってインラインにコルーチンを記述できる。
* コルーチンをキャンセルすることができる。
* コルーチンをキャンセルした際に、そのコルーチンを待機しているコルーチンもまとめてキャンセルされる。
* 様々な便利なメソッドを標準で提供している。
* ステートマシン生成のコストが気になる場合は、継承を利用して自作可能
* インターフェースを利用して、一部の処理の挙動を書き換え可能
* F#向けに TaskBuilder.fs を基にしたコンピューテーション式を提供している。（AwaitableCoroutine.FSharp パッケージ）

## 使い方

例えば以下のようにコルーチンを定義します。

```csharp
// async メソッドを利用して Coroutine を作成可能
private static async Coroutine CreateCoroutine()
{
    while (true)
    {
        for (var i = 0; i < 5; i++)
        {
            Console.WriteLine($"Hello {i}");
            
            // Yieldを利用して、一度だけawaitで待機（中断）する
            await Coroutine.Yield();
        }

        Console.WriteLine("Start delay");

        // 指定したカウントだけ実行するコルーチンを生成して、awaitで待機する
        await Coroutine.DelayCount(10);
    }
}
```

そして、次のように呼び出します。

```csharp
public static void Main(string[] _)
{
    // Runnerのインスタンスを作成します
    var runner = new CoroutineRunner();

    // 注意: AwaitableCoroutineはCreate拡張メソッドまたはContext拡張メソッドに渡すコールバック内で作成する必要があります
    var coroutine = runner.Create(CreateCoroutine);
    /*
      // Context拡張メソッドを利用する場合
      var coroutine = runner.Context(CreateCoroutine);
    */

    // メインループ
    while(!coroutine.IsCompletedSuccessfully)
    {
        // ICoroutineRunnerのUpdate拡張メソッドを呼び出すことで、登録されているコルーチンを次に進めます
        runner.Update();
    }
}
```

非同期ラムダ式を利用してコルーチンの生成を行うこともできます。

```csharp
public static void Main(string[] _)
{
    var runner = new CoroutineRunner();

    // 非同期ラムダ式を利用して`Coroutine`を作成する
    var coroutine = runner.Create(async () => {
        for (var i = 0; i < 5; i++)
        {
            Console.WriteLine($"Hello with async lambda {i}");
            await Coroutine.Yield();
        }
    });

    // 注意: 非同期ラムダ式を`Context`メソッドで利用する場合はジェネリックパラメータの明示的な宣言が必要です
    /*
      var coroutine = runner.Context<Coroutine>(async () => {
        await Coroutine.Yield();
      });
    */

    while(!coroutine.IsCompletedSuccessfully)
    {
        runner.Update();
    }
}
```

## 注意事項

`Coroutine`, `Coroutine<T>`は、 `Create`拡張メソッドまたは`Context` 拡張メソッドのコールバック関数の中で生成する点に注意してください。
コルーチンをどの `ICoroutineRunner` に登録するかの情報を与えるために必要になります。
基本的には`Create`拡張メソッドを使用します。

`Coroutine`をまとめて作成したタプルを返すなど、`AwaitableCoroutien`または`AwaitableCoroutien<T>`以外の型を返したい場合は、以下のように`Context`拡張メソッドを利用します。

```csharp
var (c1, c2) = runner.Context(() => (Coroutine.DelayCount(1), Coroutine.DelayCount(1)));
```

非同期ラムダ式を利用して`Coroutine`を作成する場合、ジェネリックパラメータを明示しないと`Task`として推論されてしまうので気をつけてください。

コルーチンの作成に引数を与えたい場合は以下のようにします。

```csharp
var coroutine = runner.Context(() => FooBarCoroutine(arg1, arg2));
```

## Coroutine, Coroutine<T> クラス

待機可能なコルーチンのクラスです。

[Coroutine.md](Coroutine.md)を参照してください。

## ICoroutineRunner インターフェース

コルーチンを実行するコンテキストとなるインターフェースです。

[ICoroutineRunner.md](ICoroutineRunner.md)を参照してください。

## AwaitableCoroutine.FSharp パッケージ

F#向けの拡張パッケージです。

[AwaitableCoroutine.FSharp.md](AwaitableCoroutine.FSharp.md)を参照してください。

## AwaitableCoroutine.Altseed2 パッケージ

ゲームエンジンAltseed2向けの拡張パッケージです。

[AwaitableCoroutine.Altseed2.md](AwaitableCoroutine.Altseed2.md)を参照してください。
