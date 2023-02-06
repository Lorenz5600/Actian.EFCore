using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities.Xunit;

namespace Actian.EFCore.TestUtilities
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ServerSupportsSequencesAttribute : ActianTestAttribute, ITestCondition
    {
        public ServerSupportsSequencesAttribute([CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            : base(sourceFilePath, sourceLineNumber)
        {
        }

        public ValueTask<bool> IsMetAsync() => new ValueTask<bool>(true);

        public string SkipReason => $"The server does not support sequences. The server version is \"{TestEnvironment.ServerVersion}\".";
    }
}
