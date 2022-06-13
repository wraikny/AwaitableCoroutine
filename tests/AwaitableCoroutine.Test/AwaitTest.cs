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

        private async Coroutine SingleYieldCoroutine(Counter counter)
        {
            counter.Inc();
            Log($"Count: {counter.Count}");
            await Coroutine.Yield();
            counter.Inc();
            Log($"Count: {counter.Count}");
        }

        private async Coroutine GetCoroutine(Counter counter)
        {
            for (var i = 0; i < 3; i++)
            {
                counter.Inc();
                Log($"Count: {counter.Count}");
                await Coroutine.Yield();
            }

            counter.Inc();
            Log($"Count: {counter.Count}");
            // inc 4
        }

        private async Coroutine GetCoroutine2(Counter counter)
        {
            counter.Inc();
            Log($"Count: {counter.Count}");
            await Coroutine.Yield(); // 1

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

        private async Coroutine CreateWithException()
        {
            await Coroutine.Yield();
            throw new MyException();
        }

        [Fact]
        public void CreateCoroutine()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();

            _ = runner.Create(() => GetCoroutine(counter));
        }

        [Fact]
        public void RunSingleYieldCoroutine()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();

            var co = runner.Create(() => SingleYieldCoroutine(counter));

            Assert.False(co.IsCompletedSuccessfully);
            Assert.Equal(0, counter.Count);

            runner.Update();
            Assert.False(co.IsCompletedSuccessfully);
            Assert.Equal(1, counter.Count);

            runner.Update();
            Assert.True(co.IsCompletedSuccessfully);
            Assert.Equal(2, counter.Count);
        }

        [Fact]
        public void RunCoroutine()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();

            _ = runner.Create(() => GetCoroutine(counter));

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

            _ = runner.Create(() => GetCoroutine2(counter));

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

            var coroutine1 = runner1.Create(() => Coroutine.DelayCount(0));

            var coroutine2 = runner2.Create(() =>
                coroutine1.AndThen(() => Coroutine.DelayCount(0))
            );

            Assert.False(coroutine1.IsCompletedSuccessfully);
            Assert.False(coroutine2.IsCompletedSuccessfully);

            runner1.Update();
            Assert.True(coroutine1.IsCompletedSuccessfully);
            Assert.False(coroutine2.IsCompletedSuccessfully);

            for (var i = 0; i < 5; i++)
            {
                runner1.Update();
                Assert.False(coroutine2.IsCompletedSuccessfully);
            }

            runner2.Update();
            runner2.Update();
            runner2.Update();
            Assert.True(coroutine2.IsCompletedSuccessfully);
        }

        [Fact]
        public void WithExceptionTest()
        {
            var runner = new CoroutineRunner();

            var co = runner.Create(CreateWithException);

            Assert.False(co.IsCompletedSuccessfully);

            runner.Update();

            Assert.Throws<MyException>(runner.Update);

            runner.Update();
            Assert.True(co.IsFaulted);
        }

        [Fact]
        public void WithExceptionsTest()
        {
            var runner = new CoroutineRunner();

            var (co1, co2, co3) = runner.Context(() =>
                (CreateWithException(), Coroutine.While(() => true), CreateWithException())
            );

            var waitAll = runner.Create(() => Coroutine.WaitAll(co1, co2, co3));

            Assert.False(co1.IsCompletedSuccessfully);
            Assert.False(co2.IsCompletedSuccessfully);

            runner.Update();

            Assert.Throws<System.AggregateException>(runner.Update);

            runner.Update();
            Assert.True(co1.IsFaulted);
            Assert.False(co2.IsFaulted);
            Assert.True(co3.IsFaulted);
            Assert.True(waitAll.IsCanceled);
        }
    }
}
