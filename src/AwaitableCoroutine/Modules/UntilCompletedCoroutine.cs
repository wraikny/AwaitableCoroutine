using System;

namespace AwaitableCoroutine
{
    public static class UntilCompletedCoroutineExt
    {
        public static async Coroutine UntilCompleted(this CoroutineBase coroutine, Action action)
        {
            if (coroutine is null)
            {
                ThrowHelper.ArgNull(nameof(coroutine));
            }

            if (action is null)
            {
                ThrowHelper.ArgNull(nameof(action));
            }

            while (!coroutine.IsCompletedSuccessfully)
            {
                if (coroutine.IsCanceled)
                {
                    Coroutine.ThrowChildCancel<CoroutineBase>(coroutine);
                }
                action.Invoke();
                await Coroutine.Yield();
            }
        }

        public static async Coroutine UntilCompleted(this CoroutineBase coroutine, Func<Coroutine> createCoroutine)
        {
            if (coroutine is null)
            {
                ThrowHelper.ArgNull(nameof(coroutine));
            }

            if (createCoroutine is null)
            {
                ThrowHelper.ArgNull(nameof(createCoroutine));
            }

            while (!coroutine.IsCompletedSuccessfully)
            {
                if (coroutine.IsCanceled)
                {
                    Coroutine.ThrowChildCancel<CoroutineBase>(coroutine);
                }
                await createCoroutine.Invoke();
            }
        }

        public static async Coroutine UntilCompleted<T>(this CoroutineBase coroutine, Func<Coroutine<T>> action)
        {
            if (coroutine is null)
            {
                ThrowHelper.ArgNull(nameof(coroutine));
            }

            if (action is null)
            {
                ThrowHelper.ArgNull(nameof(action));
            }

            while (!coroutine.IsCompletedSuccessfully)
            {
                if (coroutine.IsCanceled)
                {
                    Coroutine.ThrowChildCancel<CoroutineBase>(coroutine);
                }
                _ = await action.Invoke();
            }
        }
    }
}
