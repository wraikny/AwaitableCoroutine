using System;

namespace AwaitableCoroutine
{
    public static class AwaitableCoroutineBaseExtension
    {
        public static T OnCompleted<T>(this T coroutine, Action onCompleted)
            where T : AwaitableCoroutineBase
        {
            coroutine.ContinueWith(onCompleted);
            return coroutine;
        }

        public static T OnUpdating<T>(this T coroutine, Action onUpdating)
            where T : AwaitableCoroutineBase
        {
            coroutine.OnUpdating += onUpdating;
            return coroutine;
        }
    }
}