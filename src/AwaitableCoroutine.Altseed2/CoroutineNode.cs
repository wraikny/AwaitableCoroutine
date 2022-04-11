using System;

using Altseed2;

namespace AwaitableCoroutine.Altseed2
{
    public sealed class CoroutineNode : Node, ICoroutineRunner
    {
        private readonly ICoroutineRunner _runner;

        public CoroutineNode()
        {
            _runner = new CoroutineRunner();
        }

        protected override void OnUpdate()
        {
            _runner.Update();
        }

        bool ICoroutineRunner.IsUpdating => _runner.IsUpdating;

        void ICoroutineRunner.OnRegistering(CoroutineBase coroutine) => _runner.OnRegistering(coroutine);

        void ICoroutineRunner.OnUpdate() => _runner.OnUpdate();

        void ICoroutineRunner.Post(Action continuation) => _runner.Post(continuation);
    }
}
