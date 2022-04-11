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

        private sealed class CustomCoroutine : Coroutine
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

            var c = runner.Create(() => new CustomCoroutine());

            Assert.False(c.IsCompletedSuccessfully);

            runner.Update();

            Assert.True(c.IsCompletedSuccessfully);
        }
    }
}
