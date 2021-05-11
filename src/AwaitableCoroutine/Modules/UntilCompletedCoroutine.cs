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
                if (coroutine.IsCanceled)
                {
                    throw new ChildCanceledException<AwaitableCoroutineBase>(coroutine);
                }
                action.Invoke();
                await AwaitableCoroutine.Yield();
            }
        }
    }
}
