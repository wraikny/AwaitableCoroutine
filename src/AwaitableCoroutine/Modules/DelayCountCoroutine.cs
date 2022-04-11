using System;

namespace AwaitableCoroutine
{
    public partial class Coroutine
    {
        public static async Coroutine DelayCount(int count, Action<int> onUpdating = null)
        {
            for (var i = 0; i < count; i++)
            {
                onUpdating?.Invoke(i);
                await Yield();
            }
        }
    }
}
