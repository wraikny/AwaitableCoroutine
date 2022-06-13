using System;
using System.Collections.Generic;
using System.Linq;

namespace AwaitableCoroutine
{
    public partial class Coroutine
    {
        public static async Coroutine WaitAll(Coroutine c1, Coroutine c2)
        {
            while (!(c1.IsCompletedSuccessfully && c2.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled || c1.IsFaulted) Coroutine.ThrowChildCancel(c1, innerException: c1.Exception);
                if (c2.IsCanceled || c2.IsFaulted) Coroutine.ThrowChildCancel(c2, innerException: c2.Exception);
                await Yield();
            }
        }

        public static async Coroutine WaitAll(Coroutine c1, Coroutine c2, Coroutine c3)
        {
            while (!(c1.IsCompletedSuccessfully && c2.IsCompletedSuccessfully && c3.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled || c1.IsFaulted) Coroutine.ThrowChildCancel(c1, innerException: c1.Exception);
                if (c2.IsCanceled || c2.IsFaulted) Coroutine.ThrowChildCancel(c2, innerException: c2.Exception);
                if (c3.IsCanceled || c3.IsFaulted) Coroutine.ThrowChildCancel(c3, innerException: c3.Exception);
                await Yield();
            }
        }

        public static async Coroutine WaitAll(Coroutine c1, Coroutine c2, Coroutine c3, Coroutine c4)
        {
            while (!(c1.IsCompletedSuccessfully && c2.IsCompletedSuccessfully && c3.IsCompletedSuccessfully && c4.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled || c1.IsFaulted) Coroutine.ThrowChildCancel(c1, innerException: c1.Exception);
                if (c2.IsCanceled || c2.IsFaulted) Coroutine.ThrowChildCancel(c2, innerException: c2.Exception);
                if (c3.IsCanceled || c3.IsFaulted) Coroutine.ThrowChildCancel(c3, innerException: c3.Exception);
                if (c4.IsCanceled || c4.IsFaulted) Coroutine.ThrowChildCancel(c4, innerException: c4.Exception);
                await Yield();
            }
        }

        public static async Coroutine WaitAll(Coroutine c1, Coroutine c2, Coroutine c3, Coroutine c4, Coroutine c5)
        {
            while (!(c1.IsCompletedSuccessfully && c2.IsCompletedSuccessfully && c3.IsCompletedSuccessfully && c4.IsCompletedSuccessfully && c5.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled || c1.IsFaulted) Coroutine.ThrowChildCancel(c1, innerException: c1.Exception);
                if (c2.IsCanceled || c2.IsFaulted) Coroutine.ThrowChildCancel(c2, innerException: c2.Exception);
                if (c3.IsCanceled || c3.IsFaulted) Coroutine.ThrowChildCancel(c3, innerException: c3.Exception);
                if (c4.IsCanceled || c4.IsFaulted) Coroutine.ThrowChildCancel(c4, innerException: c4.Exception);
                if (c5.IsCanceled || c5.IsFaulted) Coroutine.ThrowChildCancel(c5, innerException: c5.Exception);
                await Yield();
            }
        }

        public static async Coroutine WaitAll(Coroutine c1, Coroutine c2, Coroutine c3, Coroutine c4, Coroutine c5, Coroutine c6)
        {
            while (!(c1.IsCompletedSuccessfully && c2.IsCompletedSuccessfully && c3.IsCompletedSuccessfully && c4.IsCompletedSuccessfully && c5.IsCompletedSuccessfully && c6.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled || c1.IsFaulted) Coroutine.ThrowChildCancel(c1, innerException: c1.Exception);
                if (c2.IsCanceled || c2.IsFaulted) Coroutine.ThrowChildCancel(c2, innerException: c2.Exception);
                if (c3.IsCanceled || c3.IsFaulted) Coroutine.ThrowChildCancel(c3, innerException: c3.Exception);
                if (c4.IsCanceled || c4.IsFaulted) Coroutine.ThrowChildCancel(c4, innerException: c4.Exception);
                if (c5.IsCanceled || c5.IsFaulted) Coroutine.ThrowChildCancel(c5, innerException: c5.Exception);
                if (c6.IsCanceled || c6.IsFaulted) Coroutine.ThrowChildCancel(c6, innerException: c6.Exception);
                await Yield();
            }
        }

