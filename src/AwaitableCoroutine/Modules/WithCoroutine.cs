using System;

namespace AwaitableCoroutine
{
    public static class WithCoroutineExt
    {
        public static AwaitableCoroutine With(this AwaitableCoroutineBase coroutine, Action onUpdating, Action onCompleted)
        {
            return AwaitableCoroutine.Until(
                () => coroutine.IsCompleted,
                onUpdating: onUpdating,
                onCompleted: onCompleted
            );
        }
    }
}
