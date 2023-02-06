using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities.Xunit;

namespace Actian.EFCore.TestUtilities
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class ActianSkipAttribute : ActianTestAttribute, ITestCondition
    {
        public IEnumerable<ActianCondition> Conditions { get; set; }

        public ActianSkipAttribute(string skipReason, [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            : base(sourceFilePath, sourceLineNumber)
        {
            SkipReason = skipReason.NormalizeText() ?? throw new ArgumentNullException(nameof(skipReason));
        }

        private static readonly ValueTask<bool> FalseValueTask = new ValueTask<bool>(false);

        public ValueTask<bool> IsMetAsync() => FalseValueTask;

        public string SkipReason { get; }
    }
}
