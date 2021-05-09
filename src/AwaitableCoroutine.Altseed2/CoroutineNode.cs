using System;

using Altseed2;

namespace AwaitableCoroutine.Altseed2
{
    public sealed class CoroutineNode : Node
    {
        public int CoroutineCount => _runner.Count;
        private readonly CoroutineRunner _runner;

        public CoroutineNode()
        {
            _runner = new CoroutineRunner();
        }

        protected override void OnUpdate()
        {
            _runner.Update();
        }

        public void Context(Action action)
        {
            _runner.Context(action);
        }

        public T Context<T>(Func<T> init)
        {
            return _runner.Context(init);
        }
    }
}
