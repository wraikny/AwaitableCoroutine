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
