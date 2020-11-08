using Actian.EFCore.Metadata.Internal;
using Actian.EFCore.Utilities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Actian.EFCore
{
    /// <summary>
    /// Extension methods for <see cref="IModel" /> for SQL Server-specific metadata.
    /// </summary>
    public static class ActianModelExtensions
    {
        /// <summary>
        /// The default name for the hi-lo sequence.
        /// </summary>
        public const string DefaultHiLoSequenceName = "EntityFrameworkHiLoSequence";

        /// <summary>
        /// Returns the name to use for the default hi-lo sequence.
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <returns> The name to use for the default hi-lo sequence. </returns>
        public static string GetHiLoSequenceName([NotNull] this IModel model)
            => (string)model[ActianAnnotationNames.HiLoSequenceName]
                ?? DefaultHiLoSequenceName;

        /// <summary>
        /// Sets the name to use for the default hi-lo sequence.
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <param name="name"> The value to set. </param>
        public static void SetHiLoSequenceName([NotNull] this IMutableModel model, [CanBeNull] string name)
        {
            Check.NullButNotEmpty(name, nameof(name));

            model.SetOrRemoveAnnotation(ActianAnnotationNames.HiLoSequenceName, name);
        }

        /// <summary>
        /// Sets the name to use for the default hi-lo sequence.
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <param name="name"> The value to set. </param>
        /// <param name="fromDataAnnotation"> Indicates whether the configuration was specified using a data annotation. </param>
        public static void SetHiLoSequenceName(
            [NotNull] this IConventionModel model, [CanBeNull] string name, bool fromDataAnnotation = false)
        {
            Check.NullButNotEmpty(name, nameof(name));

            model.SetOrRemoveAnnotation(ActianAnnotationNames.HiLoSequenceName, name, fromDataAnnotation);
        }

        /// <summary>
        /// Returns the <see cref="ConfigurationSource" /> for the default hi-lo sequence name.
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <returns> The <see cref="ConfigurationSource" /> for the default hi-lo sequence name. </returns>
        public static ConfigurationSource? GetHiLoSequenceNameConfigurationSource([NotNull] this IConventionModel model)
            => model.FindAnnotation(ActianAnnotationNames.HiLoSequenceName)?.GetConfigurationSource();

        /// <summary>
        /// Returns the schema to use for the default hi-lo sequence.
        /// <see cref="ActianPropertyBuilderExtensions.UseHiLo" />
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <returns> The schema to use for the default hi-lo sequence. </returns>
        public static string GetHiLoSequenceSchema([NotNull] this IModel model)
            => (string)model[ActianAnnotationNames.HiLoSequenceSchema];

        /// <summary>
        /// Sets the schema to use for the default hi-lo sequence.
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <param name="value"> The value to set. </param>
        public static void SetHiLoSequenceSchema([NotNull] this IMutableModel model, [CanBeNull] string value)
        {
            Check.NullButNotEmpty(value, nameof(value));

            model.SetOrRemoveAnnotation(ActianAnnotationNames.HiLoSequenceSchema, value);
        }

        /// <summary>
        /// Sets the schema to use for the default hi-lo sequence.
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <param name="value"> The value to set. </param>
        /// <param name="fromDataAnnotation"> Indicates whether the configuration was specified using a data annotation. </param>
        public static void SetHiLoSequenceSchema(
            [NotNull] this IConventionModel model, [CanBeNull] string value, bool fromDataAnnotation = false)
        {
            Check.NullButNotEmpty(value, nameof(value));

            model.SetOrRemoveAnnotation(ActianAnnotationNames.HiLoSequenceSchema, value, fromDataAnnotation);
        }

        /// <summary>
        /// Returns the <see cref="ConfigurationSource" /> for the default hi-lo sequence schema.
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <returns> The <see cref="ConfigurationSource" /> for the default hi-lo sequence schema. </returns>
        public static ConfigurationSource? GetHiLoSequenceSchemaConfigurationSource([NotNull] this IConventionModel model)
            => model.FindAnnotation(ActianAnnotationNames.HiLoSequenceSchema)?.GetConfigurationSource();

        /// <summary>
        /// Returns the default identity seed.
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <returns> The default identity seed. </returns>
        public static int GetIdentitySeed([NotNull] this IModel model)
            => (int?)model[ActianAnnotationNames.IdentitySeed] ?? 1;

        /// <summary>
        /// Sets the default identity seed.
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <param name="seed"> The value to set. </param>
        public static void SetIdentitySeed([NotNull] this IMutableModel model, int? seed)
            => model.SetOrRemoveAnnotation(
                ActianAnnotationNames.IdentitySeed,
                seed);

        /// <summary>
        /// Sets the default identity seed.
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <param name="seed"> The value to set. </param>
        /// <param name="fromDataAnnotation"> Indicates whether the configuration was specified using a data annotation. </param>
        public static void SetIdentitySeed([NotNull] this IConventionModel model, int? seed, bool fromDataAnnotation = false)
            => model.SetOrRemoveAnnotation(
                ActianAnnotationNames.IdentitySeed,
                seed,
                fromDataAnnotation);

        /// <summary>
        /// Returns the <see cref="ConfigurationSource" /> for the default schema.
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <returns> The <see cref="ConfigurationSource" /> for the default schema. </returns>
        public static ConfigurationSource? GetIdentitySeedConfigurationSource([NotNull] this IConventionModel model)
            => model.FindAnnotation(ActianAnnotationNames.IdentitySeed)?.GetConfigurationSource();

        /// <summary>
        /// Returns the default identity increment.
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <returns> The default identity increment. </returns>
        public static int GetIdentityIncrement([NotNull] this IModel model)
            => (int?)model[ActianAnnotationNames.IdentityIncrement] ?? 1;

        /// <summary>
        /// Sets the default identity increment.
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <param name="increment"> The value to set. </param>
        public static void SetIdentityIncrement([NotNull] this IMutableModel model, int? increment)
            => model.SetOrRemoveAnnotation(
                ActianAnnotationNames.IdentityIncrement,
                increment);

        /// <summary>
        /// Sets the default identity increment.
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <param name="increment"> The value to set. </param>
        /// <param name="fromDataAnnotation"> Indicates whether the configuration was specified using a data annotation. </param>
        public static void SetIdentityIncrement(
            [NotNull] this IConventionModel model, int? increment, bool fromDataAnnotation = false)
            => model.SetOrRemoveAnnotation(
                ActianAnnotationNames.IdentityIncrement,
                increment,
                fromDataAnnotation);

        /// <summary>
        /// Returns the <see cref="ConfigurationSource" /> for the default identity increment.
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <returns> The <see cref="ConfigurationSource" /> for the default identity increment. </returns>
        public static ConfigurationSource? GetIdentityIncrementConfigurationSource([NotNull] this IConventionModel model)
            => model.FindAnnotation(ActianAnnotationNames.IdentityIncrement)?.GetConfigurationSource();

        /// <summary>
        /// Returns the <see cref="ActianValueGenerationStrategy" /> to use for properties
        /// of keys in the model, unless the property has a strategy explicitly set.
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <returns> The default <see cref="ActianValueGenerationStrategy" />. </returns>
        public static ActianValueGenerationStrategy? GetValueGenerationStrategy([NotNull] this IModel model)
            => (ActianValueGenerationStrategy?)model[ActianAnnotationNames.ValueGenerationStrategy];

        /// <summary>
        /// Sets the <see cref="ActianValueGenerationStrategy" /> to use for properties
        /// of keys in the model that don't have a strategy explicitly set.
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <param name="value"> The value to set. </param>
        public static void SetValueGenerationStrategy([NotNull] this IMutableModel model, ActianValueGenerationStrategy? value)
            => model.SetOrRemoveAnnotation(ActianAnnotationNames.ValueGenerationStrategy, value);

        /// <summary>
        /// Sets the <see cref="ActianValueGenerationStrategy" /> to use for properties
        /// of keys in the model that don't have a strategy explicitly set.
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <param name="value"> The value to set. </param>
        /// <param name="fromDataAnnotation"> Indicates whether the configuration was specified using a data annotation. </param>
        public static void SetValueGenerationStrategy(
            [NotNull] this IConventionModel model, ActianValueGenerationStrategy? value, bool fromDataAnnotation = false)
            => model.SetOrRemoveAnnotation(ActianAnnotationNames.ValueGenerationStrategy, value, fromDataAnnotation);

        /// <summary>
        /// Returns the <see cref="ConfigurationSource" /> for the default <see cref="ActianValueGenerationStrategy" />.
        /// </summary>
        /// <param name="model"> The model. </param>
        /// <returns> The <see cref="ConfigurationSource" /> for the default <see cref="ActianValueGenerationStrategy" />. </returns>
        public static ConfigurationSource? GetValueGenerationStrategyConfigurationSource([NotNull] this IConventionModel model)
            => model.FindAnnotation(ActianAnnotationNames.ValueGenerationStrategy)?.GetConfigurationSource();
    }
}
