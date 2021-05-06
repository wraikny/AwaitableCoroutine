namespace AwaitableCoroutine
{
    public sealed class YieldCoroutine : AwaitableCoroutine
    {
        public YieldCoroutine()
        {

        }

        public override void MoveNext()
        {
            if (IsCompleted) return;
            Complete();
        }
    }

    public partial class AwaitableCoroutine
    {
        public static AwaitableCoroutine Yield()
        {
            return new YieldCoroutine().SetupRunner();
        }
    }
}
