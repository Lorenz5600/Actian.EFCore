using System;
using Actian.EFCore.Storage.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Actian.EFCore.ValueGeneration.Internal
{
#nullable enable
    public class ActianValueGeneratorSelector : RelationalValueGeneratorSelector
    {
        private readonly IActianSequenceValueGeneratorFactory _sequenceFactory;
        private readonly IActianConnection _connection;
        private readonly IRawSqlCommandBuilder _rawSqlCommandBuilder;
        private readonly IRelationalCommandDiagnosticsLogger _commandLogger;

        public ActianValueGeneratorSelector(
            ValueGeneratorSelectorDependencies dependencies,
            IActianSequenceValueGeneratorFactory sequenceFactory,
            IActianConnection connection,
            IRawSqlCommandBuilder rawSqlCommandBuilder,
            IRelationalCommandDiagnosticsLogger commandLogger)
            : base(dependencies)
        {
            _sequenceFactory = sequenceFactory;
            _connection = connection;
            _rawSqlCommandBuilder = rawSqlCommandBuilder;
            _commandLogger = commandLogger;
        }

        public new virtual IActianValueGeneratorCache Cache => (IActianValueGeneratorCache)base.Cache;

        public override ValueGenerator Select(IProperty property, ITypeBase typeBase)
        {
            if (property.GetValueGeneratorFactory() != null
                || property.GetValueGenerationStrategy() != ActianValueGenerationStrategy.SequenceHiLo)
            {
                return base.Select(property, typeBase);
            }

            var propertyType = property.ClrType.UnwrapNullableType().UnwrapEnumType();

            var generator = _sequenceFactory.TryCreate(
                property,
                propertyType,
                Cache.GetOrAddSequenceState(property, _connection),
                _connection,
                _rawSqlCommandBuilder,
                _commandLogger);

            if (generator != null)
            {
                return generator;
            }

            var converter = property.GetTypeMapping().Converter;
            if (converter != null
                && converter.ProviderClrType != propertyType)
            {
                generator = _sequenceFactory.TryCreate(
                    property,
                    converter.ProviderClrType,
                    Cache.GetOrAddSequenceState(property, _connection),
                    _connection,
                    _rawSqlCommandBuilder,
                    _commandLogger);

                if (generator != null)
                {
                    return generator.WithConverter(converter);
                }
            }

            throw new ArgumentException(
                CoreStrings.InvalidValueGeneratorFactoryProperty(
                    nameof(ActianSequenceValueGeneratorFactory), property.Name, property.DeclaringType.DisplayName()));
        }

        protected override ValueGenerator? FindForType(IProperty property, ITypeBase typeBase, Type clrType)
            => property.ClrType.UnwrapNullableType() == typeof(Guid)
                ? property.ValueGenerated == ValueGenerated.Never || property.GetDefaultValueSql() != null
                    ? new TemporaryGuidValueGenerator()
                    : new SequentialGuidValueGenerator()
                : base.FindForType(property, typeBase, clrType);
    }
}
