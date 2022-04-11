using System.Collections;

namespace AwaitableCoroutine
{
    public partial class Coroutine
    {
        public static async Coroutine FromEnumerator(IEnumerator enumerator)
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
}
