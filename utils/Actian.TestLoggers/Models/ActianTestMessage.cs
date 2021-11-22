using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

namespace Actian.TestLoggers
{
    public class ActianTestMessage
    {
        public ActianTestMessage() { }
        public ActianTestMessage(TestMessageLevel level, string message)
        {
            Level = level;
            Message = message;
        }

        public TestMessageLevel Level { get; set; }
        public string Message { get; set; }
    }
}
