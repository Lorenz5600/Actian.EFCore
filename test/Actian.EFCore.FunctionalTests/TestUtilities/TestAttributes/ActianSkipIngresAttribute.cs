using System;
using System.Runtime.CompilerServices;

namespace Actian.EFCore.TestUtilities
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class ActianSkipIngresAttribute : ActianSkipAttribute
    {
        public ActianSkipIngresAttribute([CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            : base(ActianSkipReasons.TestFailsForIngres, ActianCompatibility.Ingres, sourceFilePath, sourceLineNumber)
        {
        }
    }
}
