using System;
using System.Collections;

namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static AwaitableCoroutine FromEnumerator(IEnumerator enumerator)
        {
            if (enumerator is null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            return While(enumerator.MoveNext);
        }
    }

    public static class IEnumeratorExt
    {
        public static AwaitableCoroutine ToAwaitable(this IEnumerator enumerator)
        {
            if (enumerator is null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            return AwaitableCoroutine.While(enumerator.MoveNext);
        }
    }
}
