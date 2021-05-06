# AwaitableCoroutine

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

