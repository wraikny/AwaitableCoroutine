using System;

namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static async AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2)
        {
            while (!(c1.IsCompletedSuccessfully || c2.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled && c2.IsCanceled)
                {
                    AwaitableCoroutine.ThrowChildrenCancel<AwaitableCoroutineBase>(new AwaitableCoroutineBase[] { c1, c2 });
                }
                await Yield();
            }
        }

        public static async AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3)
        {
            while (!(c1.IsCompletedSuccessfully || c2.IsCompletedSuccessfully || c3.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled && c2.IsCanceled && c3.IsCanceled)
                {
                    AwaitableCoroutine.ThrowChildrenCancel<AwaitableCoroutineBase>(new AwaitableCoroutineBase[] { c1, c2, c3 });
                }
                await Yield();
            }
        }

        public static async AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4)
        {
            while (!(c1.IsCompletedSuccessfully || c2.IsCompletedSuccessfully || c3.IsCompletedSuccessfully || c4.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled && c2.IsCanceled && c3.IsCanceled && c4.IsCanceled)
                {
                    AwaitableCoroutine.ThrowChildrenCancel<AwaitableCoroutineBase>(new AwaitableCoroutineBase[] { c1, c2, c3, c4 });
                }
                await Yield();
            }
        }

        public static async AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4, AwaitableCoroutineBase c5)
        {
            while (!(c1.IsCompletedSuccessfully || c2.IsCompletedSuccessfully || c3.IsCompletedSuccessfully || c4.IsCompletedSuccessfully || c5.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled && c2.IsCanceled && c3.IsCanceled && c4.IsCanceled && c5.IsCanceled)
                {
                    AwaitableCoroutine.ThrowChildrenCancel<AwaitableCoroutineBase>(new AwaitableCoroutineBase[] { c1, c2, c3, c4, c5 });
                }
                await Yield();
            }
        }

        public static async AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4, AwaitableCoroutineBase c5, AwaitableCoroutineBase c6)
        {
            while (!(c1.IsCompletedSuccessfully || c2.IsCompletedSuccessfully || c3.IsCompletedSuccessfully || c4.IsCompletedSuccessfully || c5.IsCompletedSuccessfully || c6.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled && c2.IsCanceled && c3.IsCanceled && c4.IsCanceled && c5.IsCanceled && c6.IsCanceled)
                {
                    AwaitableCoroutine.ThrowChildrenCancel<AwaitableCoroutineBase>(new AwaitableCoroutineBase[] { c1, c2, c3, c4, c5, c6 });
                }
                await Yield();
            }
        }

        public static async AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4, AwaitableCoroutineBase c5, AwaitableCoroutineBase c6, AwaitableCoroutineBase c7)
        {
            while (!(c1.IsCompletedSuccessfully || c2.IsCompletedSuccessfully || c3.IsCompletedSuccessfully || c4.IsCompletedSuccessfully || c5.IsCompletedSuccessfully || c6.IsCompletedSuccessfully || c7.IsCompletedSuccessfully))
            {
                if (c1.IsCanceled && c2.IsCanceled && c3.IsCanceled && c4.IsCanceled && c5.IsCanceled && c6.IsCanceled && c7.IsCanceled)
                {
                    AwaitableCoroutine.ThrowChildrenCancel<AwaitableCoroutineBase>(new AwaitableCoroutineBase[] { c1, c2, c3, c4, c5, c6, c7 });
                }
                await Yield();
            }
        }

        public static async AwaitableCoroutine<T> WaitAny<T>(AwaitableCoroutine<T> c1, AwaitableCoroutine<T> c2)
        {
            while (true)
            {
                if (c1.IsCompletedSuccessfully) return c1.Result;
                if (c2.IsCompletedSuccessfully) return c2.Result;

                if (c1.IsCanceled && c2.IsCanceled)
                {
                    AwaitableCoroutine.ThrowChildrenCancel<AwaitableCoroutine<T>>(new AwaitableCoroutine<T>[] { c1, c2 });
                }

                await Yield();
            }
        }

        public static async AwaitableCoroutine<T> WaitAny<T>(AwaitableCoroutine<T> c1, AwaitableCoroutine<T> c2, AwaitableCoroutine<T> c3)
        {
            while (true)
            {
                if (c1.IsCompletedSuccessfully) return c1.Result;
                if (c2.IsCompletedSuccessfully) return c2.Result;
                if (c3.IsCompletedSuccessfully) return c3.Result;

                if (c1.IsCanceled && c2.IsCanceled && c3.IsCanceled)
                {
                    AwaitableCoroutine.ThrowChildrenCancel<AwaitableCoroutine<T>>(new AwaitableCoroutine<T>[] { c1, c2, c3 });
                }

                await Yield();
            }
        }

        public static async AwaitableCoroutine<T> WaitAny<T>(AwaitableCoroutine<T> c1, AwaitableCoroutine<T> c2, AwaitableCoroutine<T> c3, AwaitableCoroutine<T> c4)
        {
            while (true)
            {
                if (c1.IsCompletedSuccessfully) return c1.Result;
                if (c2.IsCompletedSuccessfully) return c2.Result;
                if (c3.IsCompletedSuccessfully) return c3.Result;
                if (c4.IsCompletedSuccessfully) return c4.Result;

                if (c1.IsCanceled && c2.IsCanceled && c3.IsCanceled && c4.IsCanceled)
                {
                    AwaitableCoroutine.ThrowChildrenCancel<AwaitableCoroutine<T>>(new AwaitableCoroutine<T>[] { c1, c2, c3, c4 });
                }

                await Yield();
            }
        }

        public static async AwaitableCoroutine<T> WaitAny<T>(AwaitableCoroutine<T> c1, AwaitableCoroutine<T> c2, AwaitableCoroutine<T> c3, AwaitableCoroutine<T> c4, AwaitableCoroutine<T> c5)
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
                    AwaitableCoroutine.ThrowChildrenCancel<AwaitableCoroutine<T>>(new AwaitableCoroutine<T>[] { c1, c2, c3, c4, c5 });
                }

                await Yield();
            }
        }

        public static async AwaitableCoroutine<T> WaitAny<T>(AwaitableCoroutine<T> c1, AwaitableCoroutine<T> c2, AwaitableCoroutine<T> c3, AwaitableCoroutine<T> c4, AwaitableCoroutine<T> c5, AwaitableCoroutine<T> c6)
        {
            while (true)
            {
                if (c1.IsCanceled && c2.IsCanceled && c3.IsCanceled && c4.IsCanceled && c5.IsCanceled && c6.IsCanceled)
                {
                    AwaitableCoroutine.ThrowChildrenCancel<AwaitableCoroutine<T>>(new AwaitableCoroutine<T>[] { c1, c2, c3, c4, c5, c6 });
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

        public static async AwaitableCoroutine<T> WaitAny<T>(AwaitableCoroutine<T> c1, AwaitableCoroutine<T> c2, AwaitableCoroutine<T> c3, AwaitableCoroutine<T> c4, AwaitableCoroutine<T> c5, AwaitableCoroutine<T> c6, AwaitableCoroutine<T> c7)
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
                    AwaitableCoroutine.ThrowChildrenCancel<AwaitableCoroutine<T>>(new AwaitableCoroutine<T>[] { c1, c2, c3, c4, c5, c6, c7 });
                }

                await Yield();
            }
        }


        public static AwaitableCoroutine WaitAny(ReadOnlySpan<AwaitableCoroutineBase> span)
        {
            static async AwaitableCoroutine Create(AwaitableCoroutineBase[] coroutines)
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
                        AwaitableCoroutine.ThrowChildrenCancel<AwaitableCoroutineBase>(coroutines);
                    }
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
                        AwaitableCoroutine.ThrowChildrenCancel<AwaitableCoroutineBase>(coroutines);
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
