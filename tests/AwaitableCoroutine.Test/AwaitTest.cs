using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class AwaitTest : TestTemplate
    {
        public AwaitTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
            
        }

        private async AwaitableCoroutine GetCoroutine(Counter counter)
        {
            for (var i = 0; i < 3; i++)
            {
                counter.Inc();
                Log($"Count: {counter.Count}");
                await AwaitableCoroutine.Yield();
            }

            counter.Inc();
            Log($"Count: {counter.Count}");
            // inc 4
        }

        private async AwaitableCoroutine GetCoroutine2(Counter counter)
        {
            counter.Inc();
            Log($"Count: {counter.Count}");
            await AwaitableCoroutine.Yield(); // inc 1

            counter.Inc();
            Log($"Count: {counter.Count}");

            await GetCoroutine(counter); // inc 6

            counter.Inc();
            Log($"Count: {counter.Count}");

            await GetCoroutine(counter); // inc 11

            counter.Inc();
            Log($"Count: {counter.Count}");
            // inc 12
        }

        [Fact]
        public void CreateCoroutine()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();

            _ = runner.AddCoroutine(() => GetCoroutine(counter));
        }

        [Fact]
        public void RunCoroutine()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();

            _ = runner.AddCoroutine(() => GetCoroutine(counter));

            var i = 0;
            while (i < 3)
            {
                runner.Update();
                i++;
                Log($"Actual: {i}, Coroutines: {runner.Count}");
                Assert.Equal(i, counter.Count);
            }
        }

        [Fact]
        public void RunCoroutineInside()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();

            _ = runner.AddCoroutine(() => GetCoroutine2(counter));

            var i = 0;
            while (i < 12)
            {
                runner.Update();
                i++;
                Log($"Actual: {i}, Coroutines: {runner.Count}");
                Assert.Equal(i, counter.Count);
            }

            runner.Update();
            Log($"Actual: {i}, Coroutines: {runner.Count}");
        }
    }
}
