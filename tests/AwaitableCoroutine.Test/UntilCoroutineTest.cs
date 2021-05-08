using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class UntilCoroutineTest : TestTemplate
    {
        public UntilCoroutineTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        [Fact]
        public void RunUntilCoroutine()
        {
            var runner = new CoroutineRunner();

            var condition = false;

            var coroutine = runner.AddCoroutine(() => AwaitableCoroutine.Until(() => condition));

            for (var i = 0; i < 3; i++)
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
