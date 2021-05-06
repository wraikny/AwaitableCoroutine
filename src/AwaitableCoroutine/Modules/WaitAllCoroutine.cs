using System;
using System.Collections.Generic;
using System.Linq;

namespace AwaitableCoroutine
{
    internal sealed class WaitAllCoroutine : AwaitableCoroutine
    {
        private readonly List<AwaitableCoroutineBase> _coroutines;

        public WaitAllCoroutine(ICoroutineRunner runner, ReadOnlySpan<AwaitableCoroutineBase> coroutines)
            : base(runner)
        {
            _coroutines = new List<AwaitableCoroutineBase>();
            foreach(var c in coroutines)
            {
                _coroutines.Add(c);
            }
        }

        public override void MoveNext()
        {
            if (IsCompleted) return;

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

    public sealed class WaitAllCoroutine<T> : AwaitableCoroutine<IReadOnlyList<T>>
    {
        private readonly AwaitableCoroutine<T>[] _coroutines;

        public WaitAllCoroutine(ICoroutineRunner runner, ReadOnlySpan<AwaitableCoroutine<T>> coroutines)
            : base(runner)
        {
            _coroutines = new AwaitableCoroutine<T>[coroutines.Length];
            coroutines.CopyTo(_coroutines);
        }


        public override void MoveNext()
        {
            if (IsCompleted) return;

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

    public static partial class ICoroutineRunnerExt
    {
        public static AwaitableCoroutine WaitAll(this ICoroutineRunner runner, ReadOnlySpan<AwaitableCoroutineBase> coroutines)
        {
            var coroutine = new WaitAllCoroutine(runner, coroutines);
            runner.Register(coroutine);
            return coroutine;
        }

        public static AwaitableCoroutine<IReadOnlyList<T>> WaitAll<T>(this ICoroutineRunner runner, ReadOnlySpan<AwaitableCoroutine<T>> coroutines)
        {
            var coroutine = new WaitAllCoroutine<T>(runner, coroutines);
            runner.Register(coroutine);
            return coroutine;
        }
    }
}
