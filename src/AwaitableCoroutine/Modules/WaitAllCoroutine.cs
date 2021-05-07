using System;
using System.Collections.Generic;
using System.Linq;

namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static AwaitableCoroutine WaitAll(ReadOnlySpan<AwaitableCoroutineBase> span)
        {
            var coroutines = new List<AwaitableCoroutineBase>(span.Length);
            foreach (var c in span)
            {
                coroutines.Add(c);
            }

            return While(() =>
            {
                coroutines.RemoveAll(c =>
                {
                    c.MoveNext();
                    return c.IsCompleted;
                });
                return coroutines.Count > 0;
            });
        }

        public static AwaitableCoroutine<T[]> WaitAll<T>(ReadOnlySpan<AwaitableCoroutine<T>> span)
        {
            var coroutines = new AwaitableCoroutine<T>[span.Length];
            span.CopyTo(coroutines);

            return Until(() =>
            {
                var completed = true;
                foreach (var c in coroutines)
                {
                    if (c.IsCompleted) continue;
                    c.MoveNext();
                    completed &= c.IsCompleted;
                }
                return completed;
            })
                .Select(() => {
                    return coroutines.Select(c => c.Result).ToArray();
                });
        }
    }
}
