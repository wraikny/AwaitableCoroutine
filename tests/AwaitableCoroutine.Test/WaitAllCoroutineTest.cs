using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class WaitAllCoroutineTest
    {
        private readonly ITestOutputHelper _outputHelper;

        public WaitAllCoroutineTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            Internal.Logger.SetLogger(_outputHelper.WriteLine);
        }

        [Fact]
        public void WaitAllOfYieldTest()
        {
            var runner = new CoroutineRunner();

            var waitAll = runner.AddCoroutine(() =>
                AwaitableCoroutine.WaitAll(new AwaitableCoroutineBase[] {
                    AwaitableCoroutine.Yield(),
                    AwaitableCoroutine.Yield(),
                    AwaitableCoroutine.Yield(),
                    AwaitableCoroutine.Yield(),
                })
            );

            Assert.False(waitAll.IsCompleted);

            runner.Update();
            Assert.True(waitAll.IsCompleted);
        }
    }
}
