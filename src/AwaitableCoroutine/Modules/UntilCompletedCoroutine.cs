using System;

namespace AwaitableCoroutine
{
    public static class UntilCompletedCoroutineExt
    {
        public static async AwaitableCoroutine UntilCompleted(this AwaitableCoroutineBase coroutine, Action action)
        {
            if (coroutine is null)
            {
                ThrowHelper.ArgNull(nameof(coroutine));
            }

            if (action is null)
            {
                ThrowHelper.ArgNull(nameof(action));
            }

            while (!coroutine.IsCompleted)
            {
                if (coroutine.IsCanceled)
                {
                    AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(coroutine);
                }
                action.Invoke();
                await AwaitableCoroutine.Yield();
            }
        }

        public static async AwaitableCoroutine UntilCompleted(this AwaitableCoroutineBase coroutine, Func<AwaitableCoroutine> createCoroutine)
        {
            if (coroutine is null)
            {
                ThrowHelper.ArgNull(nameof(coroutine));
            }

            if (createCoroutine is null)
            {
                ThrowHelper.ArgNull(nameof(createCoroutine));
            }

            while (!coroutine.IsCompleted)
            {
                if (coroutine.IsCanceled)
                {
                    AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(coroutine);
                }
                await createCoroutine.Invoke();
            }
        }

        public static async AwaitableCoroutine UntilCompleted<T>(this AwaitableCoroutineBase coroutine, Func<AwaitableCoroutine<T>> action)
        {
            if (coroutine is null)
            {
                ThrowHelper.ArgNull(nameof(coroutine));
            }

            if (action is null)
            {
                ThrowHelper.ArgNull(nameof(action));
            }

            while (!coroutine.IsCompleted)
            {
                if (coroutine.IsCanceled)
                {
                    AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(coroutine);
                }
                _ = await action.Invoke();
            }
        }
    }
}
