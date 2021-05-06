using System;
using System.Threading;
using System.ComponentModel;

using System.Collections.Generic;

namespace AwaitableCoroutine
{
    public interface ICoroutineRunner
    {
        private static readonly ThreadLocal<Stack<ICoroutineRunner>> s_instances = new ThreadLocal<Stack<ICoroutineRunner>>();

        internal static ICoroutineRunner Peek()
        {
            var stack = s_instances.Value;
            if (stack is null) return null;

            return stack.TryPeek(out var res) ? res : null;
        }

        internal static void Push(ICoroutineRunner runner)
        {
            if (runner is null)
            {
                throw new ArgumentNullException(nameof(runner));
            }

            s_instances.Value ??= new Stack<ICoroutineRunner>();

            s_instances.Value.Push(runner);
        }

        internal static void Pop()
        {
            s_instances.Value.Pop();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        void OnRegistering(AwaitableCoroutineBase coroutine);

        [EditorBrowsable(EditorBrowsableState.Never)]
        void Post(Action continuation);

        void OnUpdate();
    }

    public static class ICoroutineRunnerExt
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static T SetupRunner<T>(this T coroutine)
            where T : AwaitableCoroutineBase
        {
            if (coroutine is null)
            {
                throw new ArgumentNullException(nameof(coroutine));
            }

            var runner = ICoroutineRunner.Peek();

            if (runner is null)
            {
                throw new InvalidOperationException("Out of ICoroutineRunner context");
            }

            runner.OnRegistering(coroutine);
            coroutine.SetRegistered(runner);

            return coroutine;
        }

        public static void Update(this ICoroutineRunner runner)
        {
            try
            {
                ICoroutineRunner.Push(runner);
                runner.OnUpdate();
            }
            finally
            {
                ICoroutineRunner.Pop();
            }
        }

        public static TRes AddCoroutine<TRes>(this ICoroutineRunner runner, Func<TRes> init)
            where TRes : AwaitableCoroutineBase
        {
            if (init is null)
            {
                throw new ArgumentNullException(nameof(init));
            }

            TRes coroutine;

            try
            {
                ICoroutineRunner.Push(runner);
                coroutine = init();
            }
            finally
            {
                ICoroutineRunner.Pop();
            }

            return coroutine;
        }

        public static TRes AddCoroutine<TRes, T0>(this ICoroutineRunner runner, Func<T0, TRes> init, T0 a0)
            where TRes : AwaitableCoroutineBase
        {
            if (init is null)
            {
                throw new ArgumentNullException(nameof(init));
            }

            TRes coroutine;

            try
            {
                ICoroutineRunner.Push(runner);
                coroutine = init(a0);
            }
            finally
            {
                ICoroutineRunner.Pop();
            }

            return coroutine;
        }

        public static TRes AddCoroutine<TRes, T0, T1>(this ICoroutineRunner runner, Func<T0, T1, TRes> init, T0 a0, T1 a1)
            where TRes : AwaitableCoroutineBase
        {
            if (init is null)
            {
                throw new ArgumentNullException(nameof(init));
            }

            TRes coroutine;

            try
            {
                ICoroutineRunner.Push(runner);
                coroutine = init(a0, a1);
            }
            finally
            {
                ICoroutineRunner.Pop();
            }

            return coroutine;
        }

        public static TRes AddCoroutine<TRes, T0, T1, T2>(this ICoroutineRunner runner, Func<T0, T1, T2, TRes> init, T0 a0, T1 a1, T2 a2)
            where TRes : AwaitableCoroutineBase
        {
            if (init is null)
            {
                throw new ArgumentNullException(nameof(init));
            }

            TRes coroutine;

            try
            {
                ICoroutineRunner.Push(runner);
                coroutine = init(a0, a1, a2);
            }
            finally
            {
                ICoroutineRunner.Pop();
            }

            return coroutine;
        }

        public static TRes AddCoroutine<TRes, T0, T1, T2, T3>(this ICoroutineRunner runner, Func<T0, T1, T2, T3, TRes> init, T0 a0, T1 a1, T2 a2, T3 a3)
            where TRes : AwaitableCoroutineBase
        {
            if (init is null)
            {
                throw new ArgumentNullException(nameof(init));
            }

            TRes coroutine;

            try
            {
                ICoroutineRunner.Push(runner);
                coroutine = init(a0, a1, a2, a3);
            }
            finally
            {
                ICoroutineRunner.Pop();
            }

            return coroutine;
        }

        public static TRes AddCoroutine<TRes, T0, T1, T2, T3, T4>(this ICoroutineRunner runner, Func<T0, T1, T2, T3, T4, TRes> init, T0 a0, T1 a1, T2 a2, T3 a3, T4 a4)
            where TRes : AwaitableCoroutineBase
        {
            if (init is null)
            {
                throw new ArgumentNullException(nameof(init));
            }

            TRes coroutine;

            try
            {
                ICoroutineRunner.Push(runner);
                coroutine = init(a0, a1, a2, a3, a4);
            }
            finally
            {
                ICoroutineRunner.Pop();
            }

            return coroutine;
        }

        public static TRes AddCoroutine<TRes, T0, T1, T2, T3, T4, T5>(this ICoroutineRunner runner, Func<T0, T1, T2, T3, T4, T5, TRes> init, T0 a0, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
            where TRes : AwaitableCoroutineBase
        {
            if (init is null)
            {
                throw new ArgumentNullException(nameof(init));
            }

            TRes coroutine;

            try
            {
                ICoroutineRunner.Push(runner);
                coroutine = init(a0, a1, a2, a3, a4, a5);
            }
            finally
            {
                ICoroutineRunner.Pop();
            }

            return coroutine;
        }

        public static TRes AddCoroutine<TRes, T0, T1, T2, T3, T4, T5, T6>(this ICoroutineRunner runner, Func<T0, T1, T2, T3, T4, T5, T6, TRes> init, T0 a0, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
            where TRes : AwaitableCoroutineBase
        {
            if (init is null)
            {
                throw new ArgumentNullException(nameof(init));
            }

            TRes coroutine;

            try
            {
                ICoroutineRunner.Push(runner);
                coroutine = init(a0, a1, a2, a3, a4, a5, a6);
            }
            finally
            {
                ICoroutineRunner.Pop();
            }

            return coroutine;
        }

        public static TRes AddCoroutine<TRes, T0, T1, T2, T3, T4, T5, T6, T7>(this ICoroutineRunner runner, Func<T0, T1, T2, T3, T4, T5, T6, T7, TRes> init, T0 a0, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
            where TRes : AwaitableCoroutineBase
        {
            if (init is null)
            {
                throw new ArgumentNullException(nameof(init));
            }

            TRes coroutine;

            try
            {
                ICoroutineRunner.Push(runner);
                coroutine = init(a0, a1, a2, a3, a4, a5, a6, a7);
            }
            finally
            {
                ICoroutineRunner.Pop();
            }

            return coroutine;
        }
    }
}
