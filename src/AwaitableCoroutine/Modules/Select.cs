using System;

namespace AwaitableCoroutine
{
    public static class AwaitableCoroutineSelectExt
    {
        public static async AwaitableCoroutine<T> SelectTo<T>(this AwaitableCoroutineBase coroutine, T res)
        {
            while (true)
            {
                coroutine.MoveNext();
                if (coroutine.IsCompleted) break;
                await AwaitableCoroutine.Yield();
            }

            return res;
        }

        public static async AwaitableCoroutine<T> Select<T>(this AwaitableCoroutineBase coroutine, Func<T> thunk)
        {
            if (thunk is null)
            {
                throw new ArgumentNullException(nameof(thunk));
            }

            while (true)
            {
                coroutine.MoveNext();
                if (coroutine.IsCompleted) break;
                await AwaitableCoroutine.Yield();
            }

            return thunk.Invoke();
        }

        public static async AwaitableCoroutine<U> Select<T, U>(this AwaitableCoroutine<T> coroutine, Func<T, U> thunk)
        {
            if (thunk is null)
            {
                throw new ArgumentNullException(nameof(thunk));
            }

            while (true)
            {
                coroutine.MoveNext();
                if (coroutine.IsCompleted) break;
                await AwaitableCoroutine.Yield();
            }

            return thunk.Invoke(coroutine.Result);
        }
    }
}
