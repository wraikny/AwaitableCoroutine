namespace AwaitableCoroutine.Test
{
    internal sealed class Counter
    {
        public int Count { get; private set; }

        public void Inc() => Count++;
    }
}
