using System;
using System.ComponentModel;
using System.Threading;

namespace AwaitableCoroutine
{
    public interface ICoroutineRunner
    {
        private static readonly ThreadLocal<ICoroutineRunner> s_instance = new ThreadLocal<ICoroutineRunner>();

        internal static ICoroutineRunner Context
        {
            get => s_instance.Value;
            set => s_instance.Value = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        void OnRegistering(AwaitableCoroutineBase coroutine);

        [EditorBrowsable(EditorBrowsableState.Never)]
        void Post(Action continuation);

        void OnUpdate();
    }

    public static class ICoroutineRunnerExt
    {
        public static void Update(this ICoroutineRunner runner)
        {
            ICoroutineRunner lastRunner = null;
            try
            {
                lastRunner = ICoroutineRunner.Context;
                ICoroutineRunner.Context = runner;
                runner.OnUpdate();
            }
            finally
            {
                ICoroutineRunner.Context = lastRunner;
            }
        }

        public static TRes AddCoroutine<TRes>(this ICoroutineRunner runner, Func<TRes> init)
            where TRes : AwaitableCoroutineBase
        {
            if (init is null)
            {
                throw new ArgumentNullException(nameof(init));
            }

            ICoroutineRunner lastRunner = null;
            TRes coroutine;
            try
            {
                lastRunner = ICoroutineRunner.Context;
                ICoroutineRunner.Context = runner;
                coroutine = init();
            }
            finally
            {
                ICoroutineRunner.Context = lastRunner;
            }

            return coroutine;
        }
    }
}
