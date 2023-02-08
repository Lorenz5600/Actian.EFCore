using System.Collections.Generic;
using Actian.EFCore.Metadata.Internal;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Actian.EFCore.Migrations.Internal
{
    public class ActianMigrationsAnnotationProvider : MigrationsAnnotationProvider
    {
        public ActianMigrationsAnnotationProvider(
            [NotNull] MigrationsAnnotationProviderDependencies dependencies
            )
            : base(dependencies)
        {
        }

        public override IEnumerable<IAnnotation> For(IProperty property)
        {
            var valueGenerationStrategy = property.GetValueGenerationStrategy();
            if (valueGenerationStrategy != ActianValueGenerationStrategy.None)
            {
                yield return new Annotation(ActianAnnotationNames.ValueGenerationStrategy, valueGenerationStrategy);

                if (valueGenerationStrategy.IsIdentity())
                {
                    if (property[ActianAnnotationNames.IdentityOptions] is string identityOptions)
                    {
                        yield return new Annotation(ActianAnnotationNames.IdentityOptions, identityOptions);
                    }
                }
            }
        }
    }
}
