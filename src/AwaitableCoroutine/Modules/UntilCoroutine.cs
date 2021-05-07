using System;

namespace AwaitableCoroutine
{
    internal sealed class UntilCoroutine : AwaitableCoroutine
    {
        private readonly bool _expected;
        private readonly Func<bool> _predicate;

        public UntilCoroutine(bool expected, Func<bool> predicate)
        {
            _expected = expected;
            _predicate = predicate;
        }

        protected override void OnMoveNext()
        {
            if (_predicate.Invoke() == _expected)
            {
                Complete();
            }
        }
    }

    public partial class AwaitableCoroutine
    {
        public static AwaitableCoroutine Until(Func<bool> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return new UntilCoroutine(true, predicate);
        }

        public static AwaitableCoroutine While(Func<bool> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return new UntilCoroutine(false, predicate);
        }
    }
}
