using System.Threading.Tasks;

namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
    {
        public static async AwaitableCoroutine AwaitTask(Task task)
        {
            while (!task.IsCompletedSuccessfully)
            {
                if (task.IsCanceled)
                {
                    AwaitableCoroutine.ThrowCancel("task is canceled");
                }

                if (task.IsFaulted)
                {
                    AwaitableCoroutine.ThrowCancel("task is faulted");
                }
                await Yield();
            }
        }

        public static async AwaitableCoroutine<T> AwaitTask<T>(Task<T> task)
        {
            while (!task.IsCompletedSuccessfully)
            {
                if (task.IsCanceled)
                {
                    AwaitableCoroutine.ThrowCancel("task is canceled");
                }

                if (task.IsFaulted)
                {
                    AwaitableCoroutine.ThrowCancel("task is faulted");
                }
                await Yield();
            }

            return task.Result;
        }

        public static async AwaitableCoroutine AwaitTask(ValueTask task)
        {
            while (!task.IsCompletedSuccessfully)
            {
                if (task.IsCanceled)
                {
                    AwaitableCoroutine.ThrowCancel("task is canceled");
                }

                if (task.IsFaulted)
                {
                    AwaitableCoroutine.ThrowCancel("task is faulted");
                }
                await Yield();
            }
        }

        public static async AwaitableCoroutine<T> AwaitTask<T>(ValueTask<T> task)
        {
            while (!task.IsCompletedSuccessfully)
            {
                if (task.IsCanceled)
                {
                    AwaitableCoroutine.ThrowCancel("task is canceled");
                }

                if (task.IsFaulted)
                {
                    AwaitableCoroutine.ThrowCancel("task is faulted");
                }
                await Yield();
            }

            return task.Result;
        }
    }
}
