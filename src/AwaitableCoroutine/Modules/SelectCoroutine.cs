using System;

namespace AwaitableCoroutine
{
    internal sealed class SelectCoroutine<T> : AwaitableCoroutine<T>
    {
        private readonly AwaitableCoroutineBase _child;
        private readonly Func<T> _result;
        public SelectCoroutine(AwaitableCoroutineBase child, Func<T> result)
        {
            _child = child;
            _result = result;
        }

        protected override void OnMoveNext()
        {
            if (_child.IsCompleted) Complete(_result.Invoke());
        }
    }

    internal sealed class SelectFromCoroutine<T, U> : AwaitableCoroutine<U>
    {
        private readonly AwaitableCoroutine<T> _child;
        private readonly Func<T, U> _result;
        public SelectFromCoroutine(AwaitableCoroutine<T> child, Func<T, U> result)
        {
            _child = child;
            _result = result;
        }

        protected override void OnMoveNext()
        {
            if (_child.IsCompleted) Complete(_result.Invoke(_child.Result));
        }
    }

    public static class AwaitableCoroutineSelectExt
    {
        public static AwaitableCoroutine<T> SelectTo<T>(this AwaitableCoroutineBase coroutine, T result)
        {
            return new SelectCoroutine<T>(coroutine, () => result);
        }

        public static AwaitableCoroutine<T> Select<T>(this AwaitableCoroutineBase coroutine, Func<T> thunk)
        {
            if (thunk is null)
            {
                throw new ArgumentNullException(nameof(thunk));
            }

            if (coroutine is null)
            {
                throw new ArgumentNullException(nameof(coroutine));
            }

            return new SelectCoroutine<T>(coroutine, thunk);
        }

        public static AwaitableCoroutine<U> Select<T, U>(this AwaitableCoroutine<T> coroutine, Func<T, U> thunk)
        {
            if (thunk is null)
            {
                throw new ArgumentNullException(nameof(thunk));
            }

            if (coroutine is null)
            {
                throw new ArgumentNullException(nameof(coroutine));
            }

            return new SelectFromCoroutine<T, U>(coroutine, thunk);
        }
    }
}
