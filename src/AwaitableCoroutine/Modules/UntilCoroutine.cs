using System;
using System.ComponentModel;

namespace AwaitableCoroutine
{
    internal sealed class UntilCoroutine : AwaitableCoroutine
    {
        private readonly Func<bool> _predicate;

        public UntilCoroutine(ICoroutineRunner runner, Func<bool> predicate)
            : base(runner)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            _predicate = predicate;
        }

        public override void MoveNext()
        {
            if (IsCompleted) return;

            if (_predicate.Invoke())
            {
                Complete();
            }
        }
    }

    public static partial class ICoroutineRunnerExt
    {
        public static AwaitableCoroutine Until(this ICoroutineRunner runner, Func<bool> predicate)
        {
            var coroutine = new UntilCoroutine(runner, predicate);
            runner.Register(coroutine);
            return coroutine;
        }
    }
}
