using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public abstract class TestTemplate
    {
        private readonly ITestOutputHelper _outputHelper;

        public TestTemplate(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            Internal.Logger.SetLogger(Log);
        }

        protected void Log(string text)
        {
            try
            {

                _outputHelper.WriteLine(text);
            }
            catch
            {

            }
        }
    }
}
