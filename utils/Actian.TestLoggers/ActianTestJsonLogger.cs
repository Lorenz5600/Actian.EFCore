using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace Actian.TestLoggers
{
    [FriendlyName("actian-json")] // Alternate user friendly string to uniquely identify the console logger.
    [ExtensionUri("logger://actian.com/TestPlatform/ActianTestJsonLogger/v1")] // Uri used to uniquely identify the logger.
    public class ActianTestJsonLogger : ActianTestLogger
    {
        public override string DefaultTestResultFileName => "TestResults.json";

        protected override IEnumerable<IActianTestReporter> Reporters
        {
            get
            {
                yield return new ActianJsonReporter(Config.LogFilePath);
            }
        }
    }
}
