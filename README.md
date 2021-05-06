# AwaitableCoroutine

AwaitableCoroutine is a library for C# that provides a coroutine that can use async/await syntax.

AwaitableCoroutine は async/await 構文を使用できるコルーチンを提供する C# 向けライブラリです。

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