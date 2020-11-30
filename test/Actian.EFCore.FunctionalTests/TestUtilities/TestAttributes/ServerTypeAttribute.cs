using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities.Xunit;

namespace Actian.EFCore.TestUtilities.TestAttributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ServerTypeAttribute : Attribute, ITestCondition
    {
        public ServerTypeAttribute(string serverType)
        {
            ServerType = serverType ?? throw new ArgumentNullException(nameof(serverType));
        }

        public string ServerType { get; }

        public ValueTask<bool> IsMetAsync() => new ValueTask<bool>(TestEnvironment.ActianServerVersion.ServerType == ServerType);

        public string SkipReason => $"Requires server type \"{ServerType}\". The server type is \"{TestEnvironment.ActianServerVersion.ServerType}\".";
    }
}
