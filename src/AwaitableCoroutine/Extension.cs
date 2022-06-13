using System;

namespace AwaitableCoroutine
{
    public static class CoroutineExtension
    {
        public static T OnCompleted<T>(this T coroutine, Action onCompleted)
            where T : Coroutine
        {
            if (coroutine is null)
            {
                ThrowHelper.ArgNull(nameof(coroutine));
            }

            if (onCompleted is null)
            {
                ThrowHelper.ArgNull(nameof(onCompleted));
            }

            if (coroutine.IsCompleted)
            {
                return coroutine;
            }

            coroutine.AddOnCompletedSuccessfully(onCompleted);
            coroutine.AddOnCanceled(onCompleted);
            return coroutine;
        }

        public static T OnUpdating<T>(this T coroutine, Action onUpdating)
            where T : Coroutine
        {
            if (coroutine is null)
            {
                ThrowHelper.ArgNull(nameof(coroutine));
            }

            if (onUpdating is null)
            {
                ThrowHelper.ArgNull(nameof(onUpdating));
            }

            if (coroutine.IsCompleted)
            {
                return coroutine;
            }

            coroutine.OnUpdating += onUpdating;
            return coroutine;
        }

        public static T OnCanceled<T>(this T coroutine, Action onCanceled)
            where T : Coroutine
        {
            if (coroutine is null)
            {
                ThrowHelper.ArgNull(nameof(coroutine));
            }

            if (onCanceled is null)
            {
                ThrowHelper.ArgNull(nameof(onCanceled));
            }

            if (coroutine.IsCompleted)
            {
                return coroutine;
            }

            coroutine.AddOnCanceled(onCanceled);
            return coroutine;
        }

        public static T OnCompletedSuccessfully<T>(this T coroutine, Action onCanceled)
            where T : Coroutine
        {
            if (coroutine is null)
            {
                ThrowHelper.ArgNull(nameof(coroutine));
            }

            if (onCanceled is null)
            {
                ThrowHelper.ArgNull(nameof(onCanceled));
            }

            if (coroutine.IsCompleted)
            {
                return coroutine;
            }

            coroutine.AddOnCompletedSuccessfully(onCanceled);
            return coroutine;
        }
    }
}
