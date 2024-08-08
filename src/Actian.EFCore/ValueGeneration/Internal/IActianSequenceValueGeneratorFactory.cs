using System;
using Actian.EFCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ValueGeneration;

#nullable enable

namespace Actian.EFCore.ValueGeneration.Internal
{
    public interface IActianSequenceValueGeneratorFactory
    {
        ValueGenerator? TryCreate(
            IProperty property,
            Type clrType,
            ActianSequenceValueGeneratorState generatorState,
            IActianConnection connection,
            IRawSqlCommandBuilder rawSqlCommandBuilder,
            IRelationalCommandDiagnosticsLogger commandLogger);
    }
}
