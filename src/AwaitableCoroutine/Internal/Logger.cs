using System;
using System.Diagnostics;

namespace AwaitableCoroutine.Internal
{
    public static class Logger
    {
#if DEBUG
        private static Action<string> s_logger;
#endif

        [Conditional("DEBUG")]
        public static void Log(string text)
        {
            s_logger?.Invoke(text);
        }

        [Conditional("DEBUG")]
        public static void SetLogger(Action<string> logger)
        {
            s_logger = logger;
        }
    }
}
