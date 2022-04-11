using System;

namespace AwaitableCoroutine
{
    public static class CoroutineAndThenExt
    {
        public static async Coroutine AndThen(this Coroutine coroutine, Func<Coroutine> thunk)
        {
            if (thunk is null)
            {
                ThrowHelper.ArgNull(nameof(thunk));
            }

            await coroutine;
            await thunk();
        }

        public static async Coroutine<T> AndThen<T>(this Coroutine coroutine, Func<Coroutine<T>> thunk)
        {
            if (thunk is null)
            {
                ThrowHelper.ArgNull(nameof(thunk));
            }

            await coroutine;
            return await thunk();
        }

        /* Coroutine<T> */

        public static async Coroutine AndThen<T>(this Coroutine<T> coroutine, Func<T, Coroutine> thunk)
        {
            if (thunk is null)
            {
                ThrowHelper.ArgNull(nameof(thunk));
            }

            var res = await coroutine;
            await thunk(res);
        }

        public static async Coroutine<U> AndThen<T, U>(this Coroutine<T> coroutine, Func<T, Coroutine<U>> thunk)
        {
            if (thunk is null)
            {
                ThrowHelper.ArgNull(nameof(thunk));
            }

            var res = await coroutine;
            return await thunk(res);
        }
    }
}
