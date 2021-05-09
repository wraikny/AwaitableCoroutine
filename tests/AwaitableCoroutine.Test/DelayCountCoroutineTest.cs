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
        public void RunDelayCoroutineInt()
        {
            var runner = new CoroutineRunner();

            var count = 5;

            var coroutine = runner.Context(() => AwaitableCoroutine.DelayCount(count));

            for (var i = 0; i < count; i++)
            {
                Assert.False(coroutine.IsCompleted);
                runner.Update();
            }

            Assert.True(coroutine.IsCompleted);
        }
    }
}
