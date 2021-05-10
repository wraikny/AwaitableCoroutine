using System;

namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static AwaitableCoroutine DelayCount(int count, Action onUpdating = null, Action onCompleted = null)
        {
            var i = 0;
            return While(() => i++ < count, onCompleted: onCompleted);
        }

        public static AwaitableCoroutine<T> DelayCount<T>(int count, Func<T> generator, Action onUpdating = null, Action<T> onCompleted = null)
        {
            var i = 0;
            return While<T>(() => i++ < count, generator, onUpdating: onUpdating, onCompleted: onCompleted);
        }
    }
}
