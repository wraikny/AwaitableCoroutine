using System.Collections;

using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class EnumeratorCoroutineTest
    {
        private readonly ITestOutputHelper _outputHelper;

        public EnumeratorCoroutineTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            Internal.Logger.SetLogger(_outputHelper.WriteLine);
        }

        private IEnumerator GetEnumerator(int n)
        {
            for (var i = 0; i < n; i++)
            {
                _outputHelper.WriteLine($"{i}");
                yield return null;
            }
        }

        [Fact]
        public void RunEnumeratorCoroutine()
        {
            var runner = new CoroutineRunner();

            var n = 5;
            var coroutine = runner.AddCoroutine(() => GetEnumerator(n).ToAwaitableCoroutine());

            for (var i = 0; i < n; i++)
            {
                runner.Update();
                Assert.False(coroutine.IsCompleted);
            }

            runner.Update();
            Assert.True(coroutine.IsCompleted);
        }
    }
}
