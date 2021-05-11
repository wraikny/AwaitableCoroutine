using System;
using System.Collections.Generic;
using System.Linq;

namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static async AwaitableCoroutine WaitAll(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2)
        {
            while (!(c1.IsCompleted && c2.IsCompleted))
            {
                if (c1.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c1);
                if (c2.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c2);
                await Yield();
            }
        }

        public static async AwaitableCoroutine WaitAll(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3)
        {
            while (!(c1.IsCompleted && c2.IsCompleted && c3.IsCompleted))
            {
                if (c1.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c1);
                if (c2.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c2);
                if (c3.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c3);
                await Yield();
            }
        }

        public static async AwaitableCoroutine WaitAll(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4)
        {
            while (!(c1.IsCompleted && c2.IsCompleted && c3.IsCompleted && c4.IsCompleted))
            {
                if (c1.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c1);
                if (c2.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c2);
                if (c3.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c3);
                if (c4.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c4);
                await Yield();
            }
        }

        public static async AwaitableCoroutine WaitAll(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4, AwaitableCoroutineBase c5)
        {
            while (!(c1.IsCompleted && c2.IsCompleted && c3.IsCompleted && c4.IsCompleted && c5.IsCompleted))
            {
                if (c1.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c1);
                if (c2.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c2);
                if (c3.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c3);
                if (c4.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c4);
                if (c5.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c5);
                await Yield();
            }
        }

        public static async AwaitableCoroutine WaitAll(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4, AwaitableCoroutineBase c5, AwaitableCoroutineBase c6)
        {
            while (!(c1.IsCompleted && c2.IsCompleted && c3.IsCompleted && c4.IsCompleted && c5.IsCompleted && c6.IsCompleted))
            {
                if (c1.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c1);
                if (c2.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c2);
                if (c3.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c3);
                if (c4.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c4);
                if (c5.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c5);
                if (c6.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c6);
                await Yield();
            }
        }

        public static async AwaitableCoroutine WaitAll(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4, AwaitableCoroutineBase c5, AwaitableCoroutineBase c6, AwaitableCoroutineBase c7)
        {
            while (!(c1.IsCompleted && c2.IsCompleted && c3.IsCompleted && c4.IsCompleted && c5.IsCompleted && c6.IsCompleted && c7.IsCompleted))
            {
                if (c1.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c1);
                if (c2.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c2);
                if (c3.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c3);
                if (c4.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c4);
                if (c5.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c5);
                if (c6.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c6);
                if (c7.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(c7);
                await Yield();
            }
        }

        public static async AwaitableCoroutine<(T1 result1, T2 result2)> WaitAll<T1, T2>(AwaitableCoroutine<T1> c1, AwaitableCoroutine<T2> c2)
        {
            while (!(c1.IsCompleted && c2.IsCompleted))
            {
                if (c1.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T1>>(c1);
                if (c2.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T2>>(c2);
                await Yield();
            }
            return (c1.Result, c2.Result);
        }

        public static async AwaitableCoroutine<(T1 result1, T2 result2, T3 result3)> WaitAll<T1, T2, T3>(AwaitableCoroutine<T1> c1, AwaitableCoroutine<T2> c2, AwaitableCoroutine<T3> c3)
        {
            while (!(c1.IsCompleted && c2.IsCompleted && c3.IsCompleted))
            {
                if (c1.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T1>>(c1);
                if (c2.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T2>>(c2);
                if (c3.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T3>>(c3);
                await Yield();
            }
            return (c1.Result, c2.Result, c3.Result);
        }

        public static async AwaitableCoroutine<(T1 result1, T2 result2, T3 result3, T4 result4)> WaitAll<T1, T2, T3, T4>(AwaitableCoroutine<T1> c1, AwaitableCoroutine<T2> c2, AwaitableCoroutine<T3> c3, AwaitableCoroutine<T4> c4)
        {
            while (!(c1.IsCompleted && c2.IsCompleted && c3.IsCompleted && c4.IsCompleted))
            {
                if (c1.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T1>>(c1);
                if (c2.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T2>>(c2);
                if (c3.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T3>>(c3);
                if (c4.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T4>>(c4);
                await Yield();
            }
            return (c1.Result, c2.Result, c3.Result, c4.Result);
        }

        public static async AwaitableCoroutine<(T1 result1, T2 result2, T3 result3, T4 result4, T5 result5)> WaitAll<T1, T2, T3, T4, T5>(AwaitableCoroutine<T1> c1, AwaitableCoroutine<T2> c2, AwaitableCoroutine<T3> c3, AwaitableCoroutine<T4> c4, AwaitableCoroutine<T5> c5)
        {
            while (!(c1.IsCompleted && c2.IsCompleted && c3.IsCompleted && c4.IsCompleted && c5.IsCompleted))
            {
                if (c1.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T1>>(c1);
                if (c2.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T2>>(c2);
                if (c3.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T3>>(c3);
                if (c4.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T4>>(c4);
                if (c5.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T5>>(c5);
                await Yield();
            }
            return (c1.Result, c2.Result, c3.Result, c4.Result, c5.Result);
        }

        public static async AwaitableCoroutine<(T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6)> WaitAll<T1, T2, T3, T4, T5, T6>(AwaitableCoroutine<T1> c1, AwaitableCoroutine<T2> c2, AwaitableCoroutine<T3> c3, AwaitableCoroutine<T4> c4, AwaitableCoroutine<T5> c5, AwaitableCoroutine<T6> c6)
        {
            while (!(c1.IsCompleted && c2.IsCompleted && c3.IsCompleted && c4.IsCompleted && c5.IsCompleted && c6.IsCompleted))
            {
                if (c1.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T1>>(c1);
                if (c2.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T2>>(c2);
                if (c3.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T3>>(c3);
                if (c4.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T4>>(c4);
                if (c5.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T5>>(c5);
                if (c6.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T6>>(c6);
                await Yield();
            }
            return (c1.Result, c2.Result, c3.Result, c4.Result, c5.Result, c6.Result);
        }

        public static async AwaitableCoroutine<(T1, T2, T3, T4, T5, T6, T7)> WaitAll<T1, T2, T3, T4, T5, T6, T7>(AwaitableCoroutine<T1> c1, AwaitableCoroutine<T2> c2, AwaitableCoroutine<T3> c3, AwaitableCoroutine<T4> c4, AwaitableCoroutine<T5> c5, AwaitableCoroutine<T6> c6, AwaitableCoroutine<T7> c7)
        {
            while (!(c1.IsCompleted && c2.IsCompleted && c3.IsCompleted && c4.IsCompleted && c5.IsCompleted && c6.IsCompleted && c7.IsCompleted))
            {
                if (c1.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T1>>(c1);
                if (c2.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T2>>(c2);
                if (c3.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T3>>(c3);
                if (c4.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T4>>(c4);
                if (c5.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T5>>(c5);
                if (c6.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T6>>(c6);
                if (c7.IsCanceled) AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T7>>(c7);
                await Yield();
            }
            return (c1.Result, c2.Result, c3.Result, c4.Result, c5.Result, c6.Result, c7.Result);
        }

        public static AwaitableCoroutine WaitAll(ReadOnlySpan<AwaitableCoroutineBase> span)
        {
            static async AwaitableCoroutine Create(List<AwaitableCoroutineBase> coroutines)
            {
                while (true)
                {
                    var insertIndex = 0;
                    for (var i = 0; i < coroutines.Count; i++)
                    {
                        var co = coroutines[i];

                        if (co.IsCanceled)
                        {
                            AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutineBase>(co);
                        }

                        if (!co.IsCompleted)
                        {
                            coroutines[insertIndex] = co;
                            insertIndex++;
                        }
                    }

                    coroutines.RemoveRange(insertIndex, coroutines.Count - insertIndex);

                    if (coroutines.Count == 0) return;

                    await Yield();
                }
            }

            var coroutines = new List<AwaitableCoroutineBase>(span.Length);
            foreach (var c in span)
            {
                coroutines.Add(c);
            }
            return Create(coroutines);
        }

        public static AwaitableCoroutine<T[]> WaitAll<T>(ReadOnlySpan<AwaitableCoroutine<T>> span)
        {
            static async AwaitableCoroutine<T[]> Create(AwaitableCoroutine<T>[] coroutines)
            {
                while (true)
                {
                    var isCompleted = true;
                    foreach (var co in coroutines)
                    {
                        if (co.IsCanceled)
                        {
                            AwaitableCoroutine.ThrowChildCancel<AwaitableCoroutine<T>>(co);
                        }

                        if (isCompleted && !co.IsCompleted)
                        {
                            isCompleted = false;
                        }
                    }
                    if (isCompleted) break;

                    await Yield();
                }
                return coroutines.Select(c => c.Result).ToArray();
            }

            var coroutines = new AwaitableCoroutine<T>[span.Length];
            span.CopyTo(coroutines);

            return Create(coroutines);
        }
    }
}
