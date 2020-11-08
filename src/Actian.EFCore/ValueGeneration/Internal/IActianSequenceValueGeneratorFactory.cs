using Actian.EFCore.Storage.Internal;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Actian.EFCore.ValueGeneration.Internal
{
    public interface IActianSequenceValueGeneratorFactory
    {
        ValueGenerator Create(
            [NotNull] IProperty property,
            [NotNull] ActianSequenceValueGeneratorState generatorState,
            [NotNull] IActianConnection connection,
            [NotNull] IRawSqlCommandBuilder rawSqlCommandBuilder,
            [NotNull] IDiagnosticsLogger<DbLoggerCategory.Database.Command> commandLogger);
    }
}
