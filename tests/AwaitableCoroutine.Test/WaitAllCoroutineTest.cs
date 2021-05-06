using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class WaitAllCoroutineTest
    {
        private readonly ITestOutputHelper _outputHelper;

        public WaitAllCoroutineTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            Internal.Logger.SetLogger(text =>
            {
                try
                {
                    _outputHelper.WriteLine(text);
                }
                catch
                {

                }
            });
        }

        private async AwaitableCoroutine<int> CreateCoroutine(int v)
        {
            await AwaitableCoroutine.Yield();
            return v;
        }

        [Fact]
        public void WaitAllOfYieldTest()
        {
            var runner = new CoroutineRunner();

            var waitAll = runner.AddCoroutine(() =>
                AwaitableCoroutine.WaitAll(new AwaitableCoroutineBase[] {
                    AwaitableCoroutine.Yield(),
                    AwaitableCoroutine.Yield(),
                    AwaitableCoroutine.Yield(),
                    AwaitableCoroutine.Yield(),
                })
            );

            Assert.False(waitAll.IsCompleted);

            runner.Update();
            Assert.True(waitAll.IsCompleted);
        }

        [Fact]
        public void WaitAllWithValuesTest()
        {
            var runner = new CoroutineRunner();

            var waitAll = runner.AddCoroutine(() =>
            {
                var coroutines = new AwaitableCoroutine<int>[4];
                for (var i = 0; i < 4; i++)
                {
                    coroutines[i] = CreateCoroutine(i);
                }
                return AwaitableCoroutine.WaitAll<int>(coroutines);
            });

            Assert.False(waitAll.IsCompleted);

            runner.Update();
            Assert.False(waitAll.IsCompleted);

            runner.Update();
            Assert.True(waitAll.IsCompleted);

            var res = waitAll.Result;
            Assert.NotNull(res);

            for (var i = 0; i < 4; i++)
            {
                Assert.Equal(i, res[i]);
            }
        }
    }
}
