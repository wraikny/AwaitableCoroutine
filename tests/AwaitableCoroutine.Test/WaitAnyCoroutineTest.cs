using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class WaitAnyCoroutineTest
    {
        private readonly ITestOutputHelper _outputHelper;

        public WaitAnyCoroutineTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            Internal.Logger.SetLogger(_outputHelper.WriteLine);
        }

        [Fact]
        public void WaitAllOfYieldTest()
        {
            var runner = new CoroutineRunner();

            var condition = false;

            var waitAll = runner.AddCoroutine(() =>
                AwaitableCoroutine.WaitAny(new AwaitableCoroutineBase[] {
                    AwaitableCoroutine.Until(() => false),
                    AwaitableCoroutine.Until(() => false),
                    AwaitableCoroutine.Until(() => false),
                    AwaitableCoroutine.Until(() => condition),
                })
            );

            Assert.False(waitAll.IsCompleted);

            for (var i = 0; i < 5; i++)
            {
                runner.Update();
                Assert.False(waitAll.IsCompleted);
            }

            condition = true;
            runner.Update();
            Assert.True(waitAll.IsCompleted);
        }
    }
}
