﻿using System;

namespace AwaitableCoroutine
{
    public static class CoroutineBaseExtension
    {
        public static T OnCompleted<T>(this T coroutine, Action onCompleted)
            where T : CoroutineBase
        {
            if (coroutine is null)
            {
                ThrowHelper.ArgNull(nameof(coroutine));
            }

            if (onCompleted is null)
            {
                ThrowHelper.ArgNull(nameof(onCompleted));
            }

            coroutine.AddOnCompletedSuccessfully(onCompleted);
            coroutine.AddOnCanceled(onCompleted);
            return coroutine;
        }

        public static T OnUpdating<T>(this T coroutine, Action onUpdating)
            where T : CoroutineBase
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

            if (coroutine.IsCompletedSuccessfully)
            {
                ThrowHelper.InvalidOp("Coroutine is already completed successfully");
            }

            coroutine.OnUpdating += onUpdating;
            return coroutine;
        }

        public static T OnCanceled<T>(this T coroutine, Action onCanceled)
            where T : CoroutineBase
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

        public static T OnCompletedSuccessfully<T>(this T coroutine, Action onCanceled)
            where T : CoroutineBase
        {
            if (coroutine is null)
            {
                ThrowHelper.ArgNull(nameof(coroutine));
            }

            if (onCanceled is null)
            {
                ThrowHelper.ArgNull(nameof(onCanceled));
            }

            coroutine.AddOnCompletedSuccessfully(onCanceled);
            return coroutine;
        }
    }
}
