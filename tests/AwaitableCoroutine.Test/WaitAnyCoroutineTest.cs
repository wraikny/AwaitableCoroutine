
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
                Coroutine.WaitAny(new CoroutineBase[] {
                    Coroutine.While(() => true),
                    Coroutine.While(() => true),
                    Coroutine.While(() => true),
                    Coroutine.While(() => !flag),
                })
            );

            Assert.False(waitAny.IsCompletedSuccessfully);

            runner.Update();
            Assert.False(waitAny.IsCompletedSuccessfully);

            flag = true;

            runner.Update();
            Assert.True(waitAny.IsCompletedSuccessfully);
        }

        [Fact]
        public void WaitAny2Test()
        {
            var runner = new CoroutineRunner();

            var flag = false;

            var waitAny = runner.Create(() =>
                Coroutine.WaitAny(
                    Coroutine.While(() => true),
                    Coroutine.While(() => !flag)
                )
            );

            Assert.False(waitAny.IsCompletedSuccessfully);

            runner.Update();
            Assert.False(waitAny.IsCompletedSuccessfully);

            flag = true;

            runner.Update();
            Assert.True(waitAny.IsCompletedSuccessfully);
        }

        [Fact]
        public void WaitAnyWithValuesTest()
        {
            var runner = new CoroutineRunner();

            var flag = false;

            var waitAny = runner.Create(() =>
            {
                return Coroutine.WaitAny<int>(new Coroutine<int>[] {
                    Coroutine.While(() => true).SelectTo(0),
                    Coroutine.While(() => !flag).SelectTo(1),
                    Coroutine.While(() => true).SelectTo(2),
                    Coroutine.While(() => !flag).SelectTo(3),
                });
            });

            Assert.False(waitAny.IsCompletedSuccessfully);

            runner.Update();
            Assert.False(waitAny.IsCompletedSuccessfully);

            flag = true;

            runner.Update();

            Assert.True(waitAny.IsCompletedSuccessfully);

            var res = waitAny.Result;
            Assert.Equal(1, res);
        }
    }
}
