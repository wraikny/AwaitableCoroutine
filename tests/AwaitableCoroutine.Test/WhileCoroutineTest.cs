using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class WhileCoroutineTest : TestTemplate
    {
        public WhileCoroutineTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        [Fact]
        public void RunWhileCoroutine()
        {
            var runner = new CoroutineRunner();

            var condition = false;

            var coroutine = runner.Context(() => AwaitableCoroutine.While(() => !condition));

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
