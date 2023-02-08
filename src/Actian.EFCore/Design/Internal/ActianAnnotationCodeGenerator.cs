using Actian.EFCore.Utilities;
using System.Diagnostics;
using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Actian.EFCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace Actian.EFCore.Design.Internal
{
    public class ActianAnnotationCodeGenerator : AnnotationCodeGenerator
    {
        public ActianAnnotationCodeGenerator([NotNull] AnnotationCodeGeneratorDependencies dependencies)
            : base(dependencies)
        {
        }
        public override bool IsHandledByConvention(IModel model, IAnnotation annotation)
        {
            Check.NotNull(model, nameof(model));
            Check.NotNull(annotation, nameof(annotation));

            if (annotation.Name == RelationalAnnotationNames.DefaultSchema
                && string.Equals("public", (string)annotation.Value))
            {
                return true;
            }

            return false;
        }

        public override bool IsHandledByConvention(IProperty property, IAnnotation annotation)
        {
            Check.NotNull(property, nameof(property));
            Check.NotNull(annotation, nameof(annotation));

            if (annotation.Name == ActianAnnotationNames.ValueGenerationStrategy)
            {
                Debug.Assert(property.ValueGenerated == ValueGenerated.OnAdd);

                return (ActianValueGenerationStrategy)annotation.Value switch
                {
                    ActianValueGenerationStrategy.IdentityByDefaultColumn => true,
                    _ => false
                };
            }

            return false;
        }

        public override MethodCallCodeFragment GenerateFluentApi(IProperty property, IAnnotation annotation)
        {
            Check.NotNull(property, nameof(property));
            Check.NotNull(annotation, nameof(annotation));

            switch (annotation.Name)
            {
                case ActianAnnotationNames.ValueGenerationStrategy:
                    return new MethodCallCodeFragment(annotation.Value switch
                    {
                        ActianValueGenerationStrategy.IdentityAlwaysColumn => nameof(ActianPropertyBuilderExtensions.UseIdentityAlwaysColumn),
                        ActianValueGenerationStrategy.IdentityByDefaultColumn => nameof(ActianPropertyBuilderExtensions.UseIdentityColumn),
                        _ => throw new ArgumentOutOfRangeException()
                    });

                case ActianAnnotationNames.IdentityOptions:
                    var identityOptions = IdentitySequenceOptionsData.Deserialize((string)annotation.Value);
                    return new MethodCallCodeFragment(
                        nameof(ActianPropertyBuilderExtensions.HasIdentityOptions),
                        identityOptions.StartValue,
                        identityOptions.IncrementBy == 1 ? null : (long?)identityOptions.IncrementBy,
                        identityOptions.MinValue,
                        identityOptions.MaxValue,
                        identityOptions.IsCyclic ? true : (bool?)null,
                        identityOptions.NumbersToCache == 1 ? null : (long?)identityOptions.NumbersToCache);
            }

            return null;
        }
    }
}
