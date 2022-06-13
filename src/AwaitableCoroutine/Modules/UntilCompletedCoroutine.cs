using System;

namespace AwaitableCoroutine
{
    public static class UntilCompletedCoroutineExt
    {
        public static async Coroutine UntilCompleted(this Coroutine coroutine, Action action)
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
                if (coroutine.IsCanceled || coroutine.IsFaulted)
                {
                    Coroutine.ThrowChildCancel(coroutine, innerException: coroutine.Exception);
                }
                action.Invoke();
                await Coroutine.Yield();
            }
        }

        public static async Coroutine UntilCompleted(this Coroutine coroutine, Func<Coroutine> createCoroutine)
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
                if (coroutine.IsCanceled || coroutine.IsFaulted)
                {
                    Coroutine.ThrowChildCancel(coroutine, innerException: coroutine.Exception);
                }
                await createCoroutine.Invoke();
            }
        }

        public static async Coroutine UntilCompleted<T>(this Coroutine coroutine, Func<Coroutine<T>> action)
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
                if (coroutine.IsCanceled || coroutine.IsFaulted)
                {
                    Coroutine.ThrowChildCancel(coroutine, innerException: coroutine.Exception);
                }
                _ = await action.Invoke();
            }
        }
    }
}
