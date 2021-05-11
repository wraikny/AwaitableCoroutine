# AwaitableCoroutine.Altseed2

ゲームエンジン[Altseed2](https://altseed.github.io)向けの拡張パッケージです。

**目次**

- [AwaitableCoroutine.Altseed2](#awaitablecoroutinealtseed2)
  - [Altseed2Coroutine](#altseed2coroutine)
    - [モジュール](#モジュール)
      - [DelaySecond](#delaysecond)
  - [ノード](#ノード)
    - [CoroutineNode](#coroutinenode)

## Altseed2Coroutine
### モジュール

#### DelaySecond
[`DelaySecond(float)`](../src/AwaitableCoroutine.Altseed2/Modules.cs#L11)
は、指定した秒数待機するコルーチンを生成します。

`Altseed2Coroutine.DelaySecond(5.0f)`など、静的メソッドとして呼び出します。

Altseed2の提供する`Engine.DeltaSecond`を利用して秒数のカウントを行います。

二秒間待機するコルーチンを作成し、完了時に`2`を出力する処理を登録する例
```csharp
coroutineNode.Create(() =>
    Altseed2Coroutine.DelaySecond(2.0f)
        .OnCompleted(() => Console.WriteLine("2"))
);
```

**引数**
* `float second`

## ノード
### [CoroutineNode](../src/AwaitableCoroutine.Altseed2/CoroutineNode.cs)
コルーチンを登録・更新するノードです。

エンジンに登録すると、更新時に自動的に`Update`拡張メソッドが呼び出されます。

1フレームづつ何らかの処理を実行するコルーチンを作成する例
```csharp
var coroutineNode = new CoroutineNode();
Engine.Add(coroutineNode);

var co = coroutineNode.Create(async () => {
  while (true)
  {
    doSomething1();
    await AwaitableCoroutine.Yield();
  }
});
```

**継承**
* `Altseed2.Node`
* `AwaitableCoroutine.ICoroutineRunner`
