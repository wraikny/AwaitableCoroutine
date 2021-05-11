namespace AwaitableCoroutine.Internal
{
    public interface IWaitingCoroutineRegisterer
    {
        void RegisterWaitingCoroutine(AwaitableCoroutineBase coroutine);
    }

    public abstract class WaitingCoroutineRegisterNotifier
    {
        public static WaitingCoroutineRegisterNotifier Instance { get; private set; }

        public abstract void RegisterWaitingCoroutine<T>(ref T awaiter, AwaitableCoroutineBase coroutine);

        public static void Register<T>(ref T awaiter, AwaitableCoroutineBase coroutine)
        {
            if (awaiter is YieldAwaitable.YieldAwaiter) return;

            Instance ??= new Default();
            Instance.RegisterWaitingCoroutine(ref awaiter, coroutine);
        }
    }

    public sealed class Default : WaitingCoroutineRegisterNotifier
    {
        public override void RegisterWaitingCoroutine<T>(ref T awaiter, AwaitableCoroutineBase coroutine)
        {
            if (awaiter is IWaitingCoroutineRegisterer registerer)
            {
                registerer.RegisterWaitingCoroutine(coroutine);
            }
        }
    }

}
