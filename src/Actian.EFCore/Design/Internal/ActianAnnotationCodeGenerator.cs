// TODO: Implement for Actian

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Design;

namespace Actian.EFCore.Design.Internal
{
    public class ActianAnnotationCodeGenerator : AnnotationCodeGenerator
    {
        public ActianAnnotationCodeGenerator([NotNull] AnnotationCodeGeneratorDependencies dependencies)
            : base(dependencies)
        {
        }
    }
}
