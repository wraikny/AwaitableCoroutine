using System;
using System.Collections.Generic;
using System.Linq;

namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2)
        {
            return Until(() => c1.IsCompleted || c2.IsCompleted);
        }

        public static AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3)
        {
            return Until(() => c1.IsCompleted || c2.IsCompleted || c3.IsCompleted);
        }

        public static AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4)
        {
            return Until(() => c1.IsCompleted || c2.IsCompleted || c3.IsCompleted || c4.IsCompleted);
        }

        public static AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4, AwaitableCoroutineBase c5)
        {
            return Until(() => c1.IsCompleted || c2.IsCompleted || c3.IsCompleted || c4.IsCompleted || c5.IsCompleted);
        }

        public static AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4, AwaitableCoroutineBase c5, AwaitableCoroutineBase c6)
        {
            return Until(() => c1.IsCompleted || c2.IsCompleted || c3.IsCompleted || c4.IsCompleted || c5.IsCompleted || c6.IsCompleted);
        }

        public static AwaitableCoroutine WaitAny(AwaitableCoroutineBase c1, AwaitableCoroutineBase c2, AwaitableCoroutineBase c3, AwaitableCoroutineBase c4, AwaitableCoroutineBase c5, AwaitableCoroutineBase c6, AwaitableCoroutineBase c7)
        {
            return Until(() => c1.IsCompleted || c2.IsCompleted || c3.IsCompleted || c4.IsCompleted || c5.IsCompleted || c6.IsCompleted || c7.IsCompleted);
        }

        public static AwaitableCoroutine<T> WaitAny<T>(AwaitableCoroutine<T> c1, AwaitableCoroutine<T> c2)
        {
            T res = default;
            return Until(() =>
            {
                if (c1.IsCompleted)
                {
                    res = c1.Result;
                    return true;
                }

                if (c2.IsCompleted)
                {
                    res = c2.Result;
                    return true;
                }

                return false;
            }, () => res);
        }

        public static AwaitableCoroutine<T> WaitAny<T>(AwaitableCoroutine<T> c1, AwaitableCoroutine<T> c2, AwaitableCoroutine<T> c3)
        {
            T res = default;
            return Until(() =>
            {
                if (c1.IsCompleted)
                {
                    res = c1.Result;
                    return true;
                }

                if (c2.IsCompleted)
                {
                    res = c2.Result;
                    return true;
                }

                if (c3.IsCompleted)
                {
                    res = c3.Result;
                    return true;
                }

                return false;
            }, () => res);
        }

        public static AwaitableCoroutine<T> WaitAny<T>(AwaitableCoroutine<T> c1, AwaitableCoroutine<T> c2, AwaitableCoroutine<T> c3, AwaitableCoroutine<T> c4)
        {
            T res = default;
            return Until(() =>
            {
                if (c1.IsCompleted)
                {
                    res = c1.Result;
                    return true;
                }

                if (c2.IsCompleted)
                {
                    res = c2.Result;
                    return true;
                }

                if (c3.IsCompleted)
                {
                    res = c3.Result;
                    return true;
                }

                if (c4.IsCompleted)
                {
                    res = c4.Result;
                    return true;
                }

                return false;
            }, () => res);
        }

        public static AwaitableCoroutine<T> WaitAny<T>(AwaitableCoroutine<T> c1, AwaitableCoroutine<T> c2, AwaitableCoroutine<T> c3, AwaitableCoroutine<T> c4, AwaitableCoroutine<T> c5)
        {
            T res = default;
            return Until(() =>
            {
                if (c1.IsCompleted)
                {
                    res = c1.Result;
                    return true;
                }

                if (c2.IsCompleted)
                {
                    res = c2.Result;
                    return true;
                }

                if (c3.IsCompleted)
                {
                    res = c3.Result;
                    return true;
                }

                if (c4.IsCompleted)
                {
                    res = c4.Result;
                    return true;
                }

                if (c5.IsCompleted)
                {
                    res = c5.Result;
                    return true;
                }

                return false;
            }, () => res);
        }

        public static AwaitableCoroutine<T> WaitAny<T>(AwaitableCoroutine<T> c1, AwaitableCoroutine<T> c2, AwaitableCoroutine<T> c3, AwaitableCoroutine<T> c4, AwaitableCoroutine<T> c5, AwaitableCoroutine<T> c6)
        {
            T res = default;
            return Until(() =>
            {
                if (c1.IsCompleted)
                {
                    res = c1.Result;
                    return true;
                }

                if (c2.IsCompleted)
                {
                    res = c2.Result;
                    return true;
                }

                if (c3.IsCompleted)
                {
                    res = c3.Result;
                    return true;
                }

                if (c4.IsCompleted)
                {
                    res = c4.Result;
                    return true;
                }

                if (c5.IsCompleted)
                {
                    res = c5.Result;
                    return true;
                }

                if (c6.IsCompleted)
                {
                    res = c6.Result;
                    return true;
                }

                return false;
            }, () => res);
        }

        public static AwaitableCoroutine<T> WaitAny<T>(AwaitableCoroutine<T> c1, AwaitableCoroutine<T> c2, AwaitableCoroutine<T> c3, AwaitableCoroutine<T> c4, AwaitableCoroutine<T> c5, AwaitableCoroutine<T> c6, AwaitableCoroutine<T> c7)
        {
            T res = default;
            return Until(() =>
            {
                if (c1.IsCompleted)
                {
                    res = c1.Result;
                    return true;
                }

                if (c2.IsCompleted)
                {
                    res = c2.Result;
                    return true;
                }

                if (c3.IsCompleted)
                {
                    res = c3.Result;
                    return true;
                }

                if (c4.IsCompleted)
                {
                    res = c4.Result;
                    return true;
                }

                if (c5.IsCompleted)
                {
                    res = c5.Result;
                    return true;
                }

                if (c6.IsCompleted)
                {
                    res = c6.Result;
                    return true;
                }

                if (c7.IsCompleted)
                {
                    res = c7.Result;
                    return true;
                }

                return false;
            }, () => res);
        }

        public static AwaitableCoroutine WaitAny(ReadOnlySpan<AwaitableCoroutineBase> span)
        {
            var coroutines = new AwaitableCoroutineBase[span.Length];
            span.CopyTo(coroutines);

            return Until(() => coroutines.Any(c => c.IsCompleted));
        }

        public static AwaitableCoroutine<T> WaitAny<T>(ReadOnlySpan<AwaitableCoroutine<T>> span)
        {
            var coroutines = new AwaitableCoroutine<T>[span.Length];
            span.CopyTo(coroutines);

            T res = default;

            return Until(() =>
            {
                foreach (var c in coroutines)
                {
                    if (c.IsCompleted)
                    {
                        res = c.Result;
                        return true;
                    }
                }

                return false;
            }, () => res);
        }
    }
}
