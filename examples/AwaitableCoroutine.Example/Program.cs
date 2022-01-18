using System;

namespace AwaitableCoroutine.Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var runner = new CoroutineRunner();

            int count = 0;

            var coroutine = runner.Create(async () => {
                for (var i = 0; i < 10; i++)
                {
                    count++;
                    await AwaitableCoroutine.Yield();
                }
            });

            Console.WriteLine("Started!");

            while (!coroutine.IsCompleted)
            {
                Console.WriteLine($"{count}");
                runner.Update();
            }

            Console.WriteLine("Finished!");
        }
    }
}
