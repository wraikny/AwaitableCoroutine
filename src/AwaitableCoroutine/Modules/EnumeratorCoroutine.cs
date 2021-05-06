using System;
using System.Collections;

namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static async AwaitableCoroutine FromEnumerator(IEnumerator enumerator)
        {
            if (enumerator is null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            while(enumerator.MoveNext()) await Yield();
        }
    }

    public static class IEnumeratorExt
    {
        public static async AwaitableCoroutine ToAwaitableCoroutine(this IEnumerator enumerator)
        {
            if (enumerator is null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            while (enumerator.MoveNext()) await AwaitableCoroutine.Yield();
        }
    }
}
