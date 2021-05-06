using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class UntilCoroutineTest
    {
        private readonly ITestOutputHelper _outputHelper;

        public UntilCoroutineTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            Internal.Logger.SetLogger(_outputHelper.WriteLine);
        }

        [Fact]
        public void RunUntilCoroutine()
        {
            var runner = new CoroutineRunner();

            var condition = false;

            var coroutine = runner.AddCoroutine(() => AwaitableCoroutine.Until(() => condition));

            for(var i = 0; i < 10; i++)
            {
                runner.Update();
                Assert.False(coroutine.IsCompleted);
            }

            condition = true;
            runner.Update();
            Assert.True(coroutine.IsCompleted);
        }
    }
}
