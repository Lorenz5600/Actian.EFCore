using System;
using Actian.EFCore.Storage.Internal;
using Actian.EFCore.Utilities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Actian.EFCore.ValueGeneration.Internal
{
    public class ActianValueGeneratorSelector : RelationalValueGeneratorSelector
    {
        private readonly IActianSequenceValueGeneratorFactory _sequenceFactory;
        private readonly IActianConnection _connection;
        private readonly IRawSqlCommandBuilder _rawSqlCommandBuilder;
        private readonly IDiagnosticsLogger<DbLoggerCategory.Database.Command> _commandLogger;

        public ActianValueGeneratorSelector(
            [NotNull] ValueGeneratorSelectorDependencies dependencies,
            [NotNull] IActianSequenceValueGeneratorFactory sequenceFactory,
            [NotNull] IActianConnection connection,
            [NotNull] IRawSqlCommandBuilder rawSqlCommandBuilder,
            [NotNull] IDiagnosticsLogger<DbLoggerCategory.Database.Command> commandLogger)
            : base(dependencies)
        {
            _sequenceFactory = sequenceFactory;
            _connection = connection;
            _rawSqlCommandBuilder = rawSqlCommandBuilder;
            _commandLogger = commandLogger;
        }

        /// <inheritdoc />
        public new virtual IActianValueGeneratorCache Cache => (IActianValueGeneratorCache)base.Cache;

        /// <inheritdoc />
        public override ValueGenerator Select(IProperty property, IEntityType entityType)
        {
            Check.NotNull(property, nameof(property));
            Check.NotNull(entityType, nameof(entityType));

            return property.GetValueGeneratorFactory() == null
                && property.GetValueGenerationStrategy() == ActianValueGenerationStrategy.SequenceHiLo
                    ? _sequenceFactory.Create(
                        property,
                        Cache.GetOrAddSequenceState(property, _connection),
                        _connection,
                        _rawSqlCommandBuilder,
                        _commandLogger)
                    : base.Select(property, entityType);
        }

        /// <inheritdoc />
        public override ValueGenerator Create(IProperty property, IEntityType entityType)
        {
            Check.NotNull(property, nameof(property));
            Check.NotNull(entityType, nameof(entityType));

            return property.ClrType.UnwrapNullableType() == typeof(Guid)
                ? property.ValueGenerated == ValueGenerated.Never || property.GetDefaultValueSql() != null
                    ? (ValueGenerator)new TemporaryGuidValueGenerator()
                    : new SequentialGuidValueGenerator()
                : base.Create(property, entityType);
        }
    }
}
