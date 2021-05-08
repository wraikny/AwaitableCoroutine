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

    // 注意: AwaitableCoroutineはAddCoroutineメソッドに渡すコールバック内で作成する必要がある
    _ = runner.AddCoroutine(CreateCoroutine);

    // メインループ
    while(true)
    {
        // Runner の Update メソッドを呼び出すことで、登録されているコルーチンが次に進む
        runner.Update();
    }
}
```

## 注意事項

`AwaitableCoroutine`, `AwaitableCoroutine<T>`は、 `AddCoroutine` メソッドのコールバック関数の中で生成する点に注意してください。

コルーチンをどの `ICoroutineRunner` に登録するかの情報を与えるために必要になります。

コルーチンの作成に引数を与えたい場合は以下のようにします。

```C#
_ = runner.AddCoroutine(() => FooBarCoroutine(arg1, arg2));
```

## AwaitableCoroutine
### コルーチン

`AwaitableCoroutine.Yield()`などとして呼び出します。

| Name | Desc |
| --- | --- |
| [Yield](../src/AwaitableCoroutine/Modules/YieldCoroutine.cs) | 一度だけ実行するコルーチンを生成 |
| [Until](../src/AwaitableCoroutine/Modules/UntilCoroutine.cs) | 条件が偽の間実行するコルーチンを生成 |
| [While](../src/AwaitableCoroutine/Modules/UntilCoroutine.cs) | 条件が真の間実行するコルーチンを生成 |
| [DelayCount](../src/AwaitableCoroutine/Modules/DelayCountCoroutine.cs) | 指定したカウント実行するコルーチンを生成 |
| [WaitAll](../src/AwaitableCoroutine/Modules/WaitAllCoroutine.cs) | 指定した全てのコルーチンが終了するまで実行するコルーチンを生成 |
| [WaitAny](../src/AwaitableCoroutine/Modules/WaitAnyCoroutine.cs) | 指定したどれか一つのコルーチンが終了するまで実行するコルーチンを生成 |
| [FromEnumerator](../src/AwaitableCoroutine/Modules/EnumeratorCoroutine.cs) | `IEnumerator`をすすめるコルーチンを生成 |


### 補助メソッド

| Name | Desc |
| --- | --- |
| [Select, SelectTo](../src/AwaitableCoroutine/Modules/SelectCoroutine.cs) | コルーチンの結果の値を変換した新たなコルーチンを生成 |
| [AndThen](../src/AwaitableCoroutine/Modules/AndThenCoroutine.cs) | コルーチンを継続した新たなコルーチンを生成 |
| [With](../src/AwaitableCoroutine/Modules/WithCoroutine.cs) | コルーチンの実行中に`Action`を実行するコルーチンを生成 |


## AwaitableCoroutine.Altseed2

### コルーチン

`AwaitableCoroutine.DelaySecond()`などとして呼び出します。


| Name | Desc |
| --- | --- |
| [DelaySecond](../src/AwaitableCoroutine.Altseed2/Modules.cs#L11) | 指定した秒数実行 |


### ノード
#### [CoroutineNode](../src/AwaitableCoroutine.Altseed2/CoroutineNode.cs)
コルーチンを登録・更新するノード。
