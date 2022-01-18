namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static Internal.YieldAwaitable Yield()
        {
            Internal.Logger.Log("AwaitableCoroutine.Yield()");
            return new Internal.YieldAwaitable();
        }
    }
}
