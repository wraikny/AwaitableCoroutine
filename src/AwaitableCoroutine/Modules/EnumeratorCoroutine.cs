using System.Collections;

namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static async AwaitableCoroutine FromEnumerator(IEnumerator enumerator)
        {
            if (enumerator is null)
            {
                ThrowHelper.ArgNull(nameof(enumerator));
            }

            while (enumerator.MoveNext())
            {
                await Yield();
            }
        }
    }

    public static class IEnumeratorExt
    {
        public static AwaitableCoroutine ToAwaitable(this IEnumerator enumerator)
        {
            return AwaitableCoroutine.FromEnumerator(enumerator);
        }
    }
}