        public static async Coroutine WaitAll(Coroutine c1, Coroutine c2, Coroutine c3, Coroutine c4, Coroutine c5, Coroutine c6, Coroutine c7)
        {
            while (!(c1.IsCompletedSuccessfully && c2.IsCompletedSuccessfully && c3.IsCompletedSuccessfully && c4.IsCompletedSuccessfully && c5.IsCompletedSuccessfully && c6.IsCompletedSuccessfully && c7.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled || c1.IsFaulted) Coroutine.ThrowChildCancel(c1, innerException: c1.Exception);
                if (c2.IsCanceled || c2.IsFaulted) Coroutine.ThrowChildCancel(c2, innerException: c2.Exception);
                if (c3.IsCanceled || c3.IsFaulted) Coroutine.ThrowChildCancel(c3, innerException: c3.Exception);
                if (c4.IsCanceled || c4.IsFaulted) Coroutine.ThrowChildCancel(c4, innerException: c4.Exception);
                if (c5.IsCanceled || c5.IsFaulted) Coroutine.ThrowChildCancel(c5, innerException: c5.Exception);
                if (c6.IsCanceled || c6.IsFaulted) Coroutine.ThrowChildCancel(c6, innerException: c6.Exception);
                if (c7.IsCanceled || c7.IsFaulted) Coroutine.ThrowChildCancel(c7, innerException: c7.Exception);
                await Yield();
            }
        }

        public static async Coroutine<(T1 result1, T2 result2)> WaitAll<T1, T2>(Coroutine<T1> c1, Coroutine<T2> c2)
        {
            while (!(c1.IsCompletedSuccessfully && c2.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled || c1.IsFaulted) Coroutine.ThrowChildCancel(c1, innerException: c1.Exception);
                if (c2.IsCanceled || c2.IsFaulted) Coroutine.ThrowChildCancel(c2, innerException: c2.Exception);
                await Yield();
            }
            return (c1.Result, c2.Result);
        }

        public static async Coroutine<(T1 result1, T2 result2, T3 result3)> WaitAll<T1, T2, T3>(Coroutine<T1> c1, Coroutine<T2> c2, Coroutine<T3> c3)
        {
            while (!(c1.IsCompletedSuccessfully && c2.IsCompletedSuccessfully && c3.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled || c1.IsFaulted) Coroutine.ThrowChildCancel(c1, innerException: c1.Exception);
                if (c2.IsCanceled || c2.IsFaulted) Coroutine.ThrowChildCancel(c2, innerException: c2.Exception);
                if (c3.IsCanceled || c3.IsFaulted) Coroutine.ThrowChildCancel(c3, innerException: c3.Exception);
                await Yield();
            }
            return (c1.Result, c2.Result, c3.Result);
        }

        public static async Coroutine<(T1 result1, T2 result2, T3 result3, T4 result4)> WaitAll<T1, T2, T3, T4>(Coroutine<T1> c1, Coroutine<T2> c2, Coroutine<T3> c3, Coroutine<T4> c4)
        {
            while (!(c1.IsCompletedSuccessfully && c2.IsCompletedSuccessfully && c3.IsCompletedSuccessfully && c4.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled || c1.IsFaulted) Coroutine.ThrowChildCancel(c1, innerException: c1.Exception);
                if (c2.IsCanceled || c2.IsFaulted) Coroutine.ThrowChildCancel(c2, innerException: c2.Exception);
                if (c3.IsCanceled || c3.IsFaulted) Coroutine.ThrowChildCancel(c3, innerException: c3.Exception);
                if (c4.IsCanceled || c4.IsFaulted) Coroutine.ThrowChildCancel(c4, innerException: c4.Exception);
                await Yield();
            }
            return (c1.Result, c2.Result, c3.Result, c4.Result);
        }

        public static async Coroutine<(T1 result1, T2 result2, T3 result3, T4 result4, T5 result5)> WaitAll<T1, T2, T3, T4, T5>(Coroutine<T1> c1, Coroutine<T2> c2, Coroutine<T3> c3, Coroutine<T4> c4, Coroutine<T5> c5)
        {
            while (!(c1.IsCompletedSuccessfully && c2.IsCompletedSuccessfully && c3.IsCompletedSuccessfully && c4.IsCompletedSuccessfully && c5.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled || c1.IsFaulted) Coroutine.ThrowChildCancel(c1, innerException: c1.Exception);
                if (c2.IsCanceled || c2.IsFaulted) Coroutine.ThrowChildCancel(c2, innerException: c2.Exception);
                if (c3.IsCanceled || c3.IsFaulted) Coroutine.ThrowChildCancel(c3, innerException: c3.Exception);
                if (c4.IsCanceled || c4.IsFaulted) Coroutine.ThrowChildCancel(c4, innerException: c4.Exception);
                if (c5.IsCanceled || c5.IsFaulted) Coroutine.ThrowChildCancel(c5, innerException: c5.Exception);
                await Yield();
            }
            return (c1.Result, c2.Result, c3.Result, c4.Result, c5.Result);
        }

