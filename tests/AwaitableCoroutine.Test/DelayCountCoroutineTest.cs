using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class DelayCoroutineTest
    {
        private readonly ITestOutputHelper _outputHelper;

        public DelayCoroutineTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            Internal.Logger.SetLogger(text =>
            {
                try
                {
                    _outputHelper.WriteLine(text);
                }
                catch
                {

                }
            });
        }

        [Fact]
        public void RunDelayCoroutineInt()
        {
            var runner = new CoroutineRunner();

            var count = 5;

            var coroutine = runner.AddCoroutine(() => AwaitableCoroutine.DelayCount(count));

            for (var i = 0; i < count; i++)
            {
                runner.Update();
                Assert.False(coroutine.IsCompleted);
            }

            runner.Update();
            Assert.True(coroutine.IsCompleted);
        }
    }
}
