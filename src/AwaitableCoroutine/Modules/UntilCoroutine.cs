using System;

namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static async AwaitableCoroutine While(Func<bool> predicate, Action onUpdating = null, Action onCompleted = null)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            while (predicate())
            {
                onUpdating?.Invoke();
                await Yield();
            }
            
            onCompleted?.Invoke();
        }
    }
}
