using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class UntilCompletedCoroutineTest : TestTemplate
    {
        public UntilCompletedCoroutineTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        [Fact]
        public void RunUntilCompleted()
        {
            var runner1 = new CoroutineRunner();
            var runner2 = new CoroutineRunner();

            var counter = new Counter();

            var coroutine1 = runner1.Create(() => Coroutine.DelayCount(3));
            var coroutine2 = runner2.Create(() => coroutine1.UntilCompleted(counter.Inc));

            Assert.False(coroutine1.IsCompletedSuccessfully);
            Assert.False(coroutine2.IsCompletedSuccessfully);

            runner2.Update();
            Assert.Equal(1, counter.Count);

            runner2.Update();
            Assert.Equal(2, counter.Count);

            for (var i = 0; i < 3; i++)
            {
                runner1.Update();
                Assert.Equal(2, counter.Count);
                Assert.False(coroutine1.IsCompletedSuccessfully);
                Assert.False(coroutine2.IsCompletedSuccessfully);
            }

            runner2.Update();
            Assert.Equal(3, counter.Count);

            runner1.Update();
            Assert.True(coroutine1.IsCompletedSuccessfully);
            Assert.False(coroutine2.IsCompletedSuccessfully);

            runner2.Update();
            Assert.True(coroutine2.IsCompletedSuccessfully);
            Assert.Equal(3, counter.Count);
        }
    }
}
