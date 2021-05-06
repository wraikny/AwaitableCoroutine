using System;

using Xunit;

using AwaitableCoroutine.Modules;

namespace AwaitableCoroutine.Test
{
    public class AwaitTest
    {
        public sealed class Counter
        {
            public int Count { get; private set; }

            public void Inc() => Count++;
        }

        public static async AwaitableCoroutine GetCoroutine(ICoroutineRunner runner, Counter counter)
        {
            Console.WriteLine("await 0");
            await runner.Yield();

            Console.WriteLine("await 1");
            counter.Inc();
            await runner.Yield();

            Console.WriteLine("await 2");
            counter.Inc();
            await runner.Yield();

            Console.WriteLine("await 3");
            counter.Inc();
        }

        [Fact]
        public void CreateCoroutine()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();

            ICoroutineRunner.SetInstance(runner);
            var _ = GetCoroutine(runner, counter);
        }

        [Fact]
        public void RunCoroutine()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();

            ICoroutineRunner.SetInstance(runner);
            var coroutine = GetCoroutine(runner, counter);

            runner.Register(coroutine);
            Assert.True(runner.Count == 1);
            Assert.True(counter.Count == 0);

            Console.WriteLine("Updating 0");
            runner.Update();
            Console.WriteLine("Updated 0");
            Assert.True(counter.Count == 0);

            Console.WriteLine("Updating 1");
            runner.Update();
            Console.WriteLine("Updated 1");
            Assert.True(counter.Count == 1);

            Console.WriteLine("Updating 2");
            runner.Update();
            Console.WriteLine("Updated 2");
            Assert.True(counter.Count == 2);


        }
    }
}
