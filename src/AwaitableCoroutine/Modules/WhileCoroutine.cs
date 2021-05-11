using System;

namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static async AwaitableCoroutine While(Func<bool> predicate)
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
