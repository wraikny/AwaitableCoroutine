using System;

using AwaitableCoroutine;

var runner = new CoroutineRunner();

int count = 0;

var coroutine = runner.Create(async () => {
    Console.WriteLine("Started!");

    for (var i = 0; i < 10; i++)
    {
        count++;
        await Coroutine.Yield();
    }
}).OnCompleted(() => Console.WriteLine("Finished!"));

while (true)
{
    runner.Update();
    if (coroutine.IsCompletedSuccessfully) break;

    Console.WriteLine($"{count}");
}
