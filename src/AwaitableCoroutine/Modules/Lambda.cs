using System;

namespace AwaitableCoroutine
{
    public partial class Coroutine
    {
        public static Coroutine Lambda(Func<Coroutine> lambda)
        {
            if (lambda is null)
            {
                ThrowHelper.ArgNull(nameof(lambda));
            }

            return lambda.Invoke();
        }

        public static Coroutine<T> Lambda<T>(Func<Coroutine<T>> lambda)
        {
            if (lambda is null)
            {
                ThrowHelper.ArgNull(nameof(lambda));
            }

            return lambda.Invoke();
        }
    }
}
