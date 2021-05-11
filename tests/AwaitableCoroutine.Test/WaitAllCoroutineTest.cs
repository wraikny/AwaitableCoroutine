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
        public void WaitAllOfDelay0Test()
        {
            var runner = new CoroutineRunner();

            var waitAll = runner.Create(() =>
                AwaitableCoroutine.WaitAll(new AwaitableCoroutineBase[] {
                    AwaitableCoroutine.DelayCount(0),
                    AwaitableCoroutine.DelayCount(0),
                    AwaitableCoroutine.DelayCount(0),
                    AwaitableCoroutine.DelayCount(0),
                })
            );

            Assert.False(waitAll.IsCompleted);

            runner.Update();
            Assert.True(waitAll.IsCompleted);
        }

        [Fact]
        public void WaitAllOfDelayTest()
        {
            var runner = new CoroutineRunner();

            var waitAll = runner.Create(() =>
                AwaitableCoroutine.WaitAll(new AwaitableCoroutineBase[] {
                    AwaitableCoroutine.DelayCount(1),
                    AwaitableCoroutine.DelayCount(2),
                    AwaitableCoroutine.DelayCount(3),
                    AwaitableCoroutine.DelayCount(4),
                })
            );

            Assert.False(waitAll.IsCompleted);

            for (var i = 0; i < 4; i++)
            {
                runner.Update();
            }
            runner.Update();
            Assert.True(waitAll.IsCompleted);
        }

        [Fact]
        public void WaitAll2Test()
        {
            var runner = new CoroutineRunner();

            var waitAll = runner.Create(() =>
                AwaitableCoroutine.WaitAll(
                    AwaitableCoroutine.DelayCount(0),
                    AwaitableCoroutine.DelayCount(0)
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

            var waitAll = runner.Create(() =>
            {
                var coroutines = new AwaitableCoroutine<int>[4];
                for (var i = 0; i < 4; i++)
                {
                    coroutines[i] = AwaitableCoroutine.DelayCount(0).SelectTo(i);
                }
                return AwaitableCoroutine.WaitAll<int>(coroutines);
            });

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
