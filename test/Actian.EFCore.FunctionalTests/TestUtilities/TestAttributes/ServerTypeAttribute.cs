using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities.Xunit;

namespace Actian.EFCore.TestUtilities
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ServerTypeAttribute : ActianTestAttribute, ITestCondition
    {
        public ServerTypeAttribute(string serverType, [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            : base(sourceFilePath, sourceLineNumber)
        {
            ServerType = serverType ?? throw new ArgumentNullException(nameof(serverType));
        }

        public string ServerType { get; }

        public ValueTask<bool> IsMetAsync() => new ValueTask<bool>(TestEnvironment.ActianServerVersion.ServerType == ServerType);

        public string SkipReason => $"Requires server type \"{ServerType}\". The server type is \"{TestEnvironment.ActianServerVersion.ServerType}\".";
    }
}
