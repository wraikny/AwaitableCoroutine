[![](https://github.com/wraikny/AwaitableCoroutine/workflows/CI/badge.svg)](https://github.com/wraikny/AwaitableCoroutine/actions?workflow=CI)
[![Nuget](https://img.shields.io/nuget/v/AwaitableCoroutine?style=plastic)](https://www.nuget.org/packages/AwaitableCoroutine/)

# AwaitableCoroutine

AwaitableCoroutine is a library for C# that provides a coroutine that allows the use of async/await syntax.
Internally it uses Task-Like, the Awaitable pattern, and AsyncMethodBuilder.

AwaitableCoroutine は、async/await 構文を使用可能にしたコルーチンを提供する C# 向けライブラリです。
内部的にはTask-Like、Awaitable パターン、AsyncMethodBuilder が使われています。

## Install

Install from [NuGet Gallery](https://www.nuget.org/packages/AwaitableCoroutine).

## Example

```C#
public class Program
{
    private static int s_count;

    private static async AwaitableCoroutine CreateCoroutine()
    {
        for (var i = 0; i < 10; i++)
        {
            s_count++;
            await AwaitableCoroutine.Yield();
        }
    }

    public static void Main(string[] args)
    {
        var runner = new CoroutineRunner();

        var coroutine = runner.AddCoroutine(CreateCoroutine);

        Console.WriteLine("Started!");

        while (!coroutine.IsCompleted)
        {
            Console.WriteLine($"{s_count}");
            runner.Update();
        }

        Console.WriteLine("Finished!");
    }
}
```

```sh
$ dotnet run -p examples/AwaitableCoroutine.Example
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

## Command

### Setup

```sh
$ git submodule update --init
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
$ dotnet fake build -t test [-- <DEBUG|RELEASE>]
```

### Pack

```sh
$ dotnet pack src/AwaitableCoroutine -c Release -o .
``
