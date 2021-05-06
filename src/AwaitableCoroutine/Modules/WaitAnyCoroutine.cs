using System;
using System.Collections.Generic;
using System.Linq;

namespace AwaitableCoroutine
{
    internal sealed class WaitAnyCoroutine : AwaitableCoroutine
    {
        private readonly List<AwaitableCoroutineBase> _coroutines;

        public WaitAnyCoroutine(ICoroutineRunner runner, ReadOnlySpan<AwaitableCoroutineBase> coroutines)
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

            var isCompleted = false;
            
            foreach(var c in _coroutines)
            {
                c.MoveNext();
                if (c.IsCompleted)
                {
                    isCompleted = true;
                    return;
                }
            }

            if (isCompleted)
            {
                Complete();
            }
        }
    }

    public sealed class WaitAnyCoroutine<T> : AwaitableCoroutine<IReadOnlyList<T>>
    {
        private readonly AwaitableCoroutine<T>[] _coroutines;

        public WaitAnyCoroutine(ICoroutineRunner runner, ReadOnlySpan<AwaitableCoroutine<T>> coroutines)
            : base(runner)
        {
            _coroutines = new AwaitableCoroutine<T>[coroutines.Length];
            coroutines.CopyTo(_coroutines);
        }


        public override void MoveNext()
        {
            if (IsCompleted) return;

            List<T> result = null;

            foreach (var c in _coroutines)
            {
                c.MoveNext();
                if (c.IsCompleted)
                {
                    result ??= new List<T>();
                    result.Add(c.Result);
                    continue;
                }
            }

            if (result is { })
            {
                Complete(result);
            }
        }
    }

    public static partial class ICoroutineRunnerExt
    {
        public static AwaitableCoroutine WaitAny(this ICoroutineRunner runner, ReadOnlySpan<AwaitableCoroutineBase> coroutines)
        {
            var coroutine = new WaitAnyCoroutine(runner, coroutines);
            runner.Register(coroutine);
            return coroutine;
        }

        public static AwaitableCoroutine<IReadOnlyList<T>> WaitAny<T>(this ICoroutineRunner runner, ReadOnlySpan<AwaitableCoroutine<T>> coroutines)
        {
            var coroutine = new WaitAnyCoroutine<T>(runner, coroutines);
            runner.Register(coroutine);
            return coroutine;
        }
    }
}
