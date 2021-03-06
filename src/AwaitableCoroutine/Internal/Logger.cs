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
#if DEBUG
            s_logger?.Invoke(text);
#endif
        }

        [Conditional("DEBUG")]
        public static void SetLogger(Action<string> logger)
        {
#if DEBUG
            s_logger = logger;
#endif
        }
    }
}
