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

            var target = 5;

            var coroutine = runner.AddCoroutine(() => AwaitableCoroutine.Delay(target, () => 1));

            for (var i = 0; i < target; i++)
            {
                runner.Update();
                Assert.False(coroutine.IsCompleted);
            }

            runner.Update();
            Assert.True(coroutine.IsCompleted);
        }

        [Fact]
        public void RunDelayCoroutineFloat()
        {
            var runner = new CoroutineRunner();

            var target = 5f;

            var coroutine = runner.AddCoroutine(() => AwaitableCoroutine.Delay(target, () => 1f));

            for (var i = 0f; i < target; i++)
            {
                runner.Update();
                Assert.False(coroutine.IsCompleted);
            }

            runner.Update();
            Assert.True(coroutine.IsCompleted);
        }

        [Fact]
        public void RunDelayCoroutineDouble()
        {
            var runner = new CoroutineRunner();

            var target = 5.0;

            var coroutine = runner.AddCoroutine(() => AwaitableCoroutine.Delay(target, () => 1.0));

            for (var i = 0.0; i < target; i++)
            {
                runner.Update();
                Assert.False(coroutine.IsCompleted);
            }

            runner.Update();
            Assert.True(coroutine.IsCompleted);
        }
    }
}
