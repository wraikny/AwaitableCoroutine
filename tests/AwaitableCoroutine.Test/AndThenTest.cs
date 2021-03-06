using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class AndThenTest : TestTemplate
    {

        public AndThenTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        [Fact]
        public void RunAndThen()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();
            var c = runner.Create(() =>
                Coroutine.DelayCount(2)
                    .AndThen(() => Coroutine.DelayCount(2).OnCompleted(() =>
                    {
                        counter.Inc();
                        Log($"Count: {counter.Count}");
                    }))
            );

            while (!c.IsCompletedSuccessfully) runner.Update();

            Assert.True(c.IsCompletedSuccessfully);

            Assert.Equal(1, counter.Count);
        }

        private async Coroutine CreateCoroutine(int target, Counter counter)
        {
            var count = 0;
            while (true)
            {
                count++;
                counter.Inc();
                if (count >= target) return;
                await Coroutine.Yield();
            }
        }

        [Fact]
        public void RunAndThenWithLoop()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();

            var target = 5;

            var c = runner.Create(() => CreateCoroutine(target, counter));

            while (!c.IsCompletedSuccessfully) runner.Update();

            Assert.Equal(target, counter.Count);
        }
    }
}
