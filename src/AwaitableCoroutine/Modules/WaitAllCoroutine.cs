using System;
using System.Collections.Generic;
using System.Linq;

namespace AwaitableCoroutine
{
    internal sealed class WaitAllCoroutine : AwaitableCoroutine
    {
        private readonly List<AwaitableCoroutineBase> _coroutines;

        public WaitAllCoroutine(ReadOnlySpan<AwaitableCoroutineBase> coroutines)
        {
            _coroutines = new List<AwaitableCoroutineBase>();
            foreach (var c in coroutines)
            {
                _coroutines.Add(c);
            }
        }

        protected override void OnMoveNext()
        {
            _coroutines.RemoveAll(c =>
            {
                c.MoveNext();
                return c.IsCompleted;
            });

            if (_coroutines.Count == 0)
            {
                Complete();
            }
        }
    }

    internal sealed class WaitAllCoroutine<T> : AwaitableCoroutine<IReadOnlyList<T>>
    {
        private readonly AwaitableCoroutine<T>[] _coroutines;

        public WaitAllCoroutine(ReadOnlySpan<AwaitableCoroutine<T>> coroutines)
        {
            _coroutines = new AwaitableCoroutine<T>[coroutines.Length];
            coroutines.CopyTo(_coroutines);
        }


        protected override void OnMoveNext()
        {
            var completed = true;
            foreach (var c in _coroutines)
            {
                if (c.IsCompleted) continue;
                c.MoveNext();
                completed &= c.IsCompleted;
            }

            if (completed)
            {
                Complete(_coroutines.Select(c => c.Result).ToArray());
            }
        }
    }

    public partial class AwaitableCoroutine
    {
        public static AwaitableCoroutine WaitAll(ReadOnlySpan<AwaitableCoroutineBase> coroutines)
        {
            return new WaitAllCoroutine(coroutines);
        }

        public static AwaitableCoroutine<IReadOnlyList<T>> WaitAll<T>(ReadOnlySpan<AwaitableCoroutine<T>> coroutines)
        {
            return new WaitAllCoroutine<T>(coroutines);
        }
    }
}
