using System;
namespace AwaitableCoroutine
{
    internal sealed class YieldCoroutine : AwaitableCoroutine
    {
        private readonly Action _action;
        public YieldCoroutine(Action action)
        {
            _action = action;
        }

        protected override void OnMoveNext()
        {
            _action?.Invoke();
            Complete();
        }
    }

    internal sealed class YieldCoroutine<T> : AwaitableCoroutine<T>
    {
        private readonly Func<T> _generator;
        public YieldCoroutine(Func<T> generator)
        {
            _generator = generator;
        }

        protected override void OnMoveNext()
        {
            Complete(_generator is null ? default : _generator.Invoke());
        }
    }

    public partial class AwaitableCoroutine
    {
        public static AwaitableCoroutine Yield(Action action = null)
        {
            return new YieldCoroutine(action);
        }

        public static AwaitableCoroutine<T> YieldOf<T>(T res)
        {
            return new YieldCoroutine<T>(() => res);
        }

        public static AwaitableCoroutine<T> Yield<T>(Func<T> generator)
        {
            return new YieldCoroutine<T>(generator);
        }
    }
}
