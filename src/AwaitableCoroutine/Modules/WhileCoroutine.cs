using System;

namespace AwaitableCoroutine
{
    public partial class Coroutine
    {
        public static async Coroutine While(Func<bool> predicate)
        {
            if (predicate is null)
            {
                ThrowHelper.ArgNull(nameof(predicate));
            }

            while (predicate())
            {
                await Yield();
            }
        }
    }
}
