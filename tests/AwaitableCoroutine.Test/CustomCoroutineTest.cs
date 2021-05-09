using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class CustomCoroutineTest : TestTemplate
    {
        public CustomCoroutineTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        private sealed class CustomCoroutine : AwaitableCoroutine
        {
            public CustomCoroutine() { }

            protected override void OnMoveNext()
            {
                Complete();
            }
        }

        [Fact]
        public void RunCustomCoroutine()
        {
            var runner = new CoroutineRunner();

            var c = runner.Context(() => new CustomCoroutine());

            Assert.False(c.IsCompleted);

            runner.Update();

            Assert.True(c.IsCompleted);
        }
    }
}
