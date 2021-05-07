using System;
using System.Collections.Generic;
using System.Linq;

namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static AwaitableCoroutine WaitAny(ReadOnlySpan<AwaitableCoroutineBase> span)
        {
            var coroutines = new AwaitableCoroutineBase[span.Length];
            span.CopyTo(coroutines);

            return Until(() => {
                foreach (var c in coroutines)
                {
                    c.MoveNext();
                    if (c.IsCompleted) return true;
                }

                return false;
            });
        }

        public static AwaitableCoroutine<T> WaitAny<T>(ReadOnlySpan<AwaitableCoroutine<T>> span)
        {
            var coroutines = new AwaitableCoroutine<T>[span.Length];
            span.CopyTo(coroutines);
            
            T result = default;

            return Until(() => {
                foreach (var c in coroutines)
                {
                    c.MoveNext();
                    if (c.IsCompleted)
                    {
                        result = c.Result;
                        return true;
                    }
                }

                return false;
            }).Select(() => result);
        }
    }
}
