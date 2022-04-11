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
                Coroutine.WaitAll(new CoroutineBase[] {
                    Coroutine.DelayCount(0),
                    Coroutine.DelayCount(0),
                    Coroutine.DelayCount(0),
                    Coroutine.DelayCount(0),
                })
            );

            Assert.False(waitAll.IsCompletedSuccessfully);

            runner.Update();
            Assert.True(waitAll.IsCompletedSuccessfully);
        }

        [Fact]
        public void WaitAllOfDelayTest()
        {
            var runner = new CoroutineRunner();

            var waitAll = runner.Create(() =>
                Coroutine.WaitAll(new CoroutineBase[] {
                    Coroutine.DelayCount(1),
                    Coroutine.DelayCount(2),
                    Coroutine.DelayCount(3),
                    Coroutine.DelayCount(4),
                })
            );

            Assert.False(waitAll.IsCompletedSuccessfully);

            for (var i = 0; i < 4; i++)
            {
                runner.Update();
            }
            runner.Update();
            Assert.True(waitAll.IsCompletedSuccessfully);
        }

        [Fact]
        public void WaitAll2Test()
        {
            var runner = new CoroutineRunner();

            var waitAll = runner.Create(() =>
                Coroutine.WaitAll(
                    Coroutine.DelayCount(0),
                    Coroutine.DelayCount(0)
                )
            );

            Assert.False(waitAll.IsCompletedSuccessfully);

            // completes children
            runner.Update();

            Assert.True(waitAll.IsCompletedSuccessfully);
        }

        [Fact]
        public void WaitAllWithValuesTest()
        {
            var runner = new CoroutineRunner();

            var waitAll = runner.Create(() =>
            {
                var coroutines = new Coroutine<int>[4];
                for (var i = 0; i < 4; i++)
                {
                    coroutines[i] = Coroutine.DelayCount(0).SelectTo(i);
                }
                return Coroutine.WaitAll<int>(coroutines);
            });

            Assert.False(waitAll.IsCompletedSuccessfully);

            runner.Update();

            Assert.True(waitAll.IsCompletedSuccessfully);

            var res = waitAll.Result;
            Assert.NotNull(res);

            for (var i = 0; i < 4; i++)
            {
                Assert.Equal(i, res[i]);
            }
        }
    }
}
