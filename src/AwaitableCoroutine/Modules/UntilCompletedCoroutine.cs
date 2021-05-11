using System;

namespace AwaitableCoroutine
{
    public static class UntilCompletedCoroutineExt
    {
        public static async AwaitableCoroutine UntilCompleted(this AwaitableCoroutineBase coroutine, Action action)
        {
            if (action is null)
            {
                ThrowHelper.ArgNull(nameof(action));
            }

            while (!coroutine.IsCompleted)
            {
                if (coroutine.IsCanceled)
                {
                    AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(coroutine);
                }
                action.Invoke();
                await AwaitableCoroutine.Yield();
            }
        }
    }
}
