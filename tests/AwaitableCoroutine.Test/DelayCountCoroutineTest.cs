using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class DelayCoroutineTest : TestTemplate
    {
        public DelayCoroutineTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        [Fact]
        public void RunDelayCount()
        {
            var runner = new CoroutineRunner();

            var count = 5;

            var flag = false;

            var coroutine = runner.Create(() => Coroutine.DelayCount(count).OnCompleted(() => flag = true));

            for (var i = 0; i < count; i++)
            {
                runner.Update();
                Assert.False(coroutine.IsCompletedSuccessfully);
            }

            runner.Update();
            Assert.True(coroutine.IsCompletedSuccessfully);
            Assert.True(flag);
        }

        private async Coroutine CreateAwaitDelayCount(Counter counter)
        {
            counter.Inc();
            await Coroutine.DelayCount(5);
            counter.Inc();
            await Coroutine.DelayCount(5);
            counter.Inc();
        }

        [Fact]
        public void RunAwaitDelayCount()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();

            var co = runner.Create(() => CreateAwaitDelayCount(counter));

            Assert.False(co.IsCompletedSuccessfully);

            runner.Update();
            Assert.Equal(1, counter.Count);

            for (var i = 0; i < 5; i++) runner.Update();

            runner.Update();
            Assert.Equal(2, counter.Count);

            for (var i = 0; i < 5; i++) runner.Update();

            runner.Update();
            Assert.Equal(3, counter.Count);

            Assert.True(co.IsCompletedSuccessfully);
        }
    }
}
