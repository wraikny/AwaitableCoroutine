using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class AwaitTest
    {
        private readonly ITestOutputHelper _outputHelper;

        public AwaitTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            Internal.Logger.SetLogger(text =>
            {
                try
                {
                    _outputHelper.WriteLine(text);
                }
                catch
                {

                }
            });
        }

        public sealed class Counter
        {
            public int Count { get; private set; }

            public void Inc() => Count++;
        }

        public async AwaitableCoroutine GetCoroutine(Counter counter)
        {
            for (var i = 0; i < 3; i++)
            {
                counter.Inc();
                _outputHelper.WriteLine($"Count: {counter.Count}");
                await AwaitableCoroutine.Yield();
            }

            counter.Inc();
            _outputHelper.WriteLine($"Count: {counter.Count}");
            // inc 4
        }

        public async AwaitableCoroutine GetCoroutine2(Counter counter)
        {
            counter.Inc();
            _outputHelper.WriteLine($"Count: {counter.Count}");
            await AwaitableCoroutine.Yield(); // inc 1

            counter.Inc();
            _outputHelper.WriteLine($"Count: {counter.Count}");

            await GetCoroutine(counter); // inc 6

            counter.Inc();
            _outputHelper.WriteLine($"Count: {counter.Count}");

            await GetCoroutine(counter); // inc 11

            counter.Inc();
            _outputHelper.WriteLine($"Count: {counter.Count}");
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
                _outputHelper.WriteLine($"Actual: {i}, Coroutines: {runner.Count}");
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
                _outputHelper.WriteLine($"Actual: {i}, Coroutines: {runner.Count}");
                Assert.Equal(i, counter.Count);
            }

            runner.Update();
            _outputHelper.WriteLine($"Actual: {i}, Coroutines: {runner.Count}");
        }
    }
}
