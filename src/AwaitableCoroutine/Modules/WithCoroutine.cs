using System;

namespace AwaitableCoroutine
{
    public static class WithCoroutineExt
    {
        public static async AwaitableCoroutine With(this AwaitableCoroutineBase coroutine, Action inWaiting)
        {
            if (inWaiting is null)
            {
                throw new ArgumentNullException(nameof(inWaiting));
            }

            while (!coroutine.IsCompleted)
            {
                inWaiting.Invoke();
                await AwaitableCoroutine.Yield();
            }
        }
    }
}
