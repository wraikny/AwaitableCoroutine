using System;

namespace AwaitableCoroutine
{
    internal sealed class UntilCoroutine : AwaitableCoroutine
    {
        private readonly bool _expected;
        private readonly Func<bool> _predicate;
        private readonly Action _onUpdating;
        private readonly Action _onCompleted;

        public UntilCoroutine(bool expected, Func<bool> predicate, Action onUpdating, Action onCompleted)
        {
            _expected = expected;
            _predicate = predicate;
            _onUpdating = onUpdating;
            _onCompleted = onCompleted;
        }

        protected override void OnMoveNext()
        {
            if (_predicate.Invoke() == _expected)
            {
                Complete();
                _onCompleted?.Invoke();
                return;
            }

            _onUpdating?.Invoke();
        }
    }

    internal sealed class UntilCoroutine<T> : AwaitableCoroutine<T>
    {
        private readonly bool _expected;
        private readonly Func<bool> _predicate;
        private readonly Func<T> _generator;
        private readonly Action _onUpdating;
        private readonly Action<T> _onCompleted;

        public UntilCoroutine(bool expected, Func<bool> predicate, Func<T> generator, Action onUpdating, Action<T> onCompleted)
        {
            _expected = expected;
            _predicate = predicate;
            _generator = generator;
            _onUpdating = onUpdating;
            _onCompleted = onCompleted;
        }

        protected override void OnMoveNext()
        {
            if (_predicate.Invoke() == _expected)
            {
                var res = _generator is null ? default : _generator.Invoke();
                Complete(res);
                _onCompleted?.Invoke(res);
                return;
            }

            _onUpdating?.Invoke();
        }
    }

    public partial class AwaitableCoroutine
    {
        public static AwaitableCoroutine Until(Func<bool> predicate, Action onUpdating = null, Action onCompleted = null)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return new UntilCoroutine(true, predicate, onUpdating, onCompleted);
        }

        public static AwaitableCoroutine While(Func<bool> predicate, Action onUpdating = null, Action onCompleted = null)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return new UntilCoroutine(false, predicate, onUpdating, onCompleted);
        }

        public static AwaitableCoroutine<T> Until<T>(Func<bool> predicate, Func<T> generator, Action onUpdating = null, Action<T> onCompleted = null)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return new UntilCoroutine<T>(true, predicate, generator, onUpdating, onCompleted);
        }

        public static AwaitableCoroutine<T> While<T>(Func<bool> predicate, Func<T> generator, Action onUpdating = null, Action<T> onCompleted = null)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return new UntilCoroutine<T>(false, predicate, generator, onUpdating, onCompleted);
        }
    }
}
