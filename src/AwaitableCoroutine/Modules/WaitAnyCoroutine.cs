using System;

namespace AwaitableCoroutine
{
    public partial class Coroutine
    {
        public static async Coroutine WaitAny(CoroutineBase c1, CoroutineBase c2)
        {
            while (!(c1.IsCompletedSuccessfully || c2.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled && c2.IsCanceled)
                {
                    Coroutine.ThrowChildrenCancel(new CoroutineBase[] { c1, c2 });
                }
                await Yield();
            }
        }

        public static async Coroutine WaitAny(CoroutineBase c1, CoroutineBase c2, CoroutineBase c3)
        {
            while (!(c1.IsCompletedSuccessfully || c2.IsCompletedSuccessfully || c3.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled && c2.IsCanceled && c3.IsCanceled)
                {
                    Coroutine.ThrowChildrenCancel(new CoroutineBase[] { c1, c2, c3 });
                }
                await Yield();
            }
        }

        public static async Coroutine WaitAny(CoroutineBase c1, CoroutineBase c2, CoroutineBase c3, CoroutineBase c4)
        {
            while (!(c1.IsCompletedSuccessfully || c2.IsCompletedSuccessfully || c3.IsCompletedSuccessfully || c4.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled && c2.IsCanceled && c3.IsCanceled && c4.IsCanceled)
                {
                    Coroutine.ThrowChildrenCancel(new CoroutineBase[] { c1, c2, c3, c4 });
                }
                await Yield();
            }
        }

        public static async Coroutine WaitAny(CoroutineBase c1, CoroutineBase c2, CoroutineBase c3, CoroutineBase c4, CoroutineBase c5)
        {
            while (!(c1.IsCompletedSuccessfully || c2.IsCompletedSuccessfully || c3.IsCompletedSuccessfully || c4.IsCompletedSuccessfully || c5.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled && c2.IsCanceled && c3.IsCanceled && c4.IsCanceled && c5.IsCanceled)
                {
                    Coroutine.ThrowChildrenCancel(new CoroutineBase[] { c1, c2, c3, c4, c5 });
                }
                await Yield();
            }
        }

        public static async Coroutine WaitAny(CoroutineBase c1, CoroutineBase c2, CoroutineBase c3, CoroutineBase c4, CoroutineBase c5, CoroutineBase c6)
        {
            while (!(c1.IsCompletedSuccessfully || c2.IsCompletedSuccessfully || c3.IsCompletedSuccessfully || c4.IsCompletedSuccessfully || c5.IsCompletedSuccessfully || c6.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled && c2.IsCanceled && c3.IsCanceled && c4.IsCanceled && c5.IsCanceled && c6.IsCanceled)
                {
                    Coroutine.ThrowChildrenCancel(new CoroutineBase[] { c1, c2, c3, c4, c5, c6 });
                }
                await Yield();
            }
        }

        public static async Coroutine WaitAny(CoroutineBase c1, CoroutineBase c2, CoroutineBase c3, CoroutineBase c4, CoroutineBase c5, CoroutineBase c6, CoroutineBase c7)
        {
            while (!(c1.IsCompletedSuccessfully || c2.IsCompletedSuccessfully || c3.IsCompletedSuccessfully || c4.IsCompletedSuccessfully || c5.IsCompletedSuccessfully || c6.IsCompletedSuccessfully || c7.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled && c2.IsCanceled && c3.IsCanceled && c4.IsCanceled && c5.IsCanceled && c6.IsCanceled && c7.IsCanceled)
                {
                    Coroutine.ThrowChildrenCancel(new CoroutineBase[] { c1, c2, c3, c4, c5, c6, c7 });
                }
                await Yield();
            }
        }

        public static async Coroutine<T> WaitAny<T>(Coroutine<T> c1, Coroutine<T> c2)
        {
            while (true)
            {
                if (c1.IsCompletedSuccessfully) return c1.Result;
                if (c2.IsCompletedSuccessfully) return c2.Result;

                if (c1.IsCanceled && c2.IsCanceled)
                {
                    Coroutine.ThrowChildrenCancel(new Coroutine<T>[] { c1, c2 });
                }

                await Yield();
            }
        }

        public static async Coroutine<T> WaitAny<T>(Coroutine<T> c1, Coroutine<T> c2, Coroutine<T> c3)
        {
            while (true)
            {
                if (c1.IsCompletedSuccessfully) return c1.Result;
                if (c2.IsCompletedSuccessfully) return c2.Result;
                if (c3.IsCompletedSuccessfully) return c3.Result;

                if (c1.IsCanceled && c2.IsCanceled && c3.IsCanceled)
                {
                    Coroutine.ThrowChildrenCancel(new Coroutine<T>[] { c1, c2, c3 });
                }

                await Yield();
            }
        }

