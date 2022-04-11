namespace AwaitableCoroutine
{
    public partial class Coroutine
    {
        public static YieldAwaitable Yield()
        {
            Internal.Logger.Log("Coroutine.Yield()");
            return new YieldAwaitable();
        }
    }
}
