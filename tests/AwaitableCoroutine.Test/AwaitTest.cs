using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class AwaitTest : TestTemplate
    {
        public AwaitTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        private async AwaitableCoroutine GetCoroutine(Counter counter)
        {
            for (var i = 0; i < 3; i++)
            {
                counter.Inc();
                Log($"Count: {counter.Count}");
                await AwaitableCoroutine.Yield();
            }

            counter.Inc();
            Log($"Count: {counter.Count}");
            // inc 4
        }

        private async AwaitableCoroutine GetCoroutine2(Counter counter)
        {
            counter.Inc();
            Log($"Count: {counter.Count}");
            await AwaitableCoroutine.Yield(); // 1

            counter.Inc();
            Log($"Count: {counter.Count}"); // 2

            await GetCoroutine(counter); // 6

            // counter.Inc();
            // Log($"Count: {counter.Count}");

            await GetCoroutine(counter); // 10

            // counter.Inc();
            // Log($"Count: {counter.Count}");
            // inc 10
        }

        private sealed class MyException : System.Exception
        {

        }

        private async AwaitableCoroutine CreateWithException()
        {
            await AwaitableCoroutine.Yield();
            throw new MyException();
        }

        [Fact]
        public void CreateCoroutine()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();

            _ = runner.Context(() => GetCoroutine(counter));
        }

        [Fact]
        public void RunCoroutine()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();

            _ = runner.Context(() => GetCoroutine(counter));

            var i = 0;
            while (i < 3)
            {
                runner.Update();
                i++;
                Log($"i: {i}");
                Assert.Equal(i, counter.Count);
            }
        }

        [Fact]
        public void RunCoroutineInside()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();

            _ = runner.Context(() => GetCoroutine2(counter));

            var i = 0;
            while (i < 10)
            {
                runner.Update();
                i++;
                Log($"i: {i}");
                Assert.Equal(i, counter.Count);
            }

            runner.Update();
            Log($"i: {i}");
        }

        [Fact]
        public void AwaitCoroutineWithAnotherRunner()
        {
            var runner1 = new CoroutineRunner();
            var runner2 = new CoroutineRunner();

            var coroutine1 = runner1.Context(() => AwaitableCoroutine.DelayCount(0));

            var coroutine2 = runner2.Context(() =>
                coroutine1.AndThen(() => AwaitableCoroutine.DelayCount(0))
            );

            Assert.False(coroutine1.IsCompleted);
            Assert.False(coroutine2.IsCompleted);

            runner1.Update();
            Assert.True(coroutine1.IsCompleted);
            Assert.False(coroutine2.IsCompleted);

            for (var i = 0; i < 5; i++)
            {
                runner1.Update();
                Assert.False(coroutine2.IsCompleted);
            }

            runner2.Update();
            runner2.Update();
            runner2.Update();
            Assert.True(coroutine2.IsCompleted);
        }

        [Fact]
        public void WithExceptionTest()
        {
            var runner = new CoroutineRunner();

            var co = runner.Context(CreateWithException);

            Assert.False(co.IsCompleted);

            runner.Update();

            Assert.Throws<MyException>(runner.Update);

            runner.Update();
            Assert.True(co.IsCanceled);
        }

        [Fact]
        public void WithExceptionsTest()
        {
            var runner = new CoroutineRunner();

            var (co1, co2, co3) = runner.Context(() =>
                (CreateWithException(), AwaitableCoroutine.While(() => true), CreateWithException())
            );

            var waitAll = runner.Context(() => AwaitableCoroutine.WaitAll(co1, co2, co3));

            Assert.False(co1.IsCompleted);
            Assert.False(co2.IsCompleted);

            runner.Update();

            Assert.Throws<System.AggregateException>(runner.Update);

            runner.Update();
            Assert.True(co1.IsCanceled);
            Assert.False(co2.IsCanceled);
            Assert.True(co3.IsCanceled);
            Assert.True(waitAll.IsCanceled);
        }
    }
}
