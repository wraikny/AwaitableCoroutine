using System;

using Altseed2;

using AC = AwaitableCoroutine.AwaitableCoroutine;

namespace AwaitableCoroutine.Altseed2
{
    public static class AwaitableCoroutine
    {
        public static AC DelaySecond(float second, Action onUpdating = null, Action onCompleted = null)
        {
            var i = 0.0f;
            return AC.While(() =>
                {
                    i += Engine.DeltaSecond;
                    return i < second;
                },
                onUpdating: onUpdating,
                onCompleted: onCompleted
            );
        }

        public static AwaitableCoroutine<T> DelaySecond<T>(float second, Func<T> generator, Action onUpdating = null, Action<T> onCompleted = null)
        {
            var i = 0.0f;
            return AC.While(() =>
                {
                    i += Engine.DeltaSecond;
                    return i < second;
                },
                generator,
                onUpdating: onUpdating,
                onCompleted: onCompleted);
        }
    }
}
