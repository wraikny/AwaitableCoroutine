using System;
using System.Collections.Generic;
using System.Linq;

namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static async AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2)
        {
            while (!(c1.IsCompleted || c2.IsCompleted)) await Yield();
        }

        public static async AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3)
        {
            while (!(c1.IsCompleted || c2.IsCompleted || c3.IsCompleted)) await Yield();
        }

        public static async AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4)
        {
            while (!(c1.IsCompleted || c2.IsCompleted || c3.IsCompleted || c4.IsCompleted)) await Yield();
        }

        public static async AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4, AwaitableCoroutineBase c5)
        {
            while (!(c1.IsCompleted || c2.IsCompleted || c3.IsCompleted || c4.IsCompleted || c5.IsCompleted)) await Yield();
        }

        public static async AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4, AwaitableCoroutineBase c5, AwaitableCoroutineBase c6)
        {
            while (!(c1.IsCompleted || c2.IsCompleted || c3.IsCompleted || c4.IsCompleted || c5.IsCompleted || c6.IsCompleted)) await Yield();
        }

        public static async AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4, AwaitableCoroutineBase c5, AwaitableCoroutineBase c6, AwaitableCoroutineBase c7)
        {
            while (!(c1.IsCompleted || c2.IsCompleted || c3.IsCompleted || c4.IsCompleted || c5.IsCompleted || c6.IsCompleted || c7.IsCompleted)) await Yield();
        }

        public static async AwaitableCoroutine<T> WaitAny<T>(AwaitableCoroutine<T> c1, AwaitableCoroutine<T> c2)
        {
            while (true)
            {
                if (c1.IsCompleted) return c1.Result;
                if (c2.IsCompleted) return c2.Result;

                await Yield();
            }
        }

        public static async AwaitableCoroutine<T> WaitAny<T>(AwaitableCoroutine<T> c1, AwaitableCoroutine<T> c2, AwaitableCoroutine<T> c3)
        {
            while (true)
            {
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
            static async AwaitableCoroutine Create(AwaitableCoroutineBase[] coroutines)
            {
                while (coroutines.All(c => !c.IsCompleted))
                {
                    await Yield();
                }
            }

            var coroutines = new AwaitableCoroutineBase[span.Length];
            span.CopyTo(coroutines);
            return Create(coroutines);
        }

        public static AwaitableCoroutine<T> WaitAny<T>(ReadOnlySpan<AwaitableCoroutine<T>> span)
        {
            async AwaitableCoroutine<T> Create(AwaitableCoroutine<T>[] coroutines)
            {
                while (true)
                {
                    if (coroutines.FirstOrDefault(c => c.IsCompleted) is { } c)
                    {
                        return c.Result;
                    }

                    await Yield();
                }
            }

            var coroutines = new AwaitableCoroutine<T>[span.Length];
            span.CopyTo(coroutines);

            return Create(coroutines);
        }
    }
}
