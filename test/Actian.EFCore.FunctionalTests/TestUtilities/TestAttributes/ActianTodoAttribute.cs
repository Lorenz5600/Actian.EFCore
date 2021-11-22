using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities.Xunit;

namespace Actian.EFCore.TestUtilities
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class ActianTodoAttribute : Attribute, ITestCondition
    {
        public ActianTodoAttribute(string skipReason = null)
        {
            SkipReason = string.IsNullOrWhiteSpace(skipReason)
                ? "Todo"
                : $"Todo:\n{Text.Normalize(skipReason)}";
        }

        private static bool Disabled => Environment.GetEnvironmentVariable("DISABLE_ACTIAN_TODO") == "true";

        public ValueTask<bool> IsMetAsync() => new ValueTask<bool>(Disabled);

        public string SkipReason { get; }
    }
}
