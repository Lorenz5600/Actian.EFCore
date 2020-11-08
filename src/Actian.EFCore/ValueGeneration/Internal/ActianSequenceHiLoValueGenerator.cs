using System.Threading;
using System.Threading.Tasks;
using Actian.EFCore.Storage.Internal;
using Actian.EFCore.Update.Internal;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Actian.EFCore.ValueGeneration.Internal
{
    public class ActianSequenceHiLoValueGenerator<TValue> : HiLoValueGenerator<TValue>
    {
        private readonly IRawSqlCommandBuilder _rawSqlCommandBuilder;
        private readonly IActianUpdateSqlGenerator _sqlGenerator;
        private readonly IActianConnection _connection;
        private readonly ISequence _sequence;
        private readonly IDiagnosticsLogger<DbLoggerCategory.Database.Command> _commandLogger;

        public ActianSequenceHiLoValueGenerator(
            [NotNull] IRawSqlCommandBuilder rawSqlCommandBuilder,
            [NotNull] IActianUpdateSqlGenerator sqlGenerator,
            [NotNull] ActianSequenceValueGeneratorState generatorState,
            [NotNull] IActianConnection connection,
            [NotNull] IDiagnosticsLogger<DbLoggerCategory.Database.Command> commandLogger)
            : base(generatorState)
        {
            _sequence = generatorState.Sequence;
            _rawSqlCommandBuilder = rawSqlCommandBuilder;
            _sqlGenerator = sqlGenerator;
            _connection = connection;
            _commandLogger = commandLogger;
        }

        private RelationalCommandParameterObject RelationalCommandParameterObject => new RelationalCommandParameterObject(
            _connection,
            parameterValues: null,
            readerColumns: null,
            context: null,
            _commandLogger
        );

        /// <inheritdoc />
        protected override long GetNewLowValue() => _rawSqlCommandBuilder
            .Build(_sqlGenerator.GenerateNextSequenceValueOperation(_sequence.Name, _sequence.Schema))
            .ExecuteScalar<long>(RelationalCommandParameterObject);

        /// <inheritdoc />
        protected override async Task<long> GetNewLowValueAsync(CancellationToken cancellationToken = default) => await _rawSqlCommandBuilder
            .Build(_sqlGenerator.GenerateNextSequenceValueOperation(_sequence.Name, _sequence.Schema))
            .ExecuteScalarAsync<long>(RelationalCommandParameterObject, cancellationToken);

        /// <inheritdoc />
        public override bool GeneratesTemporaryValues => false;
    }
}
