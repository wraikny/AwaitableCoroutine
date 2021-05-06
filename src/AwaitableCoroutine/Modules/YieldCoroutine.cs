using System;

namespace AwaitableCoroutine
{
    public sealed class YieldCoroutine : AwaitableCoroutine
    {
        public YieldCoroutine(ICoroutineRunner runner)
            : base(runner)
        {

        }

        public override void MoveNext()
        {
            if (IsCompleted) return;
            Complete();
        }
    }

    public static partial class ICoroutineRunnerExt
    {
        public static AwaitableCoroutine Yield(this ICoroutineRunner runner)
        {
            var coroutine = new YieldCoroutine(runner);
            runner.Register(coroutine);
            return coroutine;
        }
    }
}
