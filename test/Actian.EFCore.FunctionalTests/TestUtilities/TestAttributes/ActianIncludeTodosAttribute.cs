using System;
using System.Runtime.CompilerServices;

namespace Actian.EFCore.TestUtilities
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ActianIncludeTodosAttribute : ActianTestAttribute
    {
        public ActianIncludeTodosAttribute([CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            : base(sourceFilePath, sourceLineNumber)
        {
        }
    }
}
