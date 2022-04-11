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

            var delay = runner.Create(() => Coroutine.DelayCount(5));

            var co = runner.Create(async () =>
            {
                await delay;
            });

            Assert.False(delay.IsCompletedSuccessfully);
            Assert.False(delay.IsCanceled);

            Assert.False(co.IsCompletedSuccessfully);
            Assert.False(co.IsCanceled);

            runner.Update();

            Assert.False(delay.IsCompletedSuccessfully);
            Assert.False(delay.IsCanceled);

            Assert.False(co.IsCompletedSuccessfully);
            Assert.False(co.IsCanceled);

            delay.Cancel();

            Assert.False(delay.IsCompletedSuccessfully);
            Assert.True(delay.IsCanceled);

            Assert.False(co.IsCompletedSuccessfully);
            Assert.True(co.IsCanceled);
        }

        [Fact]
        public void CancelWithCanceledException()
        {
            var runner = new CoroutineRunner();

            var delay = runner.Create(() => Coroutine.DelayCount(5));

            var co = runner.Context<Coroutine>(() => Coroutine.WaitAll(delay, delay));

            Assert.False(delay.IsCompletedSuccessfully);
            Assert.False(delay.IsCanceled);

            Assert.False(co.IsCompletedSuccessfully);
            Assert.False(co.IsCanceled);

            runner.Update();

            Assert.False(delay.IsCompletedSuccessfully);
            Assert.False(delay.IsCanceled);

            Assert.False(co.IsCompletedSuccessfully);
            Assert.False(co.IsCanceled);

            delay.Cancel();

            Assert.False(delay.IsCompletedSuccessfully);
            Assert.True(delay.IsCanceled);

            Assert.False(co.IsCompletedSuccessfully);
            Assert.False(co.IsCanceled);

            runner.Update();

            Assert.False(co.IsCompletedSuccessfully);
            Assert.True(co.IsCanceled);
            Assert.True(co.Exception is ChildCanceledException<CoroutineBase>);
        }
    }
}
