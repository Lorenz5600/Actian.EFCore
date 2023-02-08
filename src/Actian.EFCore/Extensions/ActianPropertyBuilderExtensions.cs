using Actian.EFCore;
using Actian.EFCore.Metadata.Internal;
using Actian.EFCore.Utilities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// Actian specific extension methods for <see cref="ModelBuilder" />.
    /// </summary>
    public static class ActianPropertyBuilderExtensions
    {
        #region HiLo

        public static PropertyBuilder UseHiLo(
            [NotNull] this PropertyBuilder propertyBuilder,
            [CanBeNull] string name = null,
            [CanBeNull] string schema = null)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));
            Check.NullButNotEmpty(name, nameof(name));
            Check.NullButNotEmpty(schema, nameof(schema));

            var property = propertyBuilder.Metadata;

            name ??= ActianModelExtensions.DefaultHiLoSequenceName;

            var model = property.DeclaringEntityType.Model;

            if (model.FindSequence(name, schema) == null)
            {
                model.AddSequence(name, schema).IncrementBy = 10;
            }

            property.SetValueGenerationStrategy(ActianValueGenerationStrategy.SequenceHiLo);
            property.SetHiLoSequenceName(name);
            property.SetHiLoSequenceSchema(schema);
            property.RemoveIdentityOptions();

            return propertyBuilder;
        }

        public static PropertyBuilder<TProperty> UseHiLo<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder,
            [CanBeNull] string name = null,
            [CanBeNull] string schema = null)
            => (PropertyBuilder<TProperty>)UseHiLo((PropertyBuilder)propertyBuilder, name, schema);

        public static IConventionSequenceBuilder HasHiLoSequence(
            [NotNull] this IConventionPropertyBuilder propertyBuilder,
            [CanBeNull] string name,
            [CanBeNull] string schema,
            bool fromDataAnnotation = false)
        {
            if (!propertyBuilder.CanSetHiLoSequence(name, schema, fromDataAnnotation))
            {
                return null;
            }

            propertyBuilder.Metadata.SetHiLoSequenceName(name, fromDataAnnotation);
            propertyBuilder.Metadata.SetHiLoSequenceSchema(schema, fromDataAnnotation);

            return name == null
                ? null
                : propertyBuilder.Metadata.DeclaringEntityType.Model.Builder.HasSequence(name, schema, fromDataAnnotation);
        }

        public static bool CanSetHiLoSequence(
            [NotNull] this IConventionPropertyBuilder propertyBuilder,
            [CanBeNull] string name,
            [CanBeNull] string schema,
            bool fromDataAnnotation = false)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));
            Check.NullButNotEmpty(name, nameof(name));
            Check.NullButNotEmpty(schema, nameof(schema));

            return propertyBuilder.CanSetAnnotation(ActianAnnotationNames.HiLoSequenceName, name, fromDataAnnotation)
                && propertyBuilder.CanSetAnnotation(ActianAnnotationNames.HiLoSequenceSchema, schema, fromDataAnnotation);
        }

        #endregion HiLo

        #region Identity always

        public static PropertyBuilder UseIdentityAlwaysColumn([NotNull] this PropertyBuilder propertyBuilder)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));

            var property = propertyBuilder.Metadata;
            property.SetValueGenerationStrategy(ActianValueGenerationStrategy.IdentityAlwaysColumn);
            property.SetHiLoSequenceName(null);
            property.SetHiLoSequenceSchema(null);

            return propertyBuilder;
        }

        public static PropertyBuilder<TProperty> UseIdentityAlwaysColumn<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder)
            => (PropertyBuilder<TProperty>)UseIdentityAlwaysColumn((PropertyBuilder)propertyBuilder);

        public static IConventionPropertyBuilder UseIdentityAlwaysColumn([NotNull] this IConventionPropertyBuilder propertyBuilder)
        {
            if (propertyBuilder.CanSetValueGenerationStrategy(ActianValueGenerationStrategy.IdentityAlwaysColumn))
            {
                var property = propertyBuilder.Metadata;
                property.SetValueGenerationStrategy(ActianValueGenerationStrategy.IdentityAlwaysColumn);
                property.SetHiLoSequenceName(null);
                property.SetHiLoSequenceSchema(null);

                return propertyBuilder;
            }

            return null;
        }

        #endregion Identity always

        #region Identity by default

        public static PropertyBuilder UseIdentityByDefaultColumn([NotNull] this PropertyBuilder propertyBuilder)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));

            var property = propertyBuilder.Metadata;
            property.SetValueGenerationStrategy(ActianValueGenerationStrategy.IdentityByDefaultColumn);
            property.SetHiLoSequenceName(null);
            property.SetHiLoSequenceSchema(null);

            return propertyBuilder;
        }

        public static PropertyBuilder<TProperty> UseIdentityByDefaultColumn<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder)
            => (PropertyBuilder<TProperty>)UseIdentityByDefaultColumn((PropertyBuilder)propertyBuilder);

        public static IConventionPropertyBuilder UseIdentityByDefaultColumn([NotNull] this IConventionPropertyBuilder propertyBuilder)
        {
            if (propertyBuilder.CanSetValueGenerationStrategy(ActianValueGenerationStrategy.IdentityByDefaultColumn))
            {
                var property = propertyBuilder.Metadata;
                property.SetValueGenerationStrategy(ActianValueGenerationStrategy.IdentityByDefaultColumn);
                property.SetHiLoSequenceName(null);
                property.SetHiLoSequenceSchema(null);

                return propertyBuilder;
            }

            return null;
        }

        public static PropertyBuilder UseIdentityColumn(
            [NotNull] this PropertyBuilder propertyBuilder)
            => propertyBuilder.UseIdentityByDefaultColumn();

        public static PropertyBuilder<TProperty> UseIdentityColumn<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder)
            => propertyBuilder.UseIdentityByDefaultColumn();

        #endregion Identity by default

        #region Value Generation Strategy

        public static IConventionPropertyBuilder HasValueGenerationStrategy(
            [NotNull] this IConventionPropertyBuilder propertyBuilder,
            ActianValueGenerationStrategy? valueGenerationStrategy,
            bool fromDataAnnotation = false)
        {
            if (propertyBuilder.CanSetAnnotation(
                ActianAnnotationNames.ValueGenerationStrategy, valueGenerationStrategy, fromDataAnnotation))
            {
                propertyBuilder.Metadata.SetValueGenerationStrategy(valueGenerationStrategy, fromDataAnnotation);
                if (valueGenerationStrategy != ActianValueGenerationStrategy.SequenceHiLo)
                {
                    propertyBuilder.HasHiLoSequence(null, null, fromDataAnnotation);
                }

                return propertyBuilder;
            }

            return null;
        }

        public static bool CanSetValueGenerationStrategy(
            [NotNull] this IConventionPropertyBuilder propertyBuilder,
            ActianValueGenerationStrategy? valueGenerationStrategy,
            bool fromDataAnnotation = false)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));

            return (valueGenerationStrategy == null
                    || ActianPropertyExtensions.IsCompatibleWithValueGeneration(propertyBuilder.Metadata))
                && propertyBuilder.CanSetAnnotation(
                    ActianAnnotationNames.ValueGenerationStrategy, valueGenerationStrategy, fromDataAnnotation);
        }

        #endregion Value Generation Strategy

        #region Identity options

        public static PropertyBuilder HasIdentityOptions(
            [NotNull] this PropertyBuilder propertyBuilder,
            long? startValue = null,
            long? incrementBy = null,
            long? minValue = null,
            long? maxValue = null,
            bool? isCyclic = null,
            long? numbersToCache = null)
        {
            var property = propertyBuilder.Metadata;
            property.SetIdentityStartValue(startValue);
            property.SetIdentityIncrementBy(incrementBy);
            property.SetIdentityMinValue(minValue);
            property.SetIdentityMaxValue(maxValue);
            property.SetIdentityIsCyclic(isCyclic);
            property.SetIdentityNumbersToCache(numbersToCache);
            return propertyBuilder;
        }

        public static PropertyBuilder<TProperty> HasIdentityOptions<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder,
            long? startValue = null,
            long? incrementBy = null,
            long? minValue = null,
            long? maxValue = null,
            bool? isCyclic = null,
            long? numbersToCache = null)
            => (PropertyBuilder<TProperty>)HasIdentityOptions(
                (PropertyBuilder)propertyBuilder, startValue, incrementBy, minValue, maxValue, isCyclic, numbersToCache);

        public static IConventionPropertyBuilder HasIdentityOptions(
            [NotNull] this IConventionPropertyBuilder propertyBuilder,
            long? startValue = null,
            long? incrementBy = null,
            long? minValue = null,
            long? maxValue = null,
            bool? isCyclic = null,
            long? numbersToCache = null)
        {
            if (propertyBuilder.CanSetIdentityOptions(startValue, incrementBy, minValue, maxValue, isCyclic, numbersToCache))
            {
                var property = propertyBuilder.Metadata;
                property.SetIdentityStartValue(startValue);
                property.SetIdentityIncrementBy(incrementBy);
                property.SetIdentityMinValue(minValue);
                property.SetIdentityMaxValue(maxValue);
                property.SetIdentityIsCyclic(isCyclic);
                property.SetIdentityNumbersToCache(numbersToCache);
                return propertyBuilder;
            }

            return null;
        }

        public static bool CanSetIdentityOptions(
            [NotNull] this IConventionPropertyBuilder propertyBuilder,
            long? startValue = null,
            long? incrementBy = null,
            long? minValue = null,
            long? maxValue = null,
            bool? isCyclic = null,
            long? numbersToCache = null)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));

            var value = new IdentitySequenceOptionsData
            {
                StartValue = startValue,
                IncrementBy = incrementBy ?? 1,
                MinValue = minValue,
                MaxValue = maxValue,
                IsCyclic = isCyclic ?? false,
                NumbersToCache = numbersToCache ?? 1
            }.Serialize();

            return propertyBuilder.CanSetAnnotation(ActianAnnotationNames.IdentityOptions, value);
        }

        #endregion Identity options
    }
}
