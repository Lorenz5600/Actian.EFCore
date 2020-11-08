using System;
using System.Data;
using System.Data.Common;
using Ingres.Client;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;

namespace Actian.EFCore.Storage.Internal
{
    public class ActianTimestampTypeMapping : RelationalTypeMapping
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActianTimestampTypeMapping" /> class.
        /// </summary>
        /// <param name="storeType"> The name of the database type. </param>
        /// <param name="dbType"> The <see cref="DbType" /> to be used. </param>
        public ActianTimestampTypeMapping(
            [NotNull] string storeType,
            [NotNull] Type clrType,
            bool withTimeZone = false
            )
            : this(
                new RelationalTypeMappingParameters(
                    new CoreTypeMappingParameters(clrType),
                    storeType,
                    storeTypePostfix: StoreTypePostfix.Precision,
                    dbType: System.Data.DbType.Time
                ),
                withTimeZone
            )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActianTimestampTypeMapping" /> class.
        /// </summary>
        /// <param name="parameters"> Parameter object for <see cref="RelationalTypeMapping" />. </param>
        protected ActianTimestampTypeMapping(RelationalTypeMappingParameters parameters, bool withTimeZone)
            : base(parameters)
        {
            WithTimeZone = withTimeZone;
        }

        public bool WithTimeZone { get; }

        /// <inheritdoc />
        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
            => new ActianTimestampTypeMapping(parameters, WithTimeZone);

        /// <inheritdoc />
        protected override void ConfigureParameter(DbParameter parameter)
        {
            if (parameter is IngresParameter ingresParameter)
                ingresParameter.IngresType = IngresType.DateTime;
            else
                throw new InvalidOperationException($"Actian-specific type mapping {GetType().Name} being used with non-Actian parameter type {parameter.GetType().Name}");
        }

        /// <inheritdoc />
        protected override string GenerateNonNullSqlLiteral(object value) => value switch
        {
            DateTime dateTime when WithTimeZone => $"TIMESTAMP '{dateTime:yyyy-MM-dd HH:mm:ss.FFFFFFF}{GetTimeZone(dateTime)}'",
            DateTime dateTime => $"TIMESTAMP '{dateTime:yyyy-MM-dd HH:mm:ss.FFFFFFF}'",
            DateTimeOffset dateTimeOffset => $"TIMESTAMP '{dateTimeOffset:yyyy-MM-dd HH:mm:ss.FFFFFFFzzz}'",
            _ => throw new InvalidCastException($"Attempted to generate timestamp literal for type {value.GetType()}, only DateTime and DateTimeOffset are supported")
        };

        private string GetTimeZone(DateTime value)
            => value.Kind == DateTimeKind.Local ? $"{value:zzz}" : "+00:00";
    }
}
