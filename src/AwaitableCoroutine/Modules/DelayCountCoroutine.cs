using System;

namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static async AwaitableCoroutine DelayCount(int count)
        {
            for (var i = 0; i < count; i++) await Yield();
        }
    }
}
