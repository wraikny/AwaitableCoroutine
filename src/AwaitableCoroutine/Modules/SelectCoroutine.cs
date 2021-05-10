using System;

namespace AwaitableCoroutine
{
    public static class AwaitableCoroutineSelectExt
    {
        public static async AwaitableCoroutine<T> SelectTo<T>(this AwaitableCoroutineBase coroutine, T result)
        {
            while (!coroutine.IsCompleted)
            {
                await AwaitableCoroutine.Yield();
            }

            return result;
        }

        public static async AwaitableCoroutine<T> Select<T>(this AwaitableCoroutine coroutine, Func<T> thunk)
        {
            if (coroutine is null)
            {
                throw new ArgumentNullException(nameof(coroutine));
            }

            if (thunk is null)
            {
                throw new ArgumentNullException(nameof(thunk));
            }

            while (!coroutine.IsCompleted)
            {
                await AwaitableCoroutine.Yield();
            }

            return thunk();
        }

        public static async AwaitableCoroutine<U> Select<T, U>(this AwaitableCoroutine<T> coroutine, Func<T, U> thunk)
        {
            if (coroutine is null)
            {
                throw new ArgumentNullException(nameof(coroutine));
            }

            if (thunk is null)
            {
                throw new ArgumentNullException(nameof(thunk));
            }

            while (!coroutine.IsCompleted)
            {
                await AwaitableCoroutine.Yield();
            }

            return thunk(coroutine.Result);
        }
    }
}
