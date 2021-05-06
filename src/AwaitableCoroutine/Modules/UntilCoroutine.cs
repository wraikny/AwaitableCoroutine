using System;

namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static async AwaitableCoroutine Until(Func<bool> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            while (!predicate())
            {
                await Yield();
            }
        }
    }
}
