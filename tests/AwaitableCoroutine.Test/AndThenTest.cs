﻿using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class AndThenTest : TestTemplate
    {

        public AndThenTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        [Fact]
        public void RunAndThen()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();
            var c = runner.Context(() =>
                AwaitableCoroutine.Yield()
                    .AndThen(() => AwaitableCoroutine.Yield(counter.Inc))
            );

            while (!c.IsCompleted) runner.Update();

            Assert.True(c.IsCompleted);

            Assert.Equal(1, counter.Count);
        }

        private async AwaitableCoroutine CreateCoroutine(int target, Counter counter)
        {
            int count = 0;
            while (true)
            {
                count++;
                counter.Inc();
                if (count >= target) return;
                await AwaitableCoroutine.Yield();
            }
        }

        [Fact]
        public void RunAndThenWithLoop()
        {
            var runner = new CoroutineRunner();
            var counter = new Counter();

            var target = 5;

            var c = runner.Context(() => CreateCoroutine(target, counter));

            while (!c.IsCompleted) runner.Update();

            Assert.Equal(target, counter.Count);
        }
    }
}