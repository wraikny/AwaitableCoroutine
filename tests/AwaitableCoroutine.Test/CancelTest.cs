using System;

using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class CancelTest : TestTemplate
    {
        public CancelTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        [Fact]
        public void Cancel()
        {
            var runner = new CoroutineRunner();

            var delay = runner.Create(() => Coroutine.DelayCount(5));

            var co = runner.Create(async () =>
            {
                await delay;
            });

            Assert.False(delay.IsCompletedSuccessfully);
            Assert.False(delay.IsCanceled);

            Assert.False(co.IsCompletedSuccessfully);
            Assert.False(co.IsCanceled);

            runner.Update();

            Assert.False(delay.IsCompletedSuccessfully);
            Assert.False(delay.IsCanceled);

            Assert.False(co.IsCompletedSuccessfully);
            Assert.False(co.IsCanceled);

            delay.Cancel();

            Assert.False(delay.IsCompletedSuccessfully);
            Assert.True(delay.IsCanceled);

            Assert.False(co.IsCompletedSuccessfully);
            Assert.True(co.IsCanceled);
        }

        [Fact]
        public void CancelWithCanceledException()
        {
            var runner = new CoroutineRunner();

            var delay = runner.Create(() => Coroutine.DelayCount(5));

            var co = runner.Context<Coroutine>(() => Coroutine.WaitAll(delay, delay));

            Assert.False(delay.IsCompletedSuccessfully);
            Assert.False(delay.IsCanceled);

            Assert.False(co.IsCompletedSuccessfully);
            Assert.False(co.IsCanceled);

            runner.Update();

            Assert.False(delay.IsCompletedSuccessfully);
            Assert.False(delay.IsCanceled);

            Assert.False(co.IsCompletedSuccessfully);
            Assert.False(co.IsCanceled);

            delay.Cancel();

            Assert.False(delay.IsCompletedSuccessfully);
            Assert.True(delay.IsCanceled);

            Assert.False(co.IsCompletedSuccessfully);
            Assert.False(co.IsCanceled);

            runner.Update();

            Assert.False(co.IsCompletedSuccessfully);
            Assert.True(co.IsCanceled);
            Assert.True(co.Exception is ChildCanceledException);
        }

        [Fact]
        public void ExceptionFinallyTest()
        {
            var finallyIsCalledA = false;
            var finallyIsCalledB = false;
            var shouldNotBeCalled = false;

            var runner = new CoroutineRunner();

            var coB = runner.Create(async () =>
            {
                try
                {
                    await Coroutine.DelayCount(2);
                    throw new System.InvalidOperationException();
                }
                finally
                {
                    finallyIsCalledB = true;
                }
            });

            var coA = runner.Create(async () =>
            {
                try
                {
                    await coB;
                }
                catch (CanceledException e)
                {
                    Assert.True(e.InnerException is InvalidOperationException);
                    throw e;
                }
                finally
                {
                    finallyIsCalledA = true;
                }

                await Coroutine.Yield();

                shouldNotBeCalled = true;
            });

            while (!coA.IsCompleted)
            {
                try
                {
                    runner.Update();
                }
                catch (System.AggregateException es)
                {
                    foreach (var e in es.InnerExceptions)
                    {
                        Log($"{e.GetType()}: {e.Message}");
                    }
                }
                catch (System.Exception e)
                {
                    Log($"{e.GetType()}: {e.Message}");
                }
            }

            Assert.True(coA.IsCanceled);
            Assert.True(coB.IsCanceled);
            Assert.True(finallyIsCalledB);
            Assert.True(finallyIsCalledA);
            Assert.False(shouldNotBeCalled);
        }

        [Fact]
        public void CancelFinallyTest()
        {
            var finallyIsCalledA = false;

            var runner = new CoroutineRunner();

            var coB = runner.Create(async () =>
            {
                await Coroutine.Yield();
                await Coroutine.Yield();
            });

            var coA = runner.Create(async () =>
            {
                try
                {
                    Log("await");
                    await coB;
                }
                finally
                {
                    finallyIsCalledA = true;
                }
            });

            coB.Cancel();

            while (!coA.IsCompleted)
            {
                try
                {
                    Log("updating");
                    runner.Update();
                }
                catch (System.Exception e)
                {
                    Log(e.Message);
                }
                finally
                {
                    Log("updated");
                }
            }

            Assert.True(finallyIsCalledA);
        }
    }
}
