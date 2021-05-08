# AwaitableCoroutine ドキュメント

## 使い方

例えば以下のようにコルーチンを定義します。

```C#
// フレームワークなどで提供される DeltaTime があれば、そちらを使用すると良いです。
private static float DeltaSecond { get; set; }

private static async AwaitableCoroutine CreateCoroutine()
{
    while (true)
    {
        for (var i = 0; i < 5; i++)
        {
            Console.WriteLine($"Hello {i}");
            // 
            await AwaitableCoroutine.Yield();
        }

        Console.WriteLine("Start delay");

        // 指定した間待機するコルーチンです
        // 2つ目の引数で、前の更新からの経過時間を取得する関数を登録します
        await AwaitableCoroutine.Delay(5.0f, () => DeltaSecond));
    }
}
```

そして、次のように呼び出します。

```C#
public static void Main(string[] _)
{
    // Runnerのインスタンスを作成します。
    var runner = new CoroutineRunner();

    // AddCoroutine メソッドに渡す callback内で AwaitableCoroutine を作成します。
    _ = runner.AddCoroutine(CreateCoroutine);

    var sw = new StopWatch();
    sw.Start();
    
    // メインループ
    while(true)
    {
        // Runner の Update メソッドを呼び出すことで、登録されているコルーチンが次に進みます。
        runner.Update();

        DeltaSecond = (float)sw.ElapsedMilliseconds / 1000.0f;
        sw.Restart();
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
#### `AwaitableCoroutine.Yield`
一度だけ実行されるコルーチン。

#### `AwaitableCoroutine.Until`
指定した条件が真になるまで実行されるコルーチン。

#### `AwaitableCoroutine.While`
指定した条件が真の間実行されるコルーチン。

#### `AwaitableCoroutine.DelayCount`
指定したカウント実行されるコルーチン。

#### `AwaitableCoroutine.WaitAll`
指定した`AwaitableCoroutine`のうち、全てが完了するまで実行されるコルーチン。

#### `AwaitableCoroutine.WaitAny`
指定した`AwaitableCoroutine`のうち、どれか一つが完了するまで実行されるコルーチン。

### 補助メソッド
#### `Select`, `SelectTo`
`AwaitableCoroutine`の値を変換した`AwaitableCoroutine<T>`を`await`無しに生成する

#### `AndThen`
`await`演算子を使用せずに継続の処理を記述できる。

## AwaitableCoroutine.Altseed2

### コルーチン
#### `AwaitableCoroutine.DelaySecond`
指定した秒数実行されるコルーチン。

### ノード
#### `CoroutineNode`
コルーチンを登録するノード。
