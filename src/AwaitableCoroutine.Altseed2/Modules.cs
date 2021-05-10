using System;

using Altseed2;

using AC = AwaitableCoroutine.AwaitableCoroutine;

namespace AwaitableCoroutine.Altseed2
{
    public static class AwaitableCoroutine
    {
        public static async AC DelaySecond(float second)
        {
            for (var i = 0.0f; i < second; i += Engine.DeltaSecond) await AC.Yield();
        }
    }
}
