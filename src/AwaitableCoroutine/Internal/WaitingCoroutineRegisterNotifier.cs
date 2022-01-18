namespace AwaitableCoroutine.Internal
{
    public interface IWaitingCoroutineRegisterer
    {
        void RegisterWaitingCoroutine(AwaitableCoroutineBase coroutine);
    }

    public abstract class WaitingCoroutineRegisterNotifier
    {
        public static WaitingCoroutineRegisterNotifier Instance { get; set; }

        public abstract bool RegisterWaitingCoroutine<T>(ref T awaiter, AwaitableCoroutineBase coroutine);

        public static void Register<T>(ref T awaiter, AwaitableCoroutineBase coroutine)
        {
            if (awaiter is YieldAwaitable.YieldAwaiter) return;

            Instance ??= new Default();
            Instance.RegisterWaitingCoroutine(ref awaiter, coroutine);
        }

        public sealed class Default : WaitingCoroutineRegisterNotifier
        {
            public override bool RegisterWaitingCoroutine<T>(ref T awaiter, AwaitableCoroutineBase coroutine)
            {
                if (awaiter is IWaitingCoroutineRegisterer registerer)
                {
                    registerer.RegisterWaitingCoroutine(coroutine);
                    return true;
                }

                return false;
            }
        }

        public sealed class Composed : WaitingCoroutineRegisterNotifier
        {
            private readonly WaitingCoroutineRegisterNotifier[] _notifiers;

            public Composed(WaitingCoroutineRegisterNotifier[] notifiers)
            {
                if (notifiers is null)
                {
                    ThrowHelper.ArgNull(nameof(notifiers));
                }
                _notifiers = notifiers;
            }

            public override bool RegisterWaitingCoroutine<T>(ref T awaiter, AwaitableCoroutineBase coroutine)
            {
                foreach (var n in _notifiers)
                {
                    if (n.RegisterWaitingCoroutine<T>(ref awaiter, coroutine)) return true;
                }

                return false;
            }
        }
    }
}
