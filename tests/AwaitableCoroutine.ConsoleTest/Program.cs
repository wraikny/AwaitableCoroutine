using System;

using AwaitableCoroutine;

namespace AwaitableCoroutine.ConsoleTest
{
    class Program
    {
        private static void RunAndThen()
        {
            var runner = new CoroutineRunner();
            var count = 0;

            var c = runner.Context(() =>
                AwaitableCoroutine.DelayCount(0)
                    .AndThen(() => AwaitableCoroutine.DelayCount(0, onCompleted: () =>
                    {
                        count++;
                        Internal.Logger.Log($"Count: {count}");
                    }))
            );

            while (!c.IsCompleted) runner.Update();

            Internal.Logger.Log($"Result: {count}");
            Assert.True(count == 1);
        }

        static void Main(string[] args)
        {
            Internal.Logger.SetLogger(Console.WriteLine);

            RunAndThen();
        }
    }
}
