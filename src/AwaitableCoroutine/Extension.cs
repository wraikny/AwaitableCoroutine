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
                throw new ArgumentNullException(nameof(coroutine));
            }

            if (onCompleted is null)
            {
                throw new ArgumentNullException(nameof(onCompleted));
            }

            coroutine.ContinueWith(onCompleted);
            return coroutine;
        }

        public static T OnUpdating<T>(this T coroutine, Action onUpdating)
            where T : AwaitableCoroutineBase
        {
            if (coroutine is null)
            {
                throw new ArgumentNullException(nameof(coroutine));
            }

            if (onUpdating is null)
            {
                throw new ArgumentNullException(nameof(onUpdating));
            }

            if (coroutine.IsCanceled)
            {
                throw new InvalidOperationException("Coroutine is already canceled");
            }

            if (coroutine.IsCompleted)
            {
                throw new InvalidOperationException("Coroutine is already completed");
            }

            coroutine.OnUpdating += onUpdating;
            return coroutine;
        }

        public static T OnCanceled<T>(this T coroutine, Action onCanceled)
            where T : AwaitableCoroutineBase
        {
            if (coroutine is null)
            {
                throw new ArgumentNullException(nameof(coroutine));
            }

            if (onCanceled is null)
            {
                throw new ArgumentNullException(nameof(onCanceled));
            }

            coroutine.AddOnCanceled(onCanceled);
            return coroutine;
        }
    }
}
