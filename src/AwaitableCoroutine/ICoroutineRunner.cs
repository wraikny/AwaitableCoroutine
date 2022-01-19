using System;
using System.ComponentModel;
using System.Threading;

namespace AwaitableCoroutine
{
    public interface ICoroutineRunner
    {
        private static readonly ThreadLocal<ICoroutineRunner> s_instance = new ThreadLocal<ICoroutineRunner>();

        internal static ICoroutineRunner Instance
        {
            get => s_instance.Value;
            set => s_instance.Value = value;
        }

        public static ICoroutineRunner GetContext()
        {
            var ctx = s_instance.Value;

            if (ctx is null)
            {
                ThrowHelper.InvalidOp("Out of context");
            }

            return ctx;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        void OnRegistering(AwaitableCoroutineBase coroutine);

        [EditorBrowsable(EditorBrowsableState.Never)]
        void OnUpdate();

        [EditorBrowsable(EditorBrowsableState.Never)]
        void Post(Action continuation);

        bool IsUpdating { get; }
    }

    public static class ICoroutineRunnerExt
    {
        internal static void Register(this ICoroutineRunner runner, AwaitableCoroutineBase coroutine)
        {
            if (coroutine.IsCompletedSuccessfully)
            {
                return;
            }

            runner.OnRegistering(coroutine);
        }

        public static void Update(this ICoroutineRunner runner)
        {
            Internal.Logger.Log($"{runner.GetType().Name} is updating");
            runner.Context(runner.OnUpdate);
            Internal.Logger.Log($"{runner.GetType().Name} is updated");
        }

        public static void Context(this ICoroutineRunner runner, Action action)
        {
            if (runner is null)
            {
                ThrowHelper.ArgNull(nameof(runner));
            }

            if (action is null)
            {
                ThrowHelper.ArgNull(nameof(action));
            }

            var prev = ICoroutineRunner.Instance;

            if (prev == runner)
            {
                action.Invoke();
            }
            else
            {
                ICoroutineRunner.Instance = runner;

                try
                {
                    action.Invoke();
                }
                finally
                {
                    ICoroutineRunner.Instance = prev;
                }
            }
        }

        public static T Context<T>(this ICoroutineRunner runner, Func<T> init)
        {
            if (runner is null)
            {
                ThrowHelper.ArgNull(nameof(runner));
            }

            if (init is null)
            {
                ThrowHelper.ArgNull(nameof(init));
            }

            var prev = ICoroutineRunner.Instance;

            if (prev == runner)
            {
                return init.Invoke();
            }
            else
            {
                ICoroutineRunner.Instance = runner;

                try
                {
                    return init.Invoke();
                }
                finally
                {
                    ICoroutineRunner.Instance = prev;
                }
            }
        }

        public static AwaitableCoroutine Create(this ICoroutineRunner runner, Func<AwaitableCoroutine> init)
        {
            return Context<AwaitableCoroutine>(runner, init);
        }

        public static AwaitableCoroutine<T> Create<T>(this ICoroutineRunner runner, Func<AwaitableCoroutine<T>> init)
        {
            return Context<AwaitableCoroutine<T>>(runner, init);
        }
    }
}
