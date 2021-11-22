using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Actian.EFCore.TestUtilities
{
    public class ActianTestLoggerProvider : ILoggerProvider, ITestOutputHelper
    {
        public ITestOutputHelper Output { get; set; }

        public ILogger CreateLogger(string categoryName)
        {
            return new ActianTestLogger(this);
        }

        public void WriteLine(string message)
        {
            Output?.WriteLine(message);
        }

        public void WriteLine(string format, params object[] args)
        {
            Output?.WriteLine(format, args);
        }

        public void Dispose()
        {
            Output = null;
        }
    }
}
