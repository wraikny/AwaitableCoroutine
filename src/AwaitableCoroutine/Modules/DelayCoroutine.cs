using System;

namespace AwaitableCoroutine
{
    internal sealed class DelayFloatCoroutine : AwaitableCoroutine
    {
        private float _timeCount;

        private readonly float _targetTime;
        private readonly Func<float> _getDeltaTime;

        public DelayFloatCoroutine(float targetTime, Func<float> getDeltaTime)
        {
            _targetTime = targetTime;
            _getDeltaTime = getDeltaTime;
        }

        public override void MoveNext()
        {
            _timeCount += _getDeltaTime();

            if (_timeCount >= _targetTime)
            {
                Complete();
            }
        }
    }

    public partial class AwaitableCoroutine
    {
        public static AwaitableCoroutine DelayFloat(float targetTime, Func<float> getDeltaTime)
        {
            if (getDeltaTime is null)
            {
                throw new ArgumentNullException(nameof(getDeltaTime));
            }

            return new DelayFloatCoroutine(targetTime, getDeltaTime);
        }
    }
}
