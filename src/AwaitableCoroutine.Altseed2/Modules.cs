using System;
using Altseed2;

using AC = AwaitableCoroutine.AwaitableCoroutine;

namespace AwaitableCoroutine.Altseed2
{
    public static class AwaitableCoroutine
    {
        public static async AC DelaySecond(float targetSecond)
        {
            float count = 0.0f;

            while (count < targetSecond)
            {
                count += Engine.DeltaSecond;
                await AC.Yield();
            }
        }
    }
}
