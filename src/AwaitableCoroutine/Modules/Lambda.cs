using System;

namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static AwaitableCoroutine Lambda(Func<AwaitableCoroutine> lambda)
        {
            if (lambda is null)
            {
                ThrowHelper.ArgNull(nameof(lambda));
            }

            return lambda.Invoke();
        }

        public static AwaitableCoroutine<T> Lambda<T>(Func<AwaitableCoroutine<T>> lambda)
        {
            if (lambda is null)
            {
                ThrowHelper.ArgNull(nameof(lambda));
            }

            return lambda.Invoke();
        }
    }
}
