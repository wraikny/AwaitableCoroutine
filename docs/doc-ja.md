# AwaitableCoroutine ドキュメント

## 使い方

例えば以下のようにコルーチンを定義します。

```C#
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

```C#
public static void Main(string[] _)
{
    // Runnerのインスタンスを作成
    var runner = new CoroutineRunner();

    // 注意: AwaitableCoroutineはContextメソッドに渡すコールバック内で作成する必要がある
    _ = runner.Context(CreateCoroutine);

    // メインループ
    while(true)
    {
        // Runner の Update メソッドを呼び出すことで、登録されているコルーチンが次に進む
        runner.Update();
    }
}
```

## 注意事項

`AwaitableCoroutine`, `AwaitableCoroutine<T>`は、 `Context` メソッドのコールバック関数の中で生成する点に注意してください。

コルーチンをどの `ICoroutineRunner` に登録するかの情報を与えるために必要になります。

コルーチンの作成に引数を与えたい場合は以下のようにします。

```C#
var coroutine = runner.Context(() => FooBarCoroutine(arg1, arg2));
```

## AwaitableCoroutine
### コルーチン

`AwaitableCoroutine.Yield()`などと、静的メソッドとして呼び出します。

| Name | Desc |
| --- | --- |
| [While](../src/AwaitableCoroutine/Modules/WhileCoroutine.cs) | 条件が真の間実行するコルーチンを生成 |
| [DelayCount](../src/AwaitableCoroutine/Modules/DelayCountCoroutine.cs) | 指定したカウント実行するコルーチンを生成 |
| [WaitAll](../src/AwaitableCoroutine/Modules/WaitAllCoroutine.cs) | 指定した全てのコルーチンが終了するまで実行するコルーチンを生成 |
| [WaitAny](../src/AwaitableCoroutine/Modules/WaitAnyCoroutine.cs) | 指定したどれか一つのコルーチンが終了するまで実行するコルーチンを生成 |
| [FromEnumerator](../src/AwaitableCoroutine/Modules/EnumeratorCoroutine.cs) | `IEnumerator`を一つづつすすめるコルーチンを生成 |

### コルーチン（拡張メソッド）
`coroutine.Select(() => 1)`などと、拡張メソッドとして呼び出します。

| Name | Desc |
| --- | --- |
| [Select, SelectTo](../src/AwaitableCoroutine/Modules/SelectCoroutine.cs) | コルーチンの結果の値を変換した新たなコルーチンを生成 |
| [AndThen](../src/AwaitableCoroutine/Modules/AndThenCoroutine.cs) | コルーチンを継続した新たなコルーチンを生成 |
| [UntilCompleted](../src/AwaitableCoroutine/Modules/UntilCompletedCoroutine.cs) | コルーチンの実行中に`Action`を実行する新たなコルーチンを生成 |


### AwaitableYield

[AwaitableYield](../src/AwaitableCoroutine/Internal/AwaitableYield.cs.cs)

`async`メソッド内で一度だけ`await`するためだけの構造体です。

通常のコルーチンと同じように利用することはできませんが、最適化のために構造体による実装をしています。

一度だけ`await`したい場合は、基本的にこちらを利用してください。

例

```csharp
for (int i = 0; i < 10; i++)
{
    // do something
    await AwaitableCoroutine.Yield();
}
```


## AwaitableCoroutine.Altseed2

### コルーチン

`AwaitableCoroutine.DelaySecond()`などとして呼び出します。


| Name | Desc |
| --- | --- |
| [DelaySecond](../src/AwaitableCoroutine.Altseed2/Modules.cs#L11) | 指定した秒数実行 |


### ノード
#### [CoroutineNode](../src/AwaitableCoroutine.Altseed2/CoroutineNode.cs)
コルーチンを登録・更新するノード。
