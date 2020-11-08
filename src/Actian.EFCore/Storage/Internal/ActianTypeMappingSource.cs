using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Actian.EFCore.Internal;
using Actian.EFCore.Utilities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace Actian.EFCore.Storage.Internal
{
    public class ActianTypeMappingSource : RelationalTypeMappingSource
    {
        #region Mappings

        // Boolean
        private readonly ActianBooleanTypeMapping _boolean = new ActianBooleanTypeMapping("boolean", DbType.Boolean);

        // Uuid
        // TODO: Implement uuid
        //private readonly GuidTypeMapping _uuid = new GuidTypeMapping("uuid", DbType.Guid);

        // Numeric types
        private readonly ByteTypeMapping _tinyint = new ByteTypeMapping("tinyint", DbType.Byte);
        private readonly ShortTypeMapping _smallint = new ShortTypeMapping("smallint", DbType.Int16);
        private readonly IntTypeMapping _integer = new IntTypeMapping("integer", DbType.Int32);
        private readonly LongTypeMapping _bigint = new LongTypeMapping("bigint", DbType.Int64);

        private readonly FloatTypeMapping _float4 = new FloatTypeMapping("float4", DbType.Single);
        private readonly DoubleTypeMapping _float8 = new DoubleTypeMapping("float", DbType.Double);
        private readonly DecimalTypeMapping _decimal = new DecimalTypeMapping("decimal", DbType.Decimal);
        private readonly DecimalTypeMapping _money = new DecimalTypeMapping("money");

        // String types
        private readonly ActianStringTypeMapping _char = new ActianStringTypeMapping("char", unicode: false, fixedLength: true, unbounded: false);
        private readonly ActianStringTypeMapping _varchar = new ActianStringTypeMapping("varchar", unicode: false, fixedLength: false, unbounded: false);
        private readonly ActianStringTypeMapping _longVarchar = new ActianStringTypeMapping("long varchar", unicode: false, fixedLength: false, unbounded: true);
        private readonly ActianStringTypeMapping _nchar = new ActianStringTypeMapping("nchar", unicode: true, fixedLength: true, unbounded: false);
        private readonly ActianStringTypeMapping _nvarchar = new ActianStringTypeMapping("nvarchar", unicode: true, fixedLength: false, unbounded: false);
        private readonly ActianStringTypeMapping _longNVarchar = new ActianStringTypeMapping("long nvarchar", unicode: true, fixedLength: false, unbounded: true);

        // Binary types
        private readonly ActianByteArrayTypeMapping _byte = new ActianByteArrayTypeMapping("byte", fixedLength: true, unbounded: false);
        private readonly ActianByteArrayTypeMapping _varbyte = new ActianByteArrayTypeMapping("varbyte", fixedLength: false, unbounded: false);
        private readonly ActianByteArrayTypeMapping _longVarbyte = new ActianByteArrayTypeMapping("long varbyte", fixedLength: false, unbounded: true);

        // Date/Time types
        private readonly ActianIngresDateTypeMapping _ingresdate = new ActianIngresDateTypeMapping(typeof(DateTime));
        private readonly ActianAnsiDateTypeMapping _ansidate = new ActianAnsiDateTypeMapping(typeof(DateTime));
        private readonly ActianTimeTypeMapping _time = new ActianTimeTypeMapping("time", typeof(TimeSpan));
        private readonly ActianTimeTypeMapping _timeWithoutTimeZone = new ActianTimeTypeMapping("time without time zone", typeof(TimeSpan));
        private readonly ActianTimeTypeMapping _timeWithLocalTimeZone = new ActianTimeTypeMapping("time with local time zone", typeof(TimeSpan));
        private readonly ActianTimeTypeMapping _timeWithTimeZone = new ActianTimeTypeMapping("time with time zone", typeof(DateTimeOffset), withTimeZone: true);
        private readonly ActianTimestampTypeMapping _timestamp = new ActianTimestampTypeMapping("timestamp", typeof(DateTime));
        private readonly ActianTimestampTypeMapping _timestampWithoutTimeZone = new ActianTimestampTypeMapping("timestamp without time zone", typeof(DateTime));
        private readonly ActianTimestampTypeMapping _timestampWithLocalTimeZone = new ActianTimestampTypeMapping("timestamp with local time zone", typeof(DateTime));
        private readonly ActianTimestampTypeMapping _timestampWithTimeZone = new ActianTimestampTypeMapping("timestamp with time zone", typeof(DateTimeOffset), withTimeZone: true);

        #endregion Mappings

        public ConcurrentDictionary<Type, RelationalTypeMapping> ClrTypeMappings { get; }
        public ConcurrentDictionary<string, RelationalTypeMapping[]> StoreTypeMappings { get; }

        // These are disallowed only if specified without any kind of length specified in parenthesis.
        private readonly HashSet<string> _disallowedMappings = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "byte",
            "varbyte",
            "char",
            "varchar",
            "nchar",
            "nvarchar"
        };

        public ActianTypeMappingSource(
            [NotNull] TypeMappingSourceDependencies dependencies,
            [NotNull] RelationalTypeMappingSourceDependencies relationalDependencies)
            : base(dependencies, relationalDependencies)
        {
            ClrTypeMappings = new (Type clrType, RelationalTypeMapping mapping)[]
            {
                (typeof(bool), _boolean),
                //(typeof(Guid), _uuid),
                (typeof(byte), _tinyint),
                (typeof(short), _smallint),
                (typeof(int), _integer),
                (typeof(long), _bigint),
                (typeof(float), _float4),
                (typeof(double), _float8),
                (typeof(decimal), _decimal),
                (typeof(DateTime), _timestamp),
                (typeof(DateTimeOffset), _timestampWithTimeZone),
                (typeof(TimeSpan), _time)
            }.ToConcurrentDictionary();

            StoreTypeMappings = new (string dbType, RelationalTypeMapping mapping)[]
            {
                ("boolean", _boolean),
                //("uuid", _uuid),
                ("tinyint", _tinyint),
                ("smallint", _smallint),
                ("integer", _integer),
                ("bigint", _bigint),
                ("float4", _float4),
                ("float", _float8),
                ("decimal", _decimal),
                ("money", _money),
                ("char", _char),
                ("varchar", _varchar),
                ("long varchar", _longVarchar),
                ("nchar", _nchar),
                ("nvarchar", _nvarchar),
                ("long nvarchar", _longNVarchar),
                ("byte", _byte),
                ("varbyte", _varbyte),
                ("long varbyte", _longVarbyte),
                ("ingresdate", _ingresdate),
                ("ansidate", _ansidate),
                ("time", _time),
                ("time without time zone", _timeWithoutTimeZone),
                ("time with local time zone", _timeWithLocalTimeZone),
                ("time with time zone", _timeWithTimeZone),
                ("timestamp", _timestamp),
                ("timestamp without time zone", _timestampWithoutTimeZone),
                ("timestamp with local time zone", _timestampWithLocalTimeZone),
                ("timestamp with time zone", _timestampWithTimeZone),
            }.Select(item => (item.dbType, new[] { item.mapping })).ToConcurrentDictionary(StringComparer.OrdinalIgnoreCase);
        }

        /// <inheritdoc />
        protected override void ValidateMapping(CoreTypeMapping mapping, IProperty property)
        {
            if (mapping is RelationalTypeMapping relationalMapping && _disallowedMappings.Contains(relationalMapping.StoreType))
            {
                if (property == null)
                {
                    throw new ArgumentException(ActianStrings.UnqualifiedDataType(relationalMapping.StoreType));
                }

                throw new ArgumentException(ActianStrings.UnqualifiedDataTypeOnProperty(relationalMapping.StoreType, property.Name));
            }
        }

        /// <inheritdoc />
        protected override RelationalTypeMapping FindMapping(in RelationalTypeMappingInfo mappingInfo)
            => FindExistingMapping(mappingInfo)?.Clone(mappingInfo)
            ?? base.FindMapping(mappingInfo);

        protected virtual RelationalTypeMapping FindExistingMapping(in RelationalTypeMappingInfo mappingInfo)
        {
            var clrType = mappingInfo.ClrType;
            var storeTypeName = mappingInfo.StoreTypeName;
            var storeTypeNameBase = mappingInfo.StoreTypeNameBase;

            if (storeTypeName != null)
            {
                if (StoreTypeMappings.TryGetValue(storeTypeName, out var mappings))
                {
                    // We found the user-specified store type. No CLR type was provided - we're probably
                    // scaffolding from an existing database, take the first mapping as the default.
                    if (clrType == null)
                        return mappings.First();

                    // A CLR type was provided - look for a mapping between the store and CLR types. If not found, fail
                    // immediately.
                    return mappings.First(m => m.ClrType == clrType);
                }

                if (StoreTypeMappings.TryGetValue(storeTypeNameBase, out mappings))
                {
                    if (clrType == null)
                        return mappings.First();

                    // A CLR type was provided - look for a mapping between the store and CLR types. If not found, fail
                    // immediately.
                    return mappings.First(m => m.ClrType == clrType);
                }

                // A store type name was provided, but is unknown. This could be a domain (alias) type, in which case
                // we proceed with a CLR type lookup (if the type doesn't exist at all the failure will come later).
            }

            if (clrType == null || !ClrTypeMappings.TryGetValue(clrType, out var mapping))
                return null;

            // If needed, return the mapping with the configured length/precision/scale
            // TODO: Cache size/precision/scale mappings?
            if (mappingInfo.Size.HasValue)
            {
                if (clrType == typeof(string))
                {
                    mapping = (mappingInfo.IsFixedLength ?? false, mappingInfo.IsUnicode ?? false) switch
                    {
                        (true, true) => _nchar,
                        (true, false) => _nvarchar,
                        (false, true) => _char,
                        (false, false) => _varchar
                    };
                    return mapping.Clone($"{mapping.StoreType}({mappingInfo.Size})", mappingInfo.Size);
                }

                if (clrType == typeof(byte[]))
                {
                    mapping = mappingInfo.IsFixedLength switch
                    {
                        true => _byte,
                        false => _varbyte
                    };
                    return mapping.Clone($"{mapping.StoreType}({mappingInfo.Size})", mappingInfo.Size);
                }
            }
            else if (mappingInfo.Precision.HasValue)
            {
                if (clrType == typeof(decimal))
                {
                    return mappingInfo.Scale.HasValue
                        ? mapping.Clone($"{mapping.StoreType}({mappingInfo.Precision},{mappingInfo.Scale})", null)
                        : mapping.Clone($"{mapping.StoreType}({mappingInfo.Precision})", null);
                }

                if (clrType == typeof(DateTime) ||
                    clrType == typeof(DateTimeOffset) ||
                    clrType == typeof(TimeSpan))
                {
                    return mapping.Clone($"{mapping.StoreType}({mappingInfo.Precision.Value})", null);
                }
            }

            return mapping;
        }
    }
}