        public static async Coroutine<T> WaitAny<T>(Coroutine<T> c1, Coroutine<T> c2, Coroutine<T> c3, Coroutine<T> c4)
        {
            while (true)
            {
                if (c1.IsCompletedSuccessfully) return c1.Result;
                if (c2.IsCompletedSuccessfully) return c2.Result;
                if (c3.IsCompletedSuccessfully) return c3.Result;
                if (c4.IsCompletedSuccessfully) return c4.Result;

                if (c1.IsCanceled && c2.IsCanceled && c3.IsCanceled && c4.IsCanceled)
                {
                    Coroutine.ThrowChildrenCancel(new Coroutine<T>[] { c1, c2, c3, c4 });
                }

                await Yield();
            }
        }

        public static async Coroutine<T> WaitAny<T>(Coroutine<T> c1, Coroutine<T> c2, Coroutine<T> c3, Coroutine<T> c4, Coroutine<T> c5)
        {
            while (true)
            {
                if (c1.IsCompletedSuccessfully) return c1.Result;
                if (c2.IsCompletedSuccessfully) return c2.Result;
                if (c3.IsCompletedSuccessfully) return c3.Result;
                if (c4.IsCompletedSuccessfully) return c4.Result;
                if (c5.IsCompletedSuccessfully) return c5.Result;

                if (c1.IsCanceled && c2.IsCanceled && c3.IsCanceled && c4.IsCanceled && c5.IsCanceled)
                {
                    Coroutine.ThrowChildrenCancel(new Coroutine<T>[] { c1, c2, c3, c4, c5 });
                }

                await Yield();
            }
        }

        public static async Coroutine<T> WaitAny<T>(Coroutine<T> c1, Coroutine<T> c2, Coroutine<T> c3, Coroutine<T> c4, Coroutine<T> c5, Coroutine<T> c6)
        {
            while (true)
            {
                if (c1.IsCanceled && c2.IsCanceled && c3.IsCanceled && c4.IsCanceled && c5.IsCanceled && c6.IsCanceled)
                {
                    Coroutine.ThrowChildrenCancel(new Coroutine<T>[] { c1, c2, c3, c4, c5, c6 });
                }

                if (c1.IsCompletedSuccessfully) return c1.Result;
                if (c2.IsCompletedSuccessfully) return c2.Result;
                if (c3.IsCompletedSuccessfully) return c3.Result;
                if (c4.IsCompletedSuccessfully) return c4.Result;
                if (c5.IsCompletedSuccessfully) return c5.Result;
                if (c6.IsCompletedSuccessfully) return c6.Result;

                await Yield();
            }
        }

        public static async Coroutine<T> WaitAny<T>(Coroutine<T> c1, Coroutine<T> c2, Coroutine<T> c3, Coroutine<T> c4, Coroutine<T> c5, Coroutine<T> c6, Coroutine<T> c7)
        {
            while (true)
            {
                if (c1.IsCompletedSuccessfully) return c1.Result;
                if (c2.IsCompletedSuccessfully) return c2.Result;
                if (c3.IsCompletedSuccessfully) return c3.Result;
                if (c4.IsCompletedSuccessfully) return c4.Result;
                if (c5.IsCompletedSuccessfully) return c5.Result;
                if (c6.IsCompletedSuccessfully) return c6.Result;
                if (c7.IsCompletedSuccessfully) return c7.Result;

                if (c1.IsCanceled && c2.IsCanceled && c3.IsCanceled && c4.IsCanceled && c5.IsCanceled && c6.IsCanceled && c7.IsCanceled)
                {
                    Coroutine.ThrowChildrenCancel(new Coroutine<T>[] { c1, c2, c3, c4, c5, c6, c7 });
                }

                await Yield();
            }
        }


        public static Coroutine WaitAny(ReadOnlySpan<CoroutineBase> span)
        {
            static async Coroutine Create(CoroutineBase[] coroutines)
            {
                while (true)
                {
                    var isCanceled = true;
                    foreach (var c in coroutines)
                    {
                        if (c.IsCompletedSuccessfully) return;

                        if (isCanceled && !c.IsCanceled)
                        {
                            isCanceled = false;
                        }
                    }

                    if (isCanceled)
                    {
                        Coroutine.ThrowChildrenCancel(coroutines);
                    }
                    await Yield();
                }
            }

            var coroutines = new CoroutineBase[span.Length];
            span.CopyTo(coroutines);
            return Create(coroutines);
        }

        public static Coroutine<T> WaitAny<T>(ReadOnlySpan<Coroutine<T>> span)
        {
            async Coroutine<T> Create(Coroutine<T>[] coroutines)
            {
                while (true)
                {
                    var isCanceled = true;
                    foreach (var c in coroutines)
                    {
                        if (c.IsCompletedSuccessfully) return c.Result;

                        if (isCanceled && !c.IsCanceled)
                        {
                            isCanceled = false;
                        }
                    }

                    if (isCanceled)
                    {
                        Coroutine.ThrowChildrenCancel(coroutines);
                    }

                    await Yield();
                }
            }

            var coroutines = new Coroutine<T>[span.Length];
            span.CopyTo(coroutines);

            return Create(coroutines);
        }
    }
}
