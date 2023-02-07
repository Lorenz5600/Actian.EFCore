using System;
using System.Runtime.CompilerServices;

namespace Actian.EFCore.TestUtilities
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class ActianSkipAnsiAttribute : ActianSkipAttribute
    {
        public ActianSkipAnsiAttribute([CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            : base(ActianSkipReasons.TestFailsForAnsi, ActianCompatibility.Ansi, sourceFilePath, sourceLineNumber)
        {
        }
    }
}
