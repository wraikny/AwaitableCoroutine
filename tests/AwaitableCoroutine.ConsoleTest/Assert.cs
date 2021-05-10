using System;

namespace AwaitableCoroutine.ConsoleTest
{
    internal static class Assert
    {
        public static void True(bool condition)
        {
            if (!condition) throw new Exception();
        }
    }
}