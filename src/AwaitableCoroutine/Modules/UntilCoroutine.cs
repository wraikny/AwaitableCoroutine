using System;

namespace AwaitableCoroutine
{
    internal sealed class UntilCoroutine : AwaitableCoroutine
    {
        private readonly Func<bool> _predicate;

        public UntilCoroutine(Func<bool> predicate)
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

    public partial class AwaitableCoroutine
    {
        public static AwaitableCoroutine Until(Func<bool> predicate)
        {
            return new UntilCoroutine(predicate).SetupRunner();
        }
    }
}
