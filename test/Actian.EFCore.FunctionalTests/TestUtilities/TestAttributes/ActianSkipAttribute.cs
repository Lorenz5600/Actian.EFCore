using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities.Xunit;

namespace Actian.EFCore.TestUtilities
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class ActianSkipAttribute : Attribute, ITestCondition
    {
        public IEnumerable<ActianCondition> Conditions { get; set; }

        public ActianSkipAttribute(string skipReason)
        {
            SkipReason = Text.Normalize(skipReason ?? throw new ArgumentNullException(nameof(skipReason)));
        }

        private static readonly ValueTask<bool> FalseValueTask = new ValueTask<bool>(false);

        public ValueTask<bool> IsMetAsync() => FalseValueTask;

        public string SkipReason { get; }
    }
}
