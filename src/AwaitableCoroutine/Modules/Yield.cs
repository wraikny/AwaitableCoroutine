namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static YieldAwaitable Yield()
        {
            Internal.Logger.Log("AwaitableCoroutine.Yield()");
            return new YieldAwaitable();
        }
    }
}
