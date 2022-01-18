[![](https://github.com/wraikny/AwaitableCoroutine/workflows/CI/badge.svg)](https://github.com/wraikny/AwaitableCoroutine/actions?workflow=CI)

# AwaitableCoroutine

AwaitableCoroutine is a library for C# that provides a coroutine that allows the use of async/await syntax.
Internally it uses Task-Like, the Awaitable pattern, and AsyncMethodBuilder.

AwaitableCoroutine は、async/await 構文を使用可能にしたコルーチンを提供する C# 向けライブラリです。
内部的にはTask-Like、Awaitable パターン、AsyncMethodBuilder が使われています。

## Installation

Install from NuGet Gallery

| PackageId | Badge |
| --- | --- |
| AwaitableCoroutine | [![AwaitableCoroutine - NuGet Gallery](https://img.shields.io/nuget/v/AwaitableCoroutine?style=plastic)](https://www.nuget.org/packages/AwaitableCoroutine/) |
| AwaitableCoroutine.Altseed2 | [![AwaitableCoroutine.Altseed2 - NuGet Gallery](https://img.shields.io/nuget/v/AwaitableCoroutine.Altseed2?style=plastic)](https://www.nuget.org/packages/AwaitableCoroutine.Altseed2/) |
| AwaitableCoroutine.FSharp | [![AwaitableCoroutine.FSharp - NuGet Gallery](https://img.shields.io/nuget/v/AwaitableCoroutine.FSharp?style=plastic)](https://www.nuget.org/packages/AwaitableCoroutine.FSharp/) |

## Documentation

| lang | link |
| --- | --- |
| ja | [ドキュメント](docs/ja) |
| en | [Document](docs/en) |


## Example

```C#
using System;

using AwaitableCoroutine;
using AwCo = AwaitableCoroutine.AwaitableCoroutine;

var runner = new CoroutineRunner();

int count = 0;

var coroutine = runner.Create(async () => {
    Console.WriteLine("Started!");

    for (var i = 0; i < 10; i++)
    {
        count++;
        await AwCo.Yield();
    }
}).OnCompleted(() => Console.WriteLine("Finished!"));

while (true)
{
    runner.Update();
    if (coroutine.IsCompleted) break;

    Console.WriteLine($"{count}");
}
```

```sh
$ dotnet run --project examples/AwaitableCoroutine.Example
Started!
0
1
2
3
4
5
6
7
8
9
10
Finished!
```

see [examples](examples) in detail.

## Command (for dev)

### Setup

```sh
$ dotnet tool restore
```

### Build

```sh
$ dotnet fake build [-- <DEBUG|RELEASE>]
```

Default configuration is DEBUG

### Format

```sh
$ dotnet fake build -t format
```

### Test

```sh
$ dotnet fake build -t test
```

### Pack

```sh
$ dotnet pack  -c Release
```
