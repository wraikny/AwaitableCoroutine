namespace AwaitableCoroutine
{
    internal sealed class YieldCoroutine : AwaitableCoroutine
    {
        public YieldCoroutine() { }

        protected override void OnMoveNext() => Complete();
    }

    public partial class AwaitableCoroutine
    {
        public static AwaitableCoroutine Yield()
        {
            return new YieldCoroutine();
        }
    }
}
