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

#pragma warning disable CS1998
            var co = runner.Create(async () =>
            {
                counter.Inc();
            });
#pragma warning restore CS1998

            Assert.False(co.IsCompletedSuccessfully);
            Assert.Equal(0, counter.Count);

            runner.Update();
            Assert.Equal(1, counter.Count);
            Assert.True(co.IsCompletedSuccessfully);
        }

        [Fact]
        public void AwaitYieldTest()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();

            var co = runner.Create(async () =>
            {
                counter.Inc();
                await Coroutine.Yield();
                counter.Inc();
            });

            Assert.False(co.IsCompletedSuccessfully);
            Assert.Equal(0, counter.Count);
            Assert.False(co.IsCompletedSuccessfully);

            runner.Update();
            Assert.Equal(1, counter.Count);
            Assert.False(co.IsCompletedSuccessfully);

            runner.Update();
            Assert.Equal(2, counter.Count);
            Assert.True(co.IsCompletedSuccessfully);
        }

        [Fact]
        public void AwaitForLoop()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();

            var co = runner.Create(async () =>
            {
                for (var i = 0; i < 5; i++)
                {
                    counter.Inc();
                    await Coroutine.Yield();
                }
                counter.Inc();
            });

            Assert.False(co.IsCompletedSuccessfully);
            Assert.Equal(0, counter.Count);
            Assert.False(co.IsCompletedSuccessfully);

            for (var i = 1; i <= 5; i++)
            {
                runner.Update();
                Assert.False(co.IsCompletedSuccessfully);
                Assert.Equal(i, counter.Count);
                Assert.False(co.IsCompletedSuccessfully);
            }

            runner.Update();
            Assert.Equal(6, counter.Count);
            Assert.True(co.IsCompletedSuccessfully);
        }

        [Fact]
        public void AwaitDelayCountTest()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();

            var co = runner.Create(async () =>
            {
                counter.Inc();
                await Coroutine.DelayCount(5);
                counter.Inc();
            });

            Assert.False(co.IsCompletedSuccessfully);
            Assert.Equal(0, counter.Count);
            Assert.False(co.IsCompletedSuccessfully);

            runner.Update();
            Assert.Equal(1, counter.Count);
            Assert.False(co.IsCompletedSuccessfully);

            for (var i = 0; i < 5; i++)
            {
                runner.Update();
                Assert.False(co.IsCompletedSuccessfully);
                Assert.Equal(1, counter.Count);
                Assert.False(co.IsCompletedSuccessfully);
            }

            runner.Update();
            Assert.Equal(2, counter.Count);
            Assert.True(co.IsCompletedSuccessfully);
        }


    }
}
