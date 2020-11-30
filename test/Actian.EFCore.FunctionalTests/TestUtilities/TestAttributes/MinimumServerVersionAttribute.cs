using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities.Xunit;

namespace Actian.EFCore.TestUtilities.TestAttributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class MinimumServerVersionAttribute : Attribute, ITestCondition
    {
        public MinimumServerVersionAttribute(int major, int minor, string serverType = null)
        {
            MinimumServerVersion = new ActianServerVersion(major, minor, serverType);
        }

        public ActianServerVersion MinimumServerVersion { get; }

        public ValueTask<bool> IsMetAsync() => new ValueTask<bool>(TestEnvironment.ActianServerVersion.Supports(MinimumServerVersion));

        public string SkipReason => $"Requires {MinimumServerVersion} or later. The server version is \"{TestEnvironment.ServerVersion}\".";
    }
}
