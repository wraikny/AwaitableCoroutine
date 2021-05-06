using System;
using System.Threading;
using System.ComponentModel;

namespace AwaitableCoroutine
{
    public interface ICoroutineRunner
    {
        private static readonly ThreadLocal<ICoroutineRunner> s_instance = new ThreadLocal<ICoroutineRunner>();
        public static ICoroutineRunner Instance => s_instance.Value;
        public static void SetInstance(ICoroutineRunner runner)
        {
            if (s_instance.Value is { })
            {
                throw new InvalidOperationException("ICoroutineRunner.Instance is alread set");
            }
            s_instance.Value = runner;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        void Register(AwaitableCoroutineBase coroutine);

        [EditorBrowsable(EditorBrowsableState.Never)]
        void Post(Action continuation);

        void Update();
    }

    public static class ICoroutineRunnerExt
    {
        public static AwaitableCoroutine Create(this ICoroutineRunner runner, Func<AwaitableCoroutine> init)
        {
            if (runner is null)
            {
                throw new ArgumentNullException(nameof(runner));
            }

            if (init is null)
            {
                throw new ArgumentNullException(nameof(init));
            }

            ICoroutineRunner.SetInstance(runner);
            var coroutine = init.Invoke();
            ICoroutineRunner.SetInstance(null);

            runner.Register(coroutine);

            return coroutine;
        }

        public static AwaitableCoroutine<T> Create<T>(this ICoroutineRunner runner, Func<AwaitableCoroutine<T>> init)
        {
            if (runner is null)
            {
                throw new ArgumentNullException(nameof(runner));
            }

            if (init is null)
            {
                throw new ArgumentNullException(nameof(init));
            }

            ICoroutineRunner.SetInstance(runner);
            var coroutine = init.Invoke();
            ICoroutineRunner.SetInstance(null);

            runner.Register(coroutine);

            return coroutine;
        }
    }
}
