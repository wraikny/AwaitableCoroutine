using System;

namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static async AwaitableCoroutine DelayCount(int count, Action<int> onUpdating = null)
        {
            for (var i = 0; i < count; i++)
            {
                onUpdating?.Invoke(i);
                await Yield();
            }
        }
    }
}
