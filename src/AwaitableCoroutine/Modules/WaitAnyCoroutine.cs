using System;

namespace AwaitableCoroutine
{
    public partial class Coroutine
    {
        public static async Coroutine WaitAny(Coroutine c1, Coroutine c2)
        {
            while (!(c1.IsCompletedSuccessfully || c2.IsCompletedSuccessfully))
            {
                if (c1.IsCanceledOrFaulted && c2.IsCanceledOrFaulted)
                {
                    Coroutine.ThrowChildrenCancel(new Coroutine[] { c1, c2 });
                }
                await Yield();
            }
        }

        public static async Coroutine WaitAny(Coroutine c1, Coroutine c2, Coroutine c3)
        {
            while (!(c1.IsCompletedSuccessfully || c2.IsCompletedSuccessfully || c3.IsCompletedSuccessfully))
            {
                if (c1.IsCanceledOrFaulted && c2.IsCanceledOrFaulted && c3.IsCanceledOrFaulted)
                {
                    Coroutine.ThrowChildrenCancel(new Coroutine[] { c1, c2, c3 });
                }
                await Yield();
            }
        }

        public static async Coroutine WaitAny(Coroutine c1, Coroutine c2, Coroutine c3, Coroutine c4)
        {
            while (!(c1.IsCompletedSuccessfully || c2.IsCompletedSuccessfully || c3.IsCompletedSuccessfully || c4.IsCompletedSuccessfully))
            {
                if (c1.IsCanceledOrFaulted && c2.IsCanceledOrFaulted && c3.IsCanceledOrFaulted && c4.IsCanceledOrFaulted)
                {
                    Coroutine.ThrowChildrenCancel(new Coroutine[] { c1, c2, c3, c4 });
                }
                await Yield();
            }
        }

        public static async Coroutine WaitAny(Coroutine c1, Coroutine c2, Coroutine c3, Coroutine c4, Coroutine c5)
        {
            while (!(c1.IsCompletedSuccessfully || c2.IsCompletedSuccessfully || c3.IsCompletedSuccessfully || c4.IsCompletedSuccessfully || c5.IsCompletedSuccessfully))
            {
                if (c1.IsCanceledOrFaulted && c2.IsCanceledOrFaulted && c3.IsCanceledOrFaulted && c4.IsCanceledOrFaulted && c5.IsCanceledOrFaulted)
                {
                    Coroutine.ThrowChildrenCancel(new Coroutine[] { c1, c2, c3, c4, c5 });
                }
                await Yield();
            }
        }

        public static async Coroutine WaitAny(Coroutine c1, Coroutine c2, Coroutine c3, Coroutine c4, Coroutine c5, Coroutine c6)
        {
            while (!(c1.IsCompletedSuccessfully || c2.IsCompletedSuccessfully || c3.IsCompletedSuccessfully || c4.IsCompletedSuccessfully || c5.IsCompletedSuccessfully || c6.IsCompletedSuccessfully))
            {
                if (c1.IsCanceledOrFaulted && c2.IsCanceledOrFaulted && c3.IsCanceledOrFaulted && c4.IsCanceledOrFaulted && c5.IsCanceledOrFaulted && c6.IsCanceledOrFaulted)
                {
                    Coroutine.ThrowChildrenCancel(new Coroutine[] { c1, c2, c3, c4, c5, c6 });
                }
                await Yield();
            }
        }

        public static async Coroutine WaitAny(Coroutine c1, Coroutine c2, Coroutine c3, Coroutine c4, Coroutine c5, Coroutine c6, Coroutine c7)
        {
            while (!(c1.IsCompletedSuccessfully || c2.IsCompletedSuccessfully || c3.IsCompletedSuccessfully || c4.IsCompletedSuccessfully || c5.IsCompletedSuccessfully || c6.IsCompletedSuccessfully || c7.IsCompletedSuccessfully))
            {
                if (c1.IsCanceledOrFaulted && c2.IsCanceledOrFaulted && c3.IsCanceledOrFaulted && c4.IsCanceledOrFaulted && c5.IsCanceledOrFaulted && c6.IsCanceledOrFaulted && c7.IsCanceledOrFaulted)
                {
                    Coroutine.ThrowChildrenCancel(new Coroutine[] { c1, c2, c3, c4, c5, c6, c7 });
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

                if (c1.IsCanceledOrFaulted && c2.IsCanceledOrFaulted)
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

                if (c1.IsCanceledOrFaulted && c2.IsCanceledOrFaulted && c3.IsCanceledOrFaulted)
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

                if (c1.IsCanceledOrFaulted && c2.IsCanceledOrFaulted && c3.IsCanceledOrFaulted && c4.IsCanceledOrFaulted)
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

                if (c1.IsCanceledOrFaulted && c2.IsCanceledOrFaulted && c3.IsCanceledOrFaulted && c4.IsCanceledOrFaulted && c5.IsCanceledOrFaulted)
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
                if (c1.IsCanceledOrFaulted && c2.IsCanceledOrFaulted && c3.IsCanceledOrFaulted && c4.IsCanceledOrFaulted && c5.IsCanceledOrFaulted && c6.IsCanceledOrFaulted)
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

                if (c1.IsCanceledOrFaulted && c2.IsCanceledOrFaulted && c3.IsCanceledOrFaulted && c4.IsCanceledOrFaulted && c5.IsCanceledOrFaulted && c6.IsCanceledOrFaulted && c7.IsCanceledOrFaulted)
                {
                    Coroutine.ThrowChildrenCancel(new Coroutine<T>[] { c1, c2, c3, c4, c5, c6, c7 });
                }

                await Yield();
            }
        }


        public static Coroutine WaitAny(ReadOnlySpan<Coroutine> span)
        {
            static async Coroutine Create(Coroutine[] coroutines)
            {
                while (true)
                {
                    var isCanceledOrFaulted = true;
                    foreach (var c in coroutines)
                    {
                        if (c.IsCompletedSuccessfully) return;

                        if (isCanceledOrFaulted && !c.IsCanceledOrFaulted)
                        {
                            isCanceledOrFaulted = false;
                        }
                    }

                    if (isCanceledOrFaulted)
                    {
                        Coroutine.ThrowChildrenCancel(coroutines);
                    }
                    await Yield();
                }
            }

            var coroutines = new Coroutine[span.Length];
            span.CopyTo(coroutines);
            return Create(coroutines);
        }

        public static Coroutine<T> WaitAny<T>(ReadOnlySpan<Coroutine<T>> span)
        {
            async Coroutine<T> Create(Coroutine<T>[] coroutines)
            {
                while (true)
                {
                    var isCanceledOrFaulted = true;
                    foreach (var c in coroutines)
                    {
                        if (c.IsCompletedSuccessfully) return c.Result;

                        if (isCanceledOrFaulted && !c.IsCanceledOrFaulted)
                        {
                            isCanceledOrFaulted = false;
                        }
                    }

                    if (isCanceledOrFaulted)
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
