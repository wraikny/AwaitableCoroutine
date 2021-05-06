using System;
using System.Collections.Generic;

namespace AwaitableCoroutine
{
    internal sealed class WaitAnyCoroutine : AwaitableCoroutine
    {
        private readonly List<AwaitableCoroutineBase> _coroutines;

        public WaitAnyCoroutine(ReadOnlySpan<AwaitableCoroutineBase> coroutines)
        {
            _coroutines = new List<AwaitableCoroutineBase>();
            foreach (var c in coroutines)
            {
                _coroutines.Add(c);
            }
        }

        protected override void OnMoveNext()
        {
            foreach (var c in _coroutines)
            {
                c.MoveNext();
                if (c.IsCompleted)
                {
                    Complete();
                    return;
                }
            }
        }
    }

    internal sealed class WaitAnyCoroutine<T> : AwaitableCoroutine<IReadOnlyList<T>>
    {
        private readonly AwaitableCoroutine<T>[] _coroutines;

        public WaitAnyCoroutine(ReadOnlySpan<AwaitableCoroutine<T>> coroutines)
        {
            _coroutines = new AwaitableCoroutine<T>[coroutines.Length];
            coroutines.CopyTo(_coroutines);
        }


        protected override void OnMoveNext()
        {
            List<T> result = null;

            foreach (var c in _coroutines)
            {
                c.MoveNext();
                if (c.IsCompleted)
                {
                    result ??= new List<T>();
                    result.Add(c.Result);
                }
            }

            if (result is { })
            {
                Complete(result);
            }
        }
    }

    public partial class AwaitableCoroutine
    {
        public static AwaitableCoroutine WaitAny(ReadOnlySpan<AwaitableCoroutineBase> coroutines)
        {
            return new WaitAnyCoroutine(coroutines);
        }

        public static AwaitableCoroutine<IReadOnlyList<T>> WaitAny<T>(ReadOnlySpan<AwaitableCoroutine<T>> coroutines)
        {
            return new WaitAnyCoroutine<T>(coroutines);
        }
    }
}
