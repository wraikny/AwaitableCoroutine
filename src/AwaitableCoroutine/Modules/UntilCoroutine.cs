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

    internal sealed class UntilCoroutine<T> : AwaitableCoroutine<T>
    {
        private readonly bool _expected;
        private readonly Func<bool> _predicate;
        private readonly Func<T> _result;

        public UntilCoroutine(bool expected, Func<bool> predicate, Func<T> result)
        {
            _expected = expected;
            _predicate = predicate;
            _result = result;
        }

        protected override void OnMoveNext()
        {
            if (_predicate.Invoke() == _expected)
            {
                Complete(_result.Invoke());
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

        public static AwaitableCoroutine<T> Until<T>(Func<bool> predicate, Func<T> result)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            return new UntilCoroutine<T>(true, predicate, result);
        }

        public static AwaitableCoroutine<T> While<T>(Func<bool> predicate, Func<T> result)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            return new UntilCoroutine<T>(false, predicate, result);
        }
    }
}
