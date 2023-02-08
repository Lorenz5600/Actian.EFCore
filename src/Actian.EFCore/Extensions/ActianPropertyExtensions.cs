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
        #region HiLo

        /// <summary>
        /// Returns the name to use for the hi-lo sequence.
        /// </summary>
        /// <param name="property"> The property.</param>
        /// <returns>The name to use for the hi-lo sequence.</returns>
        public static string GetHiLoSequenceName([NotNull] this IProperty property)
            => (string)property[ActianAnnotationNames.HiLoSequenceName];

        /// <summary>
        /// Sets the name to use for the hi-lo sequence.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="name">The sequence name to use.</param>
        public static void SetHiLoSequenceName([NotNull] this IMutableProperty property, [CanBeNull] string name)
            => property.SetOrRemoveAnnotation(
                ActianAnnotationNames.HiLoSequenceName,
                Check.NullButNotEmpty(name, nameof(name)));

        /// <summary>
        /// Sets the name to use for the hi-lo sequence.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="name">The sequence name to use.</param>
        /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
        public static void SetHiLoSequenceName(
            [NotNull] this IConventionProperty property, [CanBeNull] string name, bool fromDataAnnotation = false)
            => property.SetOrRemoveAnnotation(
                ActianAnnotationNames.HiLoSequenceName,
                Check.NullButNotEmpty(name, nameof(name)),
                fromDataAnnotation);

        /// <summary>
        /// Returns the <see cref="ConfigurationSource" /> for the hi-lo sequence name.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The <see cref="ConfigurationSource" /> for the hi-lo sequence name.</returns>
        public static ConfigurationSource? GetHiLoSequenceNameConfigurationSource([NotNull] this IConventionProperty property)
            => property.FindAnnotation(ActianAnnotationNames.HiLoSequenceName)?.GetConfigurationSource();

        /// <summary>
        /// Returns the schema to use for the hi-lo sequence.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The schema to use for the hi-lo sequence.</returns>
        public static string GetHiLoSequenceSchema([NotNull] this IProperty property)
            => (string)property[ActianAnnotationNames.HiLoSequenceSchema];

        /// <summary>
        /// Sets the schema to use for the hi-lo sequence.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="schema">The schema to use.</param>
        public static void SetHiLoSequenceSchema([NotNull] this IMutableProperty property, [CanBeNull] string schema)
            => property.SetOrRemoveAnnotation(
                ActianAnnotationNames.HiLoSequenceSchema,
                Check.NullButNotEmpty(schema, nameof(schema)));

        /// <summary>
        /// Sets the schema to use for the hi-lo sequence.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="schema">The schema to use.</param>
        /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
        public static void SetHiLoSequenceSchema(
            [NotNull] this IConventionProperty property, [CanBeNull] string schema, bool fromDataAnnotation = false)
            => property.SetOrRemoveAnnotation(
                ActianAnnotationNames.HiLoSequenceSchema,
                Check.NullButNotEmpty(schema, nameof(schema)),
                fromDataAnnotation);

        /// <summary>
        /// Returns the <see cref="ConfigurationSource" /> for the hi-lo sequence schema.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The <see cref="ConfigurationSource" /> for the hi-lo sequence schema.</returns>
        public static ConfigurationSource? GetHiLoSequenceSchemaConfigurationSource([NotNull] this IConventionProperty property)
            => property.FindAnnotation(ActianAnnotationNames.HiLoSequenceSchema)?.GetConfigurationSource();

        /// <summary>
        /// Finds the <see cref="ISequence" /> in the model to use for the hi-lo pattern.
        /// </summary>
        /// <returns>The sequence to use, or <c>null</c> if no sequence exists in the model.</returns>
        public static ISequence FindHiLoSequence([NotNull] this IProperty property)
        {
            var model = property.DeclaringEntityType.Model;

            if (property.GetValueGenerationStrategy() != ActianValueGenerationStrategy.SequenceHiLo)
            {
                return null;
            }

            var sequenceName = property.GetHiLoSequenceName() ?? model.GetHiLoSequenceName();
            var sequenceSchema = property.GetHiLoSequenceSchema() ?? model.GetHiLoSequenceSchema();
            return model.FindSequence(sequenceName, sequenceSchema);
        }

        /// <summary>
        /// Removes all identity sequence annotations from the property.
        /// </summary>
        public static void RemoveHiLoOptions([NotNull] this IMutableProperty property)
        {
            property.SetHiLoSequenceName(null);
            property.SetHiLoSequenceSchema(null);
        }

        /// <summary>
        /// Removes all identity sequence annotations from the property.
        /// </summary>
        public static void RemoveHiLoOptions([NotNull] this IConventionProperty property)
        {
            property.SetHiLoSequenceName(null);
            property.SetHiLoSequenceSchema(null);
        }

        #endregion HiLo

        #region Value Generation Strategy

        /// <summary>
        /// <para>Returns the <see cref="ActianValueGenerationStrategy" /> to use for the property.</para>
        /// <para>
        /// If no strategy is set for the property, then the strategy to use will be taken from the <see cref="IModel" />.
        /// </para>
        /// </summary>
        /// <returns>The strategy, or <see cref="ActianValueGenerationStrategy.None"/> if none was set.</returns>
        public static ActianValueGenerationStrategy GetValueGenerationStrategy([NotNull] this IProperty property)
        {
            if (property[ActianAnnotationNames.ValueGenerationStrategy] is object annotation)
                return (ActianValueGenerationStrategy)annotation;

            if (property.FindSharedTableRootPrimaryKeyProperty() is IProperty sharedTablePrincipalPrimaryKeyProperty)
            {
                var principalStrategy = sharedTablePrincipalPrimaryKeyProperty.GetValueGenerationStrategy();
                return principalStrategy switch
                {
                    ActianValueGenerationStrategy.IdentityByDefaultColumn => principalStrategy,
                    ActianValueGenerationStrategy.IdentityAlwaysColumn => principalStrategy,
                    _ => ActianValueGenerationStrategy.None
                };
            }

            if (property.ValueGenerated != ValueGenerated.OnAdd
                || property.GetDefaultValue() != null
                || property.GetDefaultValueSql() != null
                || property.GetComputedColumnSql() != null
                || !IsCompatibleWithValueGeneration(property)
                || !property.ClrType.IsIntegerForValueGeneration())
            {
                return ActianValueGenerationStrategy.None;
            }

            return property.DeclaringEntityType.Model.GetValueGenerationStrategy()
                ?? ActianValueGenerationStrategy.None;
        }

        /// <summary>
        /// Sets the <see cref="ActianValueGenerationStrategy" /> to use for the property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="value">The strategy to use.</param>
        public static void SetValueGenerationStrategy(
            [NotNull] this IMutableProperty property, ActianValueGenerationStrategy? value)
        {
            CheckValueGenerationStrategy(property, value);

            property.SetOrRemoveAnnotation(ActianAnnotationNames.ValueGenerationStrategy, value);
        }

        /// <summary>
        /// Sets the <see cref="ActianValueGenerationStrategy" /> to use for the property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="value">The strategy to use.</param>
        /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
        public static void SetValueGenerationStrategy(
            [NotNull] this IConventionProperty property, ActianValueGenerationStrategy? value, bool fromDataAnnotation = false)
        {
            CheckValueGenerationStrategy(property, value);

            property.SetOrRemoveAnnotation(ActianAnnotationNames.ValueGenerationStrategy, value, fromDataAnnotation);
        }

        static void CheckValueGenerationStrategy(IProperty property, ActianValueGenerationStrategy? value)
        {
            if (value != null)
            {
                var propertyType = property.ClrType;

                if ((value == ActianValueGenerationStrategy.IdentityAlwaysColumn || value == ActianValueGenerationStrategy.IdentityByDefaultColumn)
                    && !propertyType.IsIntegerForValueGeneration())
                {
                    throw new ArgumentException($"Identity value generation cannot be used for the property '{property.Name}' on entity type '{property.DeclaringEntityType.DisplayName()}' because the property type is '{propertyType.ShortDisplayName()}'. Identity columns can only be of type short, int or long.");
                }

                if (value == ActianValueGenerationStrategy.SequenceHiLo && !propertyType.IsInteger())
                {
                    throw new ArgumentException($"Actian sequences cannot be used to generate values for the property '{property.Name}' on entity type '{property.DeclaringEntityType.DisplayName()}' because the property type is '{propertyType.ShortDisplayName()}'. Sequences can only be used with integer properties.");
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="ConfigurationSource" /> for the <see cref="ActianValueGenerationStrategy" />.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The <see cref="ConfigurationSource" /> for the <see cref="ActianValueGenerationStrategy" />.</returns>
        public static ConfigurationSource? GetValueGenerationStrategyConfigurationSource(
            [NotNull] this IConventionProperty property)
            => property.FindAnnotation(ActianAnnotationNames.ValueGenerationStrategy)?.GetConfigurationSource();

        /// <summary>
        /// Returns a value indicating whether the property is compatible with any <see cref="ActianValueGenerationStrategy"/>.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns><c>true</c> if compatible.</returns>
        public static bool IsCompatibleWithValueGeneration([NotNull] IProperty property)
        {
            var type = property.ClrType;

            return type.IsIntegerForValueGeneration()
                   && (property.FindTypeMapping()?.Converter ?? property.GetValueConverter()) == null;
        }

        static bool IsIntegerForValueGeneration(this Type type)
        {
            type = type.UnwrapNullableType();
            return type == typeof(int) || type == typeof(long) || type == typeof(short);
        }

        #endregion Value Generation Strategy

        #region Identity sequence options

        /// <summary>
        /// Returns the identity start value.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The identity start value.</returns>
        public static long? GetIdentityStartValue([NotNull] this IProperty property)
            => IdentitySequenceOptionsData.Get(property).StartValue;

        /// <summary>
        /// Sets the identity start value.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="startValue">The value to set.</param>
        public static void SetIdentityStartValue([NotNull] this IMutableProperty property, long? startValue)
        {
            var options = IdentitySequenceOptionsData.Get(property);
            options.StartValue = startValue;
            property.SetOrRemoveAnnotation(ActianAnnotationNames.IdentityOptions, options.Serialize());
        }

        /// <summary>
        /// Sets the identity start value.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="startValue">The value to set.</param>
        /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
        public static void SetIdentityStartValue(
            [NotNull] this IConventionProperty property, long? startValue, bool fromDataAnnotation = false)
        {
            var options = IdentitySequenceOptionsData.Get(property);
            options.StartValue = startValue;
            property.SetOrRemoveAnnotation(ActianAnnotationNames.IdentityOptions, options.Serialize(), fromDataAnnotation);
        }

        /// <summary>
        /// Returns the identity increment value.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The identity increment value.</returns>
        public static long? GetIdentityIncrementBy([NotNull] this IProperty property)
            => IdentitySequenceOptionsData.Get(property).IncrementBy;

        /// <summary>
        /// Sets the identity increment value.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="incrementBy">The value to set.</param>
        public static void SetIdentityIncrementBy([NotNull] this IMutableProperty property, long? incrementBy)
        {
            var options = IdentitySequenceOptionsData.Get(property);
            options.IncrementBy = incrementBy ?? 1;
            property.SetOrRemoveAnnotation(ActianAnnotationNames.IdentityOptions, options.Serialize());
        }

        /// <summary>
        /// Sets the identity increment value.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="incrementBy">The value to set.</param>
        /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
        public static void SetIdentityIncrementBy(
            [NotNull] this IConventionProperty property, long? incrementBy, bool fromDataAnnotation = false)
        {
            var options = IdentitySequenceOptionsData.Get(property);
            options.IncrementBy = incrementBy ?? 1;
            property.SetOrRemoveAnnotation(ActianAnnotationNames.IdentityOptions, options.Serialize(), fromDataAnnotation);
        }

        /// <summary>
        /// Returns the identity minimum value.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The identity minimum value.</returns>
        public static long? GetIdentityMinValue([NotNull] this IProperty property)
            => IdentitySequenceOptionsData.Get(property).MinValue;

        /// <summary>
        /// Sets the identity minimum value.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="minValue">The value to set.</param>
        public static void SetIdentityMinValue([NotNull] this IMutableProperty property, long? minValue)
        {
            var options = IdentitySequenceOptionsData.Get(property);
            options.MinValue = minValue;
            property.SetOrRemoveAnnotation(ActianAnnotationNames.IdentityOptions, options.Serialize());
        }

        /// <summary>
        /// Sets the identity minimum value.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="minValue">The value to set.</param>
        /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
        public static void SetIdentityMinValue(
            [NotNull] this IConventionProperty property, long? minValue, bool fromDataAnnotation = false)
        {
            var options = IdentitySequenceOptionsData.Get(property);
            options.MinValue = minValue;
            property.SetOrRemoveAnnotation(ActianAnnotationNames.IdentityOptions, options.Serialize(), fromDataAnnotation);
        }

        /// <summary>
        /// Returns the identity maximum value.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The identity maximum value.</returns>
        public static long? GetIdentityMaxValue([NotNull] this IProperty property)
            => IdentitySequenceOptionsData.Get(property).MaxValue;

        /// <summary>
        /// Sets the identity maximum value.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="maxValue">The value to set.</param>
        public static void SetIdentityMaxValue([NotNull] this IMutableProperty property, long? maxValue)
        {
            var options = IdentitySequenceOptionsData.Get(property);
            options.MaxValue = maxValue;
            property.SetOrRemoveAnnotation(ActianAnnotationNames.IdentityOptions, options.Serialize());
        }

        /// <summary>
        /// Sets the identity maximum value.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="maxValue">The value to set.</param>
        /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
        public static void SetIdentityMaxValue(
            [NotNull] this IConventionProperty property, long? maxValue, bool fromDataAnnotation = false)
        {
            var options = IdentitySequenceOptionsData.Get(property);
            options.MaxValue = maxValue;
            property.SetOrRemoveAnnotation(ActianAnnotationNames.IdentityOptions, options.Serialize(), fromDataAnnotation);
        }

        /// <summary>
        /// Returns whether the identity's sequence is cyclic.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>Whether the identity's sequence is cyclic.</returns>
        public static bool? GetIdentityIsCyclic([NotNull] this IProperty property)
            => IdentitySequenceOptionsData.Get(property).IsCyclic;

        /// <summary>
        /// Sets whether the identity's sequence is cyclic.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="isCyclic">The value to set.</param>
        public static void SetIdentityIsCyclic([NotNull] this IMutableProperty property, bool? isCyclic)
        {
            var options = IdentitySequenceOptionsData.Get(property);
            options.IsCyclic = isCyclic ?? false;
            property.SetOrRemoveAnnotation(ActianAnnotationNames.IdentityOptions, options.Serialize());
        }

        /// <summary>
        /// Sets whether the identity's sequence is cyclic.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="isCyclic">The value to set.</param>
        /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
        public static void SetIdentityIsCyclic(
            [NotNull] this IConventionProperty property, bool? isCyclic, bool fromDataAnnotation = false)
        {
            var options = IdentitySequenceOptionsData.Get(property);
            options.IsCyclic = isCyclic ?? false;
            property.SetOrRemoveAnnotation(ActianAnnotationNames.IdentityOptions, options.Serialize(), fromDataAnnotation);
        }

        /// <summary>
        /// Returns the number of sequence numbers to be preallocated and stored in memory for faster access.
        /// Defaults to 1 (no cache).
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The number of sequence numbers to be cached.</returns>
        public static long? GetIdentityNumbersToCache([NotNull] this IProperty property)
            => IdentitySequenceOptionsData.Get(property).NumbersToCache;

        /// <summary>
        /// Sets the number of sequence numbers to be preallocated and stored in memory for faster access.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="numbersToCache">The value to set.</param>
        public static void SetIdentityNumbersToCache([NotNull] this IMutableProperty property, long? numbersToCache)
        {
            var options = IdentitySequenceOptionsData.Get(property);
            options.NumbersToCache = numbersToCache ?? 1;
            property.SetOrRemoveAnnotation(ActianAnnotationNames.IdentityOptions, options.Serialize());
        }

        /// <summary>
        /// Sets the number of sequence numbers to be preallocated and stored in memory for faster access.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="numbersToCache">The value to set.</param>
        /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
        public static void SetIdentityNumbersToCache(
            [NotNull] this IConventionProperty property, long? numbersToCache, bool fromDataAnnotation = false)
        {
            var options = IdentitySequenceOptionsData.Get(property);
            options.NumbersToCache = numbersToCache ?? 1;
            property.SetOrRemoveAnnotation(ActianAnnotationNames.IdentityOptions, options.Serialize(), fromDataAnnotation);
        }

        /// <summary>
        /// Returns the <see cref="ConfigurationSource" /> for the identity sequence options.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The <see cref="ConfigurationSource" /> for the identity sequence options.</returns>
        public static ConfigurationSource? GetIdentityOptionsConfigurationSource([NotNull] this IConventionProperty property)
            => property.FindAnnotation(ActianAnnotationNames.IdentityOptions)?.GetConfigurationSource();


        /// <summary>
        /// Removes identity sequence options from the property.
        /// </summary>
        public static void RemoveIdentityOptions([NotNull] this IMutableProperty property)
            => property.RemoveAnnotation(ActianAnnotationNames.IdentityOptions);

        /// <summary>
        /// Removes identity sequence options from the property.
        /// </summary>
        public static void RemoveIdentityOptions([NotNull] this IConventionProperty property)
            => property.RemoveAnnotation(ActianAnnotationNames.IdentityOptions);

        #endregion Identity sequence options
    }
}
