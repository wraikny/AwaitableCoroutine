using System;

using Altseed2;

using AwaitableCoroutine;
using AwaitableCoroutine.Altseed2;

namespace AwaitableCoroutine.Altseed2.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var config = new Configuration { EnabledCoreModules = CoreModules.None };
                if (!Engine.Initialize("AwaitableCoroutine", 1, 1, config))
                {
                    throw new Exception("Failed to initialize the engine");
                }

                var runner = new CoroutineNode();
                Engine.AddNode(runner);

                var coroutine = runner.Create(async () => {
                    var i = 0;
                    await Altseed2Coroutine.DelaySecond(2.0f).UntilCompleted(async () => {
                        Console.WriteLine($"Stepping: {i++}");
                        await Altseed2Coroutine.DelaySecond(0.2f);
                    });
                    Console.WriteLine("Hello, AwaitableCoroutine.Altseed2");
                });

                while (Engine.DoEvents())
                {
                    if (coroutine.IsCompleted) break;

                    Engine.Update();
                }
            }
            finally
            {
                Engine.Terminate();
            }
        }
    }
}
