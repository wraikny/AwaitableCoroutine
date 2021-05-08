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
| [Yield](https://github.com/wraikny/AwaitableCoroutine/blob/master/src/AwaitableCoroutine/Modules/YieldCoroutine.cs) | 一度だけ実行 |
| [Until](https://github.com/wraikny/AwaitableCoroutine/blob/master/src/AwaitableCoroutine/Modules/UntilCoroutine.cs) | 条件が真になるまで実行 |
| [While](https://github.com/wraikny/AwaitableCoroutine/blob/master/src/AwaitableCoroutine/Modules/UntilCoroutine.cs) | 条件が真の間実行 |
| [DelayCount](https://github.com/wraikny/AwaitableCoroutine/blob/master/src/AwaitableCoroutine/Modules/DelayCountCoroutine.cs) | 指定したカウント実行 |
| [WaitAll](https://github.com/wraikny/AwaitableCoroutine/blob/master/src/AwaitableCoroutine/Modules/WaitAllCoroutine.cs) | 指定した全てのコルーチンが終了するまで実行 |
| [WaitAny](https://github.com/wraikny/AwaitableCoroutine/blob/master/src/AwaitableCoroutine/Modules/WaitAnyCoroutine.cs) | 指定したどれか一つのコルーチンが終了するまで実行 |
| [FromEnumerator](https://github.com/wraikny/AwaitableCoroutine/blob/master/src/AwaitableCoroutine/Modules/EnumeratorCoroutine.cs) | `IEnumerator`によるコルーチンを実行する |


### 補助メソッド

| Name | Desc |
| --- | --- |
| [Select, SelectTo](https://github.com/wraikny/AwaitableCoroutine/blob/master/src/AwaitableCoroutine/Modules/Select.cs) | `await`無しに値を変換した新たなコルーチンを生成 |
| [AndThen](https://github.com/wraikny/AwaitableCoroutine/blob/master/src/AwaitableCoroutine/AwaitableCoroutine.cs) | コルーチンを継続した新たなコルーチンを生成 |


## AwaitableCoroutine.Altseed2

### コルーチン

`AwaitableCoroutine.DelaySecond()`などとして呼び出します。


| Name | Desc |
| --- | --- |
| [DelaySecond](https://github.com/wraikny/AwaitableCoroutine/blob/master/src/AwaitableCoroutine.Altseed2/Modules.cs#L11) | 指定した秒数実行 |


### ノード
#### [CoroutineNode](https://github.com/wraikny/AwaitableCoroutine/blob/master/src/AwaitableCoroutine.Altseed2/CoroutineNode.cs)
コルーチンを登録・更新するノード。
