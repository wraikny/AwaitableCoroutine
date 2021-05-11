using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class LambdaCoroutineTest : TestTemplate
    {
        public LambdaCoroutineTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        [Fact]
        public void CreateCoroutineTest()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();

            var co = runner.Context<AwaitableCoroutine>(async () =>
            {
                counter.Inc();
            });

            Assert.False(co.IsCompleted);
            Assert.Equal(0, counter.Count);

            runner.Update();
            Assert.Equal(1, counter.Count);
            Assert.True(co.IsCompleted);
        }

        [Fact]
        public void AwaitYieldTest()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();

            var co = runner.Context<AwaitableCoroutine>(async () =>
            {
                counter.Inc();
                await AwaitableCoroutine.Yield();
                counter.Inc();
            });

            Assert.False(co.IsCompleted);
            Assert.Equal(0, counter.Count);
            Assert.False(co.IsCompleted);

            runner.Update();
            Assert.Equal(1, counter.Count);
            Assert.False(co.IsCompleted);

            runner.Update();
            Assert.Equal(2, counter.Count);
            Assert.True(co.IsCompleted);
        }

        [Fact]
        public void AwaitForLoop()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();

            var co = runner.Context<AwaitableCoroutine>(async () =>
            {
                for (var i = 0; i < 5; i++)
                {
                    counter.Inc();
                    await AwaitableCoroutine.Yield();
                }
                counter.Inc();
            });

            Assert.False(co.IsCompleted);
            Assert.Equal(0, counter.Count);
            Assert.False(co.IsCompleted);

            for (var i = 1; i <= 5; i++)
            {
                runner.Update();
                Assert.False(co.IsCompleted);
                Assert.Equal(i, counter.Count);
                Assert.False(co.IsCompleted);
            }

            runner.Update();
            Assert.Equal(6, counter.Count);
            Assert.True(co.IsCompleted);
        }

        [Fact]
        public void AwaitDelayCountTest()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();

            var co = runner.Context<AwaitableCoroutine>(async () =>
            {
                counter.Inc();
                await AwaitableCoroutine.DelayCount(5);
                counter.Inc();
            });

            Assert.False(co.IsCompleted);
            Assert.Equal(0, counter.Count);
            Assert.False(co.IsCompleted);

            runner.Update();
            Assert.Equal(1, counter.Count);
            Assert.False(co.IsCompleted);

            for (var i = 0; i < 5; i++)
            {
                runner.Update();
                Assert.False(co.IsCompleted);
                Assert.Equal(1, counter.Count);
                Assert.False(co.IsCompleted);
            }

            runner.Update();
            Assert.Equal(2, counter.Count);
            Assert.True(co.IsCompleted);
        }


    }
}
