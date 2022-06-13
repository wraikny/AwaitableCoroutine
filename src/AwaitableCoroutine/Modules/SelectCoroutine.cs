using System;

namespace AwaitableCoroutine
{
    public static class CoroutineSelectExt
    {
        public static async Coroutine<T> SelectTo<T>(this Coroutine coroutine, T result)
        {
            if (coroutine is null)
            {
                ThrowHelper.ArgNull(nameof(coroutine));
            }

            await coroutine;

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

            await coroutine;

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

            return thunk(await coroutine);
        }
    }
}
