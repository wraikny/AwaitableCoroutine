using System;

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

            var waitAny = runner.Context(() =>
                AwaitableCoroutine.WaitAny(new AwaitableCoroutineBase[] {
                    AwaitableCoroutine.Until(() => false),
                    AwaitableCoroutine.Until(() => false),
                    AwaitableCoroutine.Until(() => false),
                    AwaitableCoroutine.Until(() => flag),
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

            var waitAny = runner.Context(() =>
                AwaitableCoroutine.WaitAny(
                    AwaitableCoroutine.Until(() => false),
                    AwaitableCoroutine.Until(() => flag)
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

            var waitAny = runner.Context(() =>
            {
                return AwaitableCoroutine.WaitAny<int>(new AwaitableCoroutine<int>[] {
                    AwaitableCoroutine.Until(() => false, () => 0),
                    AwaitableCoroutine.Until(() => flag, () => 1),
                    AwaitableCoroutine.Until(() => false, () => 2),
                    AwaitableCoroutine.Until(() => flag, () => 3),
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
