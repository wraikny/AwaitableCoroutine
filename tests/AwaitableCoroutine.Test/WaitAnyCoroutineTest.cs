
using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class WaitAnyCoroutineTest : TestTemplate
    {
        public WaitAnyCoroutineTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        [Fact]
        public void WaitAnyTest()
        {
            var runner = new CoroutineRunner();

            var flag = false;

            var waitAny = runner.Create(() =>
                AwaitableCoroutine.WaitAny(new AwaitableCoroutineBase[] {
                    AwaitableCoroutine.While(() => true),
                    AwaitableCoroutine.While(() => true),
                    AwaitableCoroutine.While(() => true),
                    AwaitableCoroutine.While(() => !flag),
                })
            );

            Assert.False(waitAny.IsCompleted);

            runner.Update();
            Assert.False(waitAny.IsCompleted);

            flag = true;

            runner.Update();
            Assert.True(waitAny.IsCompleted);
        }

        [Fact]
        public void WaitAny2Test()
        {
            var runner = new CoroutineRunner();

            var flag = false;

            var waitAny = runner.Create(() =>
                AwaitableCoroutine.WaitAny(
                    AwaitableCoroutine.While(() => true),
                    AwaitableCoroutine.While(() => !flag)
                )
            );

            Assert.False(waitAny.IsCompleted);

            runner.Update();
            Assert.False(waitAny.IsCompleted);

            flag = true;

            runner.Update();
            Assert.True(waitAny.IsCompleted);
        }

        [Fact]
        public void WaitAnyWithValuesTest()
        {
            var runner = new CoroutineRunner();

            var flag = false;

            var waitAny = runner.Create(() =>
            {
                return AwaitableCoroutine.WaitAny<int>(new AwaitableCoroutine<int>[] {
                    AwaitableCoroutine.While(() => true).SelectTo(0),
                    AwaitableCoroutine.While(() => !flag).SelectTo(1),
                    AwaitableCoroutine.While(() => true).SelectTo(2),
                    AwaitableCoroutine.While(() => !flag).SelectTo(3),
                });
            });

            Assert.False(waitAny.IsCompleted);

            runner.Update();
            Assert.False(waitAny.IsCompleted);

            flag = true;

            runner.Update();

            Assert.True(waitAny.IsCompleted);

            var res = waitAny.Result;
            Assert.Equal(1, res);
        }
    }
}
