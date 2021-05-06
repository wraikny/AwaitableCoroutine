using System;

namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static async AwaitableCoroutine Delay(int targetTime, Func<int> getDeltaTime)
        {
            if (getDeltaTime is null)
            {
                throw new ArgumentNullException(nameof(getDeltaTime));
            }

            for (var count = 0; count < targetTime; count += getDeltaTime())
            {
                await Yield();
            }
        }

        public static async AwaitableCoroutine Delay(float targetTime, Func<float> getDeltaTime)
        {
            if (getDeltaTime is null)
            {
                throw new ArgumentNullException(nameof(getDeltaTime));
            }

            for (var count = 0f; count < targetTime; count += getDeltaTime())
            {
                await Yield();
            }
        }

        public static async AwaitableCoroutine Delay(double targetTime, Func<double> getDeltaTime)
        {
            if (getDeltaTime is null)
            {
                throw new ArgumentNullException(nameof(getDeltaTime));
            }

            for (var count = 0.0; count < targetTime; count += getDeltaTime())
            {
                await Yield();
            }
        }
    }
}
