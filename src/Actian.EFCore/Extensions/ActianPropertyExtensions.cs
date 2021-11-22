using System;
using Actian.EFCore.Internal;
using Actian.EFCore.Metadata.Internal;
using Actian.EFCore.Utilities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Actian.EFCore
{
    /// <summary>
    /// Extension methods for <see cref="IProperty" /> for Actian-specific metadata.
    /// </summary>
    public static class ActianPropertyExtensions
    {
        /// <summary>
        /// Returns the name to use for the hi-lo sequence.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <returns> The name to use for the hi-lo sequence. </returns>
        public static string GetHiLoSequenceName([NotNull] this IProperty property)
            => (string)property[ActianAnnotationNames.HiLoSequenceName];

        /// <summary>
        /// Sets the name to use for the hi-lo sequence.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <param name="name"> The sequence name to use. </param>
        public static void SetHiLoSequenceName([NotNull] this IMutableProperty property, [CanBeNull] string name)
            => property.SetOrRemoveAnnotation(
                ActianAnnotationNames.HiLoSequenceName,
                Check.NullButNotEmpty(name, nameof(name)));

        /// <summary>
        /// Sets the name to use for the hi-lo sequence.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <param name="name"> The sequence name to use. </param>
        /// <param name="fromDataAnnotation"> Indicates whether the configuration was specified using a data annotation. </param>
        public static void SetHiLoSequenceName(
            [NotNull] this IConventionProperty property, [CanBeNull] string name, bool fromDataAnnotation = false)
            => property.SetOrRemoveAnnotation(
                ActianAnnotationNames.HiLoSequenceName,
                Check.NullButNotEmpty(name, nameof(name)),
                fromDataAnnotation);

        /// <summary>
        /// Returns the <see cref="ConfigurationSource" /> for the hi-lo sequence name.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <returns> The <see cref="ConfigurationSource" /> for the hi-lo sequence name. </returns>
        public static ConfigurationSource? GetHiLoSequenceNameConfigurationSource([NotNull] this IConventionProperty property)
            => property.FindAnnotation(ActianAnnotationNames.HiLoSequenceName)?.GetConfigurationSource();

        /// <summary>
        /// Returns the schema to use for the hi-lo sequence.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <returns> The schema to use for the hi-lo sequence. </returns>
        public static string GetHiLoSequenceSchema([NotNull] this IProperty property)
            => (string)property[ActianAnnotationNames.HiLoSequenceSchema];

        /// <summary>
        /// Sets the schema to use for the hi-lo sequence.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <param name="schema"> The schema to use. </param>
        public static void SetHiLoSequenceSchema([NotNull] this IMutableProperty property, [CanBeNull] string schema)
            => property.SetOrRemoveAnnotation(
                ActianAnnotationNames.HiLoSequenceSchema,
                Check.NullButNotEmpty(schema, nameof(schema)));

        /// <summary>
        /// Sets the schema to use for the hi-lo sequence.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <param name="schema"> The schema to use. </param>
        /// <param name="fromDataAnnotation"> Indicates whether the configuration was specified using a data annotation. </param>
        public static void SetHiLoSequenceSchema(
            [NotNull] this IConventionProperty property, [CanBeNull] string schema, bool fromDataAnnotation = false)
            => property.SetOrRemoveAnnotation(
                ActianAnnotationNames.HiLoSequenceSchema,
                Check.NullButNotEmpty(schema, nameof(schema)),
                fromDataAnnotation);

        /// <summary>
        /// Returns the <see cref="ConfigurationSource" /> for the hi-lo sequence schema.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <returns> The <see cref="ConfigurationSource" /> for the hi-lo sequence schema. </returns>
        public static ConfigurationSource? GetHiLoSequenceSchemaConfigurationSource([NotNull] this IConventionProperty property)
            => property.FindAnnotation(ActianAnnotationNames.HiLoSequenceSchema)?.GetConfigurationSource();

        /// <summary>
        /// Finds the <see cref="ISequence" /> in the model to use for the hi-lo pattern.
        /// </summary>
        /// <returns> The sequence to use, or <c>null</c> if no sequence exists in the model. </returns>
        public static ISequence FindHiLoSequence([NotNull] this IProperty property)
        {
            var model = property.DeclaringEntityType.Model;

            if (property.GetValueGenerationStrategy() != ActianValueGenerationStrategy.SequenceHiLo)
            {
                return null;
            }

            var sequenceName = property.GetHiLoSequenceName()
                ?? model.GetHiLoSequenceName();

            var sequenceSchema = property.GetHiLoSequenceSchema()
                ?? model.GetHiLoSequenceSchema();

            return model.FindSequence(sequenceName, sequenceSchema);
        }

        /// <summary>
        /// Returns the identity seed.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <returns> The identity seed. </returns>
        public static int? GetIdentitySeed([NotNull] this IProperty property)
            => (int?)property[ActianAnnotationNames.IdentitySeed];

        /// <summary>
        /// Sets the identity seed.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <param name="seed"> The value to set. </param>
        public static void SetIdentitySeed([NotNull] this IMutableProperty property, int? seed)
            => property.SetOrRemoveAnnotation(
                ActianAnnotationNames.IdentitySeed,
                seed);

        /// <summary>
        /// Sets the identity seed.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <param name="seed"> The value to set. </param>
        /// <param name="fromDataAnnotation"> Indicates whether the configuration was specified using a data annotation. </param>
        public static void SetIdentitySeed(
            [NotNull] this IConventionProperty property, int? seed, bool fromDataAnnotation = false)
            => property.SetOrRemoveAnnotation(
                ActianAnnotationNames.IdentitySeed,
                seed,
                fromDataAnnotation);

        /// <summary>
        /// Returns the <see cref="ConfigurationSource" /> for the identity seed.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <returns> The <see cref="ConfigurationSource" /> for the identity seed. </returns>
        public static ConfigurationSource? GetIdentitySeedConfigurationSource([NotNull] this IConventionProperty property)
            => property.FindAnnotation(ActianAnnotationNames.IdentitySeed)?.GetConfigurationSource();

        /// <summary>
        /// Returns the identity increment.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <returns> The identity increment. </returns>
        public static int? GetIdentityIncrement([NotNull] this IProperty property)
            => (int?)property[ActianAnnotationNames.IdentityIncrement];

        /// <summary>
        /// Sets the identity increment.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <param name="increment"> The value to set. </param>
        public static void SetIdentityIncrement([NotNull] this IMutableProperty property, int? increment)
            => property.SetOrRemoveAnnotation(
                ActianAnnotationNames.IdentityIncrement,
                increment);

        /// <summary>
        /// Sets the identity increment.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <param name="increment"> The value to set. </param>
        /// <param name="fromDataAnnotation"> Indicates whether the configuration was specified using a data annotation. </param>
        public static void SetIdentityIncrement(
            [NotNull] this IConventionProperty property, int? increment, bool fromDataAnnotation = false)
            => property.SetOrRemoveAnnotation(
                ActianAnnotationNames.IdentityIncrement,
                increment,
                fromDataAnnotation);

        /// <summary>
        /// Returns the <see cref="ConfigurationSource" /> for the identity increment.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <returns> The <see cref="ConfigurationSource" /> for the identity increment. </returns>
        public static ConfigurationSource? GetIdentityIncrementConfigurationSource([NotNull] this IConventionProperty property)
            => property.FindAnnotation(ActianAnnotationNames.IdentityIncrement)?.GetConfigurationSource();

        /// <summary>
        /// <para>
        ///     Returns the <see cref="ActianValueGenerationStrategy" /> to use for the property.
        /// </para>
        /// <para>
        ///     If no strategy is set for the property, then the strategy to use will be taken from the <see cref="IModel" />.
        /// </para>
        /// </summary>
        /// <returns> The strategy, or <see cref="ActianValueGenerationStrategy.None" /> if none was set. </returns>
        public static ActianValueGenerationStrategy GetValueGenerationStrategy([NotNull] this IProperty property)
        {
            var annotation = property[ActianAnnotationNames.ValueGenerationStrategy];
            if (annotation != null)
            {
                return (ActianValueGenerationStrategy)annotation;
            }

            var sharedTablePrincipalPrimaryKeyProperty = property.FindSharedTableRootPrimaryKeyProperty();
            if (sharedTablePrincipalPrimaryKeyProperty != null)
            {
                return sharedTablePrincipalPrimaryKeyProperty.GetValueGenerationStrategy()
                    == ActianValueGenerationStrategy.IdentityColumn
                        ? ActianValueGenerationStrategy.IdentityColumn
                        : ActianValueGenerationStrategy.None;
            }

            if (property.ValueGenerated != ValueGenerated.OnAdd
                || property.GetDefaultValue() != null
                || property.GetDefaultValueSql() != null
                || property.GetComputedColumnSql() != null)
            {
                return ActianValueGenerationStrategy.None;
            }

            var modelStrategy = property.DeclaringEntityType.Model.GetValueGenerationStrategy();

            if (modelStrategy == ActianValueGenerationStrategy.SequenceHiLo
                && IsCompatibleWithValueGeneration(property))
            {
                return ActianValueGenerationStrategy.SequenceHiLo;
            }

            return modelStrategy == ActianValueGenerationStrategy.IdentityColumn
                && IsCompatibleWithValueGeneration(property)
                    ? ActianValueGenerationStrategy.IdentityColumn
                    : ActianValueGenerationStrategy.None;
        }

        /// <summary>
        /// Sets the <see cref="ActianValueGenerationStrategy" /> to use for the property.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <param name="value"> The strategy to use. </param>
        public static void SetValueGenerationStrategy(
            [NotNull] this IMutableProperty property, ActianValueGenerationStrategy? value)
        {
            CheckValueGenerationStrategy(property, value);

            property.SetOrRemoveAnnotation(ActianAnnotationNames.ValueGenerationStrategy, value);
        }

        /// <summary>
        /// Sets the <see cref="ActianValueGenerationStrategy" /> to use for the property.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <param name="value"> The strategy to use. </param>
        /// <param name="fromDataAnnotation"> Indicates whether the configuration was specified using a data annotation. </param>
        public static void SetValueGenerationStrategy(
            [NotNull] this IConventionProperty property, ActianValueGenerationStrategy? value, bool fromDataAnnotation = false)
        {
            CheckValueGenerationStrategy(property, value);

            property.SetOrRemoveAnnotation(ActianAnnotationNames.ValueGenerationStrategy, value, fromDataAnnotation);
        }

        private static void CheckValueGenerationStrategy(IProperty property, ActianValueGenerationStrategy? value)
        {
            if (value != null)
            {
                var propertyType = property.ClrType;

                if (value == ActianValueGenerationStrategy.IdentityColumn
                    && !IsCompatibleWithValueGeneration(property))
                {
                    throw new ArgumentException(
                        ActianStrings.IdentityBadType(
                            property.Name, property.DeclaringEntityType.DisplayName(), propertyType.ShortDisplayName()));
                }

                if (value == ActianValueGenerationStrategy.SequenceHiLo
                    && !IsCompatibleWithValueGeneration(property))
                {
                    throw new ArgumentException(
                        ActianStrings.SequenceBadType(
                            property.Name, property.DeclaringEntityType.DisplayName(), propertyType.ShortDisplayName()));
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="ConfigurationSource" /> for the <see cref="ActianValueGenerationStrategy" />.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <returns> The <see cref="ConfigurationSource" /> for the <see cref="ActianValueGenerationStrategy" />. </returns>
        public static ConfigurationSource? GetValueGenerationStrategyConfigurationSource(
            [NotNull] this IConventionProperty property)
            => property.FindAnnotation(ActianAnnotationNames.ValueGenerationStrategy)?.GetConfigurationSource();

        /// <summary>
        /// Returns a value indicating whether the property is compatible with any <see cref="ActianValueGenerationStrategy" />.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <returns> <c>true</c> if compatible. </returns>
        public static bool IsCompatibleWithValueGeneration([NotNull] IProperty property)
        {
            var type = property.ClrType;

            return (type.IsInteger()
                    || type == typeof(decimal))
                && (property.FindTypeMapping()?.Converter
                    ?? property.GetValueConverter())
                == null;
        }
    }
}
