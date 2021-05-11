using System;

using Altseed2;

namespace AwaitableCoroutine.Altseed2
{
    public static class Altseed2Coroutine
    {
        public static async AwaitableCoroutine DelaySecond(float second)
        {
            for (var i = 0.0f; i < second; i += Engine.DeltaSecond) await AwaitableCoroutine.Yield();
        }
    }
}