        public static async Coroutine<(T1 result1, T2 result2, T3 result3, T4 result4, T5 result5, T6 result6)> WaitAll<T1, T2, T3, T4, T5, T6>(Coroutine<T1> c1, Coroutine<T2> c2, Coroutine<T3> c3, Coroutine<T4> c4, Coroutine<T5> c5, Coroutine<T6> c6)
        {
            while (!(c1.IsCompletedSuccessfully && c2.IsCompletedSuccessfully && c3.IsCompletedSuccessfully && c4.IsCompletedSuccessfully && c5.IsCompletedSuccessfully && c6.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled || c1.IsFaulted) Coroutine.ThrowChildCancel(c1, innerException: c1.Exception);
                if (c2.IsCanceled || c2.IsFaulted) Coroutine.ThrowChildCancel(c2, innerException: c2.Exception);
                if (c3.IsCanceled || c3.IsFaulted) Coroutine.ThrowChildCancel(c3, innerException: c3.Exception);
                if (c4.IsCanceled || c4.IsFaulted) Coroutine.ThrowChildCancel(c4, innerException: c4.Exception);
                if (c5.IsCanceled || c5.IsFaulted) Coroutine.ThrowChildCancel(c5, innerException: c5.Exception);
                if (c6.IsCanceled || c6.IsFaulted) Coroutine.ThrowChildCancel(c6, innerException: c6.Exception);
                await Yield();
            }
            return (c1.Result, c2.Result, c3.Result, c4.Result, c5.Result, c6.Result);
        }

        public static async Coroutine<(T1, T2, T3, T4, T5, T6, T7)> WaitAll<T1, T2, T3, T4, T5, T6, T7>(Coroutine<T1> c1, Coroutine<T2> c2, Coroutine<T3> c3, Coroutine<T4> c4, Coroutine<T5> c5, Coroutine<T6> c6, Coroutine<T7> c7)
        {
            while (!(c1.IsCompletedSuccessfully && c2.IsCompletedSuccessfully && c3.IsCompletedSuccessfully && c4.IsCompletedSuccessfully && c5.IsCompletedSuccessfully && c6.IsCompletedSuccessfully && c7.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled || c1.IsFaulted) Coroutine.ThrowChildCancel(c1, innerException: c1.Exception);
                if (c2.IsCanceled || c2.IsFaulted) Coroutine.ThrowChildCancel(c2, innerException: c2.Exception);
                if (c3.IsCanceled || c3.IsFaulted) Coroutine.ThrowChildCancel(c3, innerException: c3.Exception);
                if (c4.IsCanceled || c4.IsFaulted) Coroutine.ThrowChildCancel(c4, innerException: c4.Exception);
                if (c5.IsCanceled || c5.IsFaulted) Coroutine.ThrowChildCancel(c5, innerException: c5.Exception);
                if (c6.IsCanceled || c6.IsFaulted) Coroutine.ThrowChildCancel(c6, innerException: c6.Exception);
                if (c7.IsCanceled || c7.IsFaulted) Coroutine.ThrowChildCancel(c7, innerException: c7.Exception);
                await Yield();
            }
            return (c1.Result, c2.Result, c3.Result, c4.Result, c5.Result, c6.Result, c7.Result);
        }

        public static Coroutine WaitAll(ReadOnlySpan<Coroutine> span)
        {
            static async Coroutine Create(List<Coroutine> coroutines)
            {
                while (true)
                {
                    var insertIndex = 0;
                    for (var i = 0; i < coroutines.Count; i++)
                    {
                        var co = coroutines[i];

                        if (co.IsCanceled || co.IsFaulted)
                        {
                            Coroutine.ThrowChildCancel(co, innerException: co.Exception);
                        }

                        if (!co.IsCompletedSuccessfully)
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

            var coroutines = new List<Coroutine>(span.Length);
            foreach (var c in span)
            {
                coroutines.Add(c);
            }
            return Create(coroutines);
        }

        public static Coroutine<T[]> WaitAll<T>(ReadOnlySpan<Coroutine<T>> span)
        {
            static async Coroutine<T[]> Create(Coroutine<T>[] coroutines)
            {
                while (true)
                {
                    var IsCompletedSuccessfully = true;
                    foreach (var co in coroutines)
                    {
                        if (co.IsCanceled || co.IsFaulted)
                        {
                            Coroutine.ThrowChildCancel(co, innerException: co.Exception);
                        }

                        if (IsCompletedSuccessfully && !co.IsCompletedSuccessfully)
                        {
                            IsCompletedSuccessfully = false;
                        }
                    }
                    if (IsCompletedSuccessfully) break;

                    await Yield();
                }
                return coroutines.Select(c => c.Result).ToArray();
            }

            var coroutines = new Coroutine<T>[span.Length];
            span.CopyTo(coroutines);

            return Create(coroutines);
        }
    }
}
