using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class SelectCoroutineTest : TestTemplate
    {
        public SelectCoroutineTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        [Fact]
        public void RunSelect()
        {
            var runner = new CoroutineRunner();

            var coroutine = runner.Context(() => AwaitableCoroutine.Yield().SelectTo(2));

            Assert.False(coroutine.IsCompleted);

            runner.Update();
            Assert.True(coroutine.IsCompleted);
            Assert.Equal(2, coroutine.Result);
        }
    }
}
