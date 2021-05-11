using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class CancelTest : TestTemplate
    {
        public CancelTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        [Fact]
        public void Cancel()
        {
            var runner = new CoroutineRunner();

            var delay = runner.Context(() => AwaitableCoroutine.DelayCount(5));

            var co = runner.Context<AwaitableCoroutine>(async () =>
            {
                await delay;
            });

            Assert.False(delay.IsCompleted);
            Assert.False(delay.IsCanceled);

            Assert.False(co.IsCompleted);
            Assert.False(co.IsCanceled);

            runner.Update();

            Assert.False(delay.IsCompleted);
            Assert.False(delay.IsCanceled);

            Assert.False(co.IsCompleted);
            Assert.False(co.IsCanceled);

            delay.Cancel();

            Assert.False(delay.IsCompleted);
            Assert.True(delay.IsCanceled);

            Assert.False(co.IsCompleted);
            Assert.True(co.IsCanceled);
        }

        [Fact]
        public void CancelWithCanceledException()
        {
            var runner = new CoroutineRunner();

            var delay = runner.Context(() => AwaitableCoroutine.DelayCount(5));

            var co = runner.Context<AwaitableCoroutine>(() => AwaitableCoroutine.WaitAll(delay, delay));

            Assert.False(delay.IsCompleted);
            Assert.False(delay.IsCanceled);

            Assert.False(co.IsCompleted);
            Assert.False(co.IsCanceled);

            runner.Update();

            Assert.False(delay.IsCompleted);
            Assert.False(delay.IsCanceled);

            Assert.False(co.IsCompleted);
            Assert.False(co.IsCanceled);

            delay.Cancel();

            Assert.False(delay.IsCompleted);
            Assert.True(delay.IsCanceled);

            Assert.False(co.IsCompleted);
            Assert.False(co.IsCanceled);

            runner.Update();

            Assert.False(co.IsCompleted);
            Assert.True(co.IsCanceled);
            Assert.True(co.Exception is ChildCanceledException<AwaitableCoroutineBase>);
        }
    }
}
