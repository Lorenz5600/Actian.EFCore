using System;
using System.Data;
using System.Data.Common;
using Ingres.Client;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;

namespace Actian.EFCore.Storage.Internal
{
    public class ActianTimeTypeMapping : RelationalTypeMapping
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActianTimeTypeMapping" /> class.
        /// </summary>
        /// <param name="storeType"> The name of the database type. </param>
        /// <param name="clrType">The CLR type</param>
        /// <param name="withTimeZone"></param>
        public ActianTimeTypeMapping(
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
        /// Initializes a new instance of the <see cref="ActianTimeTypeMapping" /> class.
        /// </summary>
        /// <param name="parameters"> Parameter object for <see cref="RelationalTypeMapping" />. </param>
        /// <param name="withTimeZone"></param>
        protected ActianTimeTypeMapping(RelationalTypeMappingParameters parameters, bool withTimeZone)
            : base(parameters)
        {
            WithTimeZone = withTimeZone;
        }

        public bool WithTimeZone { get; }

        /// <inheritdoc />
        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
            => new ActianTimeTypeMapping(parameters, WithTimeZone);

        /// <inheritdoc />
        protected override void ConfigureParameter(DbParameter parameter)
        {
            if (parameter is IngresParameter ingresParameter)
                ingresParameter.IngresType = IngresType.Time;
            else
                throw new InvalidOperationException($"Actian-specific type mapping {GetType().Name} being used with non-Actian parameter type {parameter.GetType().Name}");
        }

        /// <inheritdoc />
        protected override string GenerateNonNullSqlLiteral(object value) => value switch
        {
            TimeSpan timeSpan when WithTimeZone => $@"TIME '{timeSpan:hh\:mm\:ss\.FFFFFFF}+00:00'",
            TimeSpan timeSpan => $@"TIME '{timeSpan:hh\:mm\:ss\.FFFFFFF}'",
            DateTime dateTime when WithTimeZone => $@"TIME '{dateTime:HH:mm:ss.FFFFFFF}{GetTimeZone(dateTime)}'",
            DateTime dateTime => $@"TIME '{dateTime:HH:mm:ss.FFFFFFF}'",
            DateTimeOffset dateTimeOffset => $@"TIME '{dateTimeOffset:HH:mm:ss.FFFFFFFzzz}'",
            _ => throw new InvalidCastException($"Attempted to generate time literal for type {value.GetType()}, only TimeSpan, DateTime and DateTimeOffset are supported")
        };

        private string GetTimeZone(DateTime value)
            => value.Kind == DateTimeKind.Local ? $"{value:zzz}" : "+00:00";
    }
}
