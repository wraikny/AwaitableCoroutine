using System.Threading.Tasks;

using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class AwaitTaskTest : TestTemplate
    {
        public AwaitTaskTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        private static async Task<T> CreateTask<T>(T x)
        {
            await Task.Yield();
            return x;
        }
    }
}
