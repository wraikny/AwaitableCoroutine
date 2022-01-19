using System;
using System.Threading.Tasks;

using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class AwaitTaskTest : TestTemplate
    {
        public AwaitTaskTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }


        static async Task<T> CreateTask<T>(T x)
        {
            await Task.Yield();
            return x;
        }

        [Fact]
        private void AwaitCompletedTask()
        {
            var runner = new CoroutineRunner();

            var co = runner.Create(() => AwaitableCoroutine.AwaitTask(Task.CompletedTask));

            Assert.False(co.IsCompletedSuccessfully);

            runner.Update();
            Assert.True(co.IsCompletedSuccessfully);
        }

        [Fact]
        private async Task AwaitTask()
        {
            var runner = new CoroutineRunner();

            var task = CreateTask<int>(3);

            var co = runner.Create(() => AwaitableCoroutine.AwaitTask(task));

            Assert.False(co.IsCompletedSuccessfully);

            await Task.Yield();
            await Task.Yield();

            runner.Update();
            Assert.True(co.IsCompletedSuccessfully);
            Assert.Equal(3, co.Result);
        }
    }
}
