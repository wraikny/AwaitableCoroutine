using System;
using System.Collections.Generic;
using System.Linq;

namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static async AwaitableCoroutine WaitAll(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();

                if (c1.IsCompleted && c2.IsCompleted) break;

                await Yield();
            }
        }

        public static async AwaitableCoroutine WaitAll(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();
                c3.MoveNext();

                if (c1.IsCompleted && c2.IsCompleted && c3.IsCompleted) break;

                await Yield();
            }
        }

        public static async AwaitableCoroutine WaitAll(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();
                c3.MoveNext();
                c4.MoveNext();

                if (c1.IsCompleted && c2.IsCompleted && c3.IsCompleted && c4.IsCompleted) break;

                await Yield();
            }
        }

        public static async AwaitableCoroutine WaitAll(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4, AwaitableCoroutineBase c5)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();
                c3.MoveNext();
                c4.MoveNext();
                c5.MoveNext();

                if (c1.IsCompleted && c2.IsCompleted && c3.IsCompleted && c4.IsCompleted && c5.IsCompleted) break;

                await Yield();
            }
        }

        public static async AwaitableCoroutine WaitAll(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4, AwaitableCoroutineBase c5, AwaitableCoroutineBase c6)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();
                c3.MoveNext();
                c4.MoveNext();
                c5.MoveNext();
                c6.MoveNext();

                if (c1.IsCompleted && c2.IsCompleted && c3.IsCompleted && c4.IsCompleted && c5.IsCompleted && c6.IsCompleted) break;

                await Yield();
            }
        }

        public static async AwaitableCoroutine WaitAll(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4, AwaitableCoroutineBase c5, AwaitableCoroutineBase c6, AwaitableCoroutineBase c7)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();
                c3.MoveNext();
                c4.MoveNext();
                c5.MoveNext();
                c6.MoveNext();
                c7.MoveNext();

                if (c1.IsCompleted && c2.IsCompleted && c3.IsCompleted && c4.IsCompleted && c5.IsCompleted && c6.IsCompleted && c7.IsCompleted) break;

                await Yield();
            }
        }

        public static async AwaitableCoroutine<(T1 result1, T2 result2)> WaitAll<T1, T2>(AwaitableCoroutine<T1> c1, AwaitableCoroutine<T2> c2)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();

                if (c1.IsCompleted && c2.IsCompleted) break;

                await Yield();
            }

            return (c1.Result, c2.Result);
        }

        public static async AwaitableCoroutine<(T1 result1, T2 result2, T3 result3)> WaitAll<T1, T2, T3>(AwaitableCoroutine<T1> c1, AwaitableCoroutine<T2> c2, AwaitableCoroutine<T3> c3)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();
                c3.MoveNext();

                if (c1.IsCompleted && c2.IsCompleted && c3.IsCompleted) break;

                await Yield();
            }

            return (c1.Result, c2.Result, c3.Result);
        }

        public static async AwaitableCoroutine<(T1 result1, T2 result2, T3 result3, T4 result4)> WaitAll<T1, T2, T3, T4>(AwaitableCoroutine<T1> c1, AwaitableCoroutine<T2> c2, AwaitableCoroutine<T3> c3, AwaitableCoroutine<T4> c4)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();
                c3.MoveNext();
                c4.MoveNext();

                if (c1.IsCompleted && c2.IsCompleted && c3.IsCompleted && c4.IsCompleted) break;

                await Yield();
            }

            return (c1.Result, c2.Result, c3.Result, c4.Result);
        }

        public static async AwaitableCoroutine<(T1 result1, T2 result2, T3 result3, T4 result4, T5 result5)> WaitAll<T1, T2, T3, T4, T5>(AwaitableCoroutine<T1> c1, AwaitableCoroutine<T2> c2, AwaitableCoroutine<T3> c3, AwaitableCoroutine<T4> c4, AwaitableCoroutine<T5> c5)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();
                c3.MoveNext();
                c4.MoveNext();
                c5.MoveNext();

                if (c1.IsCompleted && c2.IsCompleted && c3.IsCompleted && c4.IsCompleted && c5.IsCompleted) break;

                await Yield();
            }

            return (c1.Result, c2.Result, c3.Result, c4.Result, c5.Result);
        }

        public static async AwaitableCoroutine<(T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6)> WaitAll<T1, T2, T3, T4, T5, T6>(AwaitableCoroutine<T1> c1, AwaitableCoroutine<T2> c2, AwaitableCoroutine<T3> c3, AwaitableCoroutine<T4> c4, AwaitableCoroutine<T5> c5, AwaitableCoroutine<T6> c6)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();
                c3.MoveNext();
                c4.MoveNext();
                c5.MoveNext();
                c6.MoveNext();

                if (c1.IsCompleted && c2.IsCompleted && c3.IsCompleted && c4.IsCompleted && c5.IsCompleted && c6.IsCompleted) break;

                await Yield();
            }

            return (c1.Result, c2.Result, c3.Result, c4.Result, c5.Result, c6.Result);
        }

        public static async AwaitableCoroutine<(T1, T2, T3, T4, T5, T6, T7)> WaitAll<T1, T2, T3, T4, T5, T6, T7>(AwaitableCoroutine<T1> c1, AwaitableCoroutine<T2> c2, AwaitableCoroutine<T3> c3, AwaitableCoroutine<T4> c4, AwaitableCoroutine<T5> c5, AwaitableCoroutine<T6> c6, AwaitableCoroutine<T7> c7)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();
                c3.MoveNext();
                c4.MoveNext();
                c5.MoveNext();
                c6.MoveNext();
                c7.MoveNext();

                if (c1.IsCompleted && c2.IsCompleted && c3.IsCompleted && c4.IsCompleted && c5.IsCompleted && c6.IsCompleted && c7.IsCompleted) break;

                await Yield();
            }

            return (c1.Result, c2.Result, c3.Result, c4.Result, c5.Result, c6.Result, c7.Result);
        }

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
                .Select(() =>
                {
                    return coroutines.Select(c => c.Result).ToArray();
                });
        }
    }
}
