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

        [Fact]
        public void WaitAnyTest()
        {
            var runner = new CoroutineRunner();

            var flag = false;

            var waitAny = runner.AddCoroutine(() =>
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

            var waitAny = runner.AddCoroutine(() =>
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

            var waitAny = runner.AddCoroutine(() =>
            {
                return AwaitableCoroutine.WaitAny<int>(new AwaitableCoroutine<int>[] {
                    AwaitableCoroutine.Until(() => false).SelectTo(0),
                    AwaitableCoroutine.Until(() => flag).SelectTo(1),
                    AwaitableCoroutine.Until(() => false).SelectTo(2),
                    AwaitableCoroutine.Until(() => flag).SelectTo(3),
                });
            });

            Assert.False(waitAny.IsCompleted);

            runner.Update();
            Assert.False(waitAny.IsCompleted);

            flag = true;

            while (!waitAny.IsCompleted)
            {
                runner.Update();
            }

            var res = waitAny.Result;
            Assert.Equal(1, res);
        }
    }
}
