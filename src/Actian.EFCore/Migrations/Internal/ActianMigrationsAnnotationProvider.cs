using System.Collections.Generic;
using System.Globalization;
using Actian.EFCore.Metadata.Internal;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Actian.EFCore.Migrations.Internal
{
    public class ActianMigrationsAnnotationProvider : MigrationsAnnotationProvider
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="dependencies"> Parameter object containing dependencies for this service. </param>
        public ActianMigrationsAnnotationProvider(
            [NotNull] MigrationsAnnotationProviderDependencies dependencies
            )
            : base(dependencies)
        {
        }

        public override IEnumerable<IAnnotation> For(IProperty property)
        {
            if (property.GetValueGenerationStrategy() == ActianValueGenerationStrategy.IdentityColumn)
            {
                var seed = property.GetIdentitySeed();
                var increment = property.GetIdentityIncrement();

                yield return new Annotation(
                    ActianAnnotationNames.Identity,
                    string.Format(CultureInfo.InvariantCulture, "{0}, {1}", seed ?? 1, increment ?? 1));
            }
        }
    }
}
