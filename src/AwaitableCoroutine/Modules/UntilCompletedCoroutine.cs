using System;

namespace AwaitableCoroutine
{
    public static class UntilCompletedCoroutineExt
    {
        public static async AwaitableCoroutine UntilCompleted(this AwaitableCoroutineBase coroutine, Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            while (!coroutine.IsCompleted)
            {
                action.Invoke();
                await AwaitableCoroutine.Yield();
            }
        }
    }
}
