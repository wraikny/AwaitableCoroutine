using System;

namespace AwaitableCoroutine
{
    public static class AwaitableCoroutineAndThenExt
    {
        public static async AwaitableCoroutine AndThen(this AwaitableCoroutine coroutine, Func<AwaitableCoroutine> thunk)
        {
            if (thunk is null)
            {
                throw new ArgumentNullException(nameof(thunk));
            }

            await coroutine;
            await thunk();
        }

        public static async AwaitableCoroutine<T> AndThen<T>(this AwaitableCoroutine coroutine, Func<AwaitableCoroutine<T>> thunk)
        {
            if (thunk is null)
            {
                throw new ArgumentNullException(nameof(thunk));
            }

            await coroutine;
            return await thunk();
        }

        /* AwaitableCoroutine<T> */

        public static async AwaitableCoroutine AndThen<T>(this AwaitableCoroutine<T> coroutine, Func<T, AwaitableCoroutine> thunk)
        {
            if (thunk is null)
            {
                throw new ArgumentNullException(nameof(thunk));
            }

            var res = await coroutine;
            await thunk(res);
        }

        public static async AwaitableCoroutine<U> AndThen<T, U>(this AwaitableCoroutine<T> coroutine, Func<T, AwaitableCoroutine<U>> thunk)
        {
            if (thunk is null)
            {
                throw new ArgumentNullException(nameof(thunk));
            }

            var res = await coroutine;
            return await thunk(res);
        }
    }
}
