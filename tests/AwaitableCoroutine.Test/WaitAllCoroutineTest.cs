using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class WaitAllCoroutineTest : TestTemplate
    {
        public WaitAllCoroutineTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

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
        public void WaitAll2Test()
        {
            var runner = new CoroutineRunner();

            var waitAll = runner.AddCoroutine(() =>
                AwaitableCoroutine.WaitAll(
                    AwaitableCoroutine.Yield(),
                    AwaitableCoroutine.Yield()
                )
            );

            Assert.False(waitAll.IsCompleted);

            // completes children
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
                    coroutines[i] = AwaitableCoroutine.Return(i);
                }
                return AwaitableCoroutine.WaitAll<int>(coroutines);
            });

            Assert.False(waitAll.IsCompleted);

            while (!waitAll.IsCompleted)
            {
                runner.Update();
            }

            var res = waitAll.Result;
            Assert.NotNull(res);

            for (var i = 0; i < 4; i++)
            {
                Assert.Equal(i, res[i]);
            }
        }
    }
}
