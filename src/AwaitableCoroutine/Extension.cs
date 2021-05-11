using System;

namespace AwaitableCoroutine
{
    public static class AwaitableCoroutineBaseExtension
    {
        public static T OnCompleted<T>(this T coroutine, Action onCompleted)
            where T : AwaitableCoroutineBase
        {
            if (coroutine is null)
            {
                ThrowHelper.ArgNull(nameof(coroutine));
            }

            if (onCompleted is null)
            {
                ThrowHelper.ArgNull(nameof(onCompleted));
            }

            coroutine.ContinueWith(onCompleted);
            return coroutine;
        }

        public static T OnUpdating<T>(this T coroutine, Action onUpdating)
            where T : AwaitableCoroutineBase
        {
            if (coroutine is null)
            {
                ThrowHelper.ArgNull(nameof(coroutine));
            }

            if (onUpdating is null)
            {
                ThrowHelper.ArgNull(nameof(onUpdating));
            }

            if (coroutine.IsCanceled)
            {
                ThrowHelper.InvalidOp("Coroutine is already canceled");
            }

            if (coroutine.IsCompleted)
            {
                ThrowHelper.InvalidOp("Coroutine is already completed");
            }

            coroutine.OnUpdating += onUpdating;
            return coroutine;
        }

        public static T OnCanceled<T>(this T coroutine, Action onCanceled)
            where T : AwaitableCoroutineBase
        {
            if (coroutine is null)
            {
                ThrowHelper.ArgNull(nameof(coroutine));
            }

            if (onCanceled is null)
            {
                ThrowHelper.ArgNull(nameof(onCanceled));
            }

            coroutine.AddOnCanceled(onCanceled);
            return coroutine;
        }
    }
}
