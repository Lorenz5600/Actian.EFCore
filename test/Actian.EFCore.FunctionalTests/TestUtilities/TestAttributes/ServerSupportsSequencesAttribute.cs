using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities.Xunit;

namespace Actian.EFCore.TestUtilities
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ServerSupportsSequencesAttribute : Attribute, ITestCondition
    {
        public ValueTask<bool> IsMetAsync() => new ValueTask<bool>(true);

        public string SkipReason => $"The server does not support sequences. The server version is \"{TestEnvironment.ServerVersion}\".";
    }
}
