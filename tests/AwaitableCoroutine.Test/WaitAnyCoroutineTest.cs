using System;

using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class WaitAnyCoroutineTest
    {
        private readonly ITestOutputHelper _outputHelper;

        public WaitAnyCoroutineTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            Internal.Logger.SetLogger(_outputHelper.WriteLine);
        }

        [Fact]
        public void WaitAllOfYieldTest()
        {
            var runner = new CoroutineRunner();

            var condition = false;

            var waitAll = runner.AddCoroutine(() =>
                AwaitableCoroutine.WaitAny(new AwaitableCoroutineBase[] {
                    AwaitableCoroutine.Until(() => false),
                    AwaitableCoroutine.Until(() => false),
                    AwaitableCoroutine.Until(() => false),
                    AwaitableCoroutine.Until(() => condition),
                })
            );

            Assert.False(waitAll.IsCompleted);

            for (var i = 0; i < 5; i++)
            {
                runner.Update();
                Assert.False(waitAll.IsCompleted);
            }

            condition = true;
            runner.Update();
            Assert.True(waitAll.IsCompleted);
        }

        private async AwaitableCoroutine<int> CreateCoroutine(int v, Func<bool> predicate)
        {
            await AwaitableCoroutine.Until(predicate);
            return v;
        }

        [Fact]
        public void WaitAnyWithValuesTest()
        {
            var runner = new CoroutineRunner();

            var condition = false;

            var waitAny = runner.AddCoroutine(() =>
            {
                return AwaitableCoroutine.WaitAny<int>(new AwaitableCoroutine<int>[] {
                    CreateCoroutine(0, () => false),
                    CreateCoroutine(1, () => condition),
                    CreateCoroutine(2, () => false),
                    CreateCoroutine(3, () => condition),
                });
            });

            Assert.False(waitAny.IsCompleted);

            runner.Update();
            Assert.False(waitAny.IsCompleted);


            condition = true;
            runner.Update();
            Assert.False(waitAny.IsCompleted);

            runner.Update();
            Assert.True(waitAny.IsCompleted);

            var res = waitAny.Result;
            Assert.NotNull(res);

            Assert.Equal(1, res[0]);
            Assert.Equal(3, res[1]);
        }
    }
}
