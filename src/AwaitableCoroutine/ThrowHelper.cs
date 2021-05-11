using System;

namespace AwaitableCoroutine
{
    internal static class ThrowHelper
    {
        public static void ArgNull(string paramName) => throw new ArgumentNullException(paramName);
        public static void InvalidOp(string message) => throw new InvalidOperationException(message);
    }
}
