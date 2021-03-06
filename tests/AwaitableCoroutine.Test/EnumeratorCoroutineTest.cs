using System.Collections;

using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class EnumeratorCoroutineTest : TestTemplate
    {
        public EnumeratorCoroutineTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        private IEnumerator GetEnumerator(int n)
        {
            for (var i = 0; i < n; i++)
            {
                Log($"{i}");
                yield return null;
            }
        }

        [Fact]
        public void RunEnumeratorCoroutine()
        {
            var runner = new CoroutineRunner();

            var n = 5;
            var coroutine = runner.Create(() => Coroutine.FromEnumerator(GetEnumerator(n)));

            for (var i = 0; i < n; i++)
            {
                runner.Update();
                Assert.False(coroutine.IsCompletedSuccessfully);
            }

            runner.Update();
            Assert.True(coroutine.IsCompletedSuccessfully);
        }
    }
}
