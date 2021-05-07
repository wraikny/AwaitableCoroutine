using System;
using System.Collections.Generic;
using System.Linq;

namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static async AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();

                if (c1.IsCompleted || c2.IsCompleted) break;

                await Yield();
            }
        }

        public static async AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();
                c3.MoveNext();

                if (c1.IsCompleted || c2.IsCompleted || c3.IsCompleted) break;

                await Yield();
            }
        }

        public static async AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();
                c3.MoveNext();
                c4.MoveNext();

                if (c1.IsCompleted || c2.IsCompleted || c3.IsCompleted || c4.IsCompleted) break;

                await Yield();
            }
        }

        public static async AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4, AwaitableCoroutineBase c5)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();
                c3.MoveNext();
                c4.MoveNext();
                c5.MoveNext();

                if (c1.IsCompleted || c2.IsCompleted || c3.IsCompleted || c4.IsCompleted || c5.IsCompleted) break;

                await Yield();
            }
        }

        public static async AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4, AwaitableCoroutineBase c5, AwaitableCoroutineBase c6)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();
                c3.MoveNext();
                c4.MoveNext();
                c5.MoveNext();
                c6.MoveNext();

                if (c1.IsCompleted || c2.IsCompleted || c3.IsCompleted || c4.IsCompleted || c5.IsCompleted || c6.IsCompleted) break;

                await Yield();
            }
        }

        public static async AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4, AwaitableCoroutineBase c5, AwaitableCoroutineBase c6, AwaitableCoroutineBase c7)
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

                if (c1.IsCompleted || c2.IsCompleted || c3.IsCompleted || c4.IsCompleted || c5.IsCompleted || c6.IsCompleted || c7.IsCompleted) break;

                await Yield();
            }
        }

        public static async AwaitableCoroutine<T> WaitAny<T>(AwaitableCoroutine<T> c1, AwaitableCoroutine<T> c2)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();

                if (c1.IsCompleted) return c1.Result;
                if (c2.IsCompleted) return c2.Result;

                await Yield();
            }
        }

        public static async AwaitableCoroutine<T> WaitAny<T>(AwaitableCoroutine<T> c1, AwaitableCoroutine<T> c2, AwaitableCoroutine<T> c3)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();
                c3.MoveNext();

                if (c1.IsCompleted) return c1.Result;
                if (c2.IsCompleted) return c2.Result;
                if (c3.IsCompleted) return c3.Result;

                await Yield();
            }
        }

        public static async AwaitableCoroutine<T> WaitAny<T>(AwaitableCoroutine<T> c1, AwaitableCoroutine<T> c2, AwaitableCoroutine<T> c3, AwaitableCoroutine<T> c4)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();
                c3.MoveNext();
                c4.MoveNext();

                if (c1.IsCompleted) return c1.Result;
                if (c2.IsCompleted) return c2.Result;
                if (c3.IsCompleted) return c3.Result;
                if (c4.IsCompleted) return c4.Result;

                await Yield();
            }
        }

        public static async AwaitableCoroutine<T> WaitAny<T>(AwaitableCoroutine<T> c1, AwaitableCoroutine<T> c2, AwaitableCoroutine<T> c3, AwaitableCoroutine<T> c4, AwaitableCoroutine<T> c5)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();
                c3.MoveNext();
                c4.MoveNext();
                c5.MoveNext();

                if (c1.IsCompleted) return c1.Result;
                if (c2.IsCompleted) return c2.Result;
                if (c3.IsCompleted) return c3.Result;
                if (c4.IsCompleted) return c4.Result;
                if (c5.IsCompleted) return c5.Result;

                await Yield();
            }
        }

        public static async AwaitableCoroutine<T> WaitAny<T>(AwaitableCoroutine<T> c1, AwaitableCoroutine<T> c2, AwaitableCoroutine<T> c3, AwaitableCoroutine<T> c4, AwaitableCoroutine<T> c5, AwaitableCoroutine<T> c6)
        {
            while (true)
            {
                c1.MoveNext();
                c2.MoveNext();
                c3.MoveNext();
                c4.MoveNext();
                c5.MoveNext();
                c6.MoveNext();

                if (c1.IsCompleted) return c1.Result;
                if (c2.IsCompleted) return c2.Result;
                if (c3.IsCompleted) return c3.Result;
                if (c4.IsCompleted) return c4.Result;
                if (c5.IsCompleted) return c5.Result;
                if (c6.IsCompleted) return c6.Result;

                await Yield();
            }
        }

        public static async AwaitableCoroutine<T> WaitAny<T>(AwaitableCoroutine<T> c1, AwaitableCoroutine<T> c2, AwaitableCoroutine<T> c3, AwaitableCoroutine<T> c4, AwaitableCoroutine<T> c5, AwaitableCoroutine<T> c6, AwaitableCoroutine<T> c7)
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

                if (c1.IsCompleted) return c1.Result;
                if (c2.IsCompleted) return c2.Result;
                if (c3.IsCompleted) return c3.Result;
                if (c4.IsCompleted) return c4.Result;
                if (c5.IsCompleted) return c5.Result;
                if (c6.IsCompleted) return c6.Result;
                if (c7.IsCompleted) return c7.Result;

                await Yield();
            }
        }

        public static AwaitableCoroutine WaitAny(ReadOnlySpan<AwaitableCoroutineBase> span)
        {
            var coroutines = new AwaitableCoroutineBase[span.Length];
            span.CopyTo(coroutines);

            return Until(() =>
            {
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

            return Until(() =>
            {
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
