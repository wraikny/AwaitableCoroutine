using System;

using Altseed2;

namespace AwaitableCoroutine.Altseed2
{
    public static class Altseed2Coroutine
    {
        public static async Coroutine DelaySecond(float second, Action<float> onUpdating = null)
        {
            if (second <= 0f) return;

            for (var t = 0.0f; t < second; t += Engine.DeltaSecond)
            {
                onUpdating?.Invoke(t / second);
                await Coroutine.Yield();
            }

            onUpdating?.Invoke(1f);
        }
    }
}
