﻿using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class UntilCompletedCoroutineTest : TestTemplate
    {
        public UntilCompletedCoroutineTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        [Fact]
        public void RunUntilCompleted()
        {
            var runner1 = new CoroutineRunner();
            var runner2 = new CoroutineRunner();

            var counter = new Counter();

            var coroutine1 = runner1.Context(() => AwaitableCoroutine.DelayCount(3));
            var coroutine2 = runner2.Context(() => coroutine1.UntilCompleted(counter.Inc));

            Assert.False(coroutine1.IsCompleted);
            Assert.False(coroutine2.IsCompleted);

            runner2.Update();
            Assert.Equal(1, counter.Count);

            runner2.Update();
            Assert.Equal(2, counter.Count);

            for (var i = 0; i < 3; i++)
            {
                runner1.Update();
                Assert.Equal(2, counter.Count);
                Assert.False(coroutine1.IsCompleted);
                Assert.False(coroutine2.IsCompleted);
            }

            runner2.Update();
            Assert.Equal(3, counter.Count);

            runner1.Update();
            Assert.True(coroutine1.IsCompleted);
            Assert.False(coroutine2.IsCompleted);

            runner2.Update();
            Assert.True(coroutine2.IsCompleted);
            Assert.Equal(3, counter.Count);
        }
    }
}