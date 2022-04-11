using System;

using Altseed2;

using AwaitableCoroutine;
using AwaitableCoroutine.Altseed2;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Engine.Initialize("AwaitableCoroutine", 400, 300);

        var runner = new CoroutineNode();
        Engine.AddNode(runner);

        var node = new RectangleNode { RectangleSize = new Vector2F(10f, 10f)};
        Engine.AddNode(node);

        var coroutine = runner.Create(() => {
            var initPos = node.Position;
            return Altseed2Coroutine.DelaySecond(2f, a => {
                var ea = Easing.GetEasing(EasingType.InOutQuad, a);
                node.Position = initPos + new Vector2F(ea * 400f, 0f);
            });
        });

        while (Engine.DoEvents())
        {
            if (coroutine.IsCompletedSuccessfully) break;

            Engine.Update();
        }

        Engine.Terminate();
    }
}
