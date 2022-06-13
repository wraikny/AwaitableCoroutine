
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
                    Log("try B 1");
                    await Coroutine.Yield();
                    Log("try B 2");
                    throw new System.InvalidOperationException();
                }
                finally
                {
                    Log("finally B");
                    finallyIsCalledB = true;
                }

                Log("after finally B");
            });

            var coA = runner.Create(async () =>
            {
                try
                {
                    Log("try A");
                    await coB;
                    Log("try A 2");
                }
                finally
                {
                    Log("finally A");
                    finallyIsCalledA = true;
                }

                Log("after finally A");

                await Coroutine.Yield();

                Log("should not be called");

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

            Assert.True(finallyIsCalledB);
            Assert.True(finallyIsCalledA);

            Assert.False(shouldNotBeCalled);

            Assert.Equal(CoroutineStatus.Faulted, coB.Status);
            Assert.Equal(CoroutineStatus.Canceled, coA.Status);
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

            Assert.Equal(CoroutineStatus.Canceled, coB.Status);
            Assert.Equal(CoroutineStatus.Canceled, coA.Status);
        }
    }
}
