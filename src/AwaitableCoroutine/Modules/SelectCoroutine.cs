using System;

namespace AwaitableCoroutine
{
    public static class CoroutineSelectExt
    {
        public static async Coroutine<T> SelectTo<T>(this CoroutineBase coroutine, T result)
        {
            while (!coroutine.IsCompletedSuccessfully)
            {
                if (coroutine.IsCanceled)
                {
                    Coroutine.ThrowChildCancel<CoroutineBase>(coroutine);
                }
                await Coroutine.Yield();
            }

            return result;
        }

        public static async Coroutine<T> Select<T>(this Coroutine coroutine, Func<T> thunk)
        {
            if (coroutine is null)
            {
                ThrowHelper.ArgNull(nameof(coroutine));
            }

            if (thunk is null)
            {
                ThrowHelper.ArgNull(nameof(thunk));
            }

            while (!coroutine.IsCompletedSuccessfully)
            {
                if (coroutine.IsCanceled)
                {
                    Coroutine.ThrowChildCancel<CoroutineBase>(coroutine);
                }
                await Coroutine.Yield();
            }

            return thunk();
        }

        public static async Coroutine<U> Select<T, U>(this Coroutine<T> coroutine, Func<T, U> thunk)
        {
            if (coroutine is null)
            {
                ThrowHelper.ArgNull(nameof(coroutine));
            }

            if (thunk is null)
            {
                ThrowHelper.ArgNull(nameof(thunk));
            }

            while (!coroutine.IsCompletedSuccessfully)
            {
                if (coroutine.IsCanceled)
                {
                    Coroutine.ThrowChildCancel<CoroutineBase>(coroutine);
                }
                await Coroutine.Yield();
            }

            return thunk(coroutine.Result);
        }
    }
}
