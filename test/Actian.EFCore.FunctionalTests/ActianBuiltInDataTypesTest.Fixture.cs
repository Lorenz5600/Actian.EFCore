using System;
using System.ComponentModel.DataAnnotations.Schema;
using Actian.EFCore.Infrastructure;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Actian.EFCore
{
    partial class ActianBuiltInDataTypesTest
    {
        public class ActianBuiltInDataTypesFixture : BuiltInDataTypesFixtureBase
        {
            public override bool StrictEquality => true;

            public override bool SupportsAnsi => true;

            public override bool SupportsUnicodeToAnsiConversion => true;

            public override bool SupportsLargeStringComparisons => true;

            public override bool SupportsDecimalComparisons => true;

            protected override ITestStoreFactory TestStoreFactory
                => ActianTestStoreFactory.Instance;

            protected override bool ShouldLogCategory(string logCategory)
                => logCategory == DbLoggerCategory.Query.Name
                || logCategory == DbLoggerCategory.Database.Command.Name
                || true;

            public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ListLoggerFactory;

            protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
            {
                base.OnModelCreating(modelBuilder, context);

                modelBuilder.Entity<StringKeyDataType>(b =>
                {
                    b.HasKey(e => e.Id);
                    b.Property(e => e.Id).HasMaxLength(100);
                });

                modelBuilder.Entity<MappedDataTypes>(b =>
                {
                    b.HasKey(e => e.Int);
                    b.Property(e => e.Int).ValueGeneratedNever();
                });

                modelBuilder.Entity<MappedNullableDataTypes>(b =>
                {
                    b.HasKey(e => e.Int);
                    b.Property(e => e.Int).ValueGeneratedNever();
                });

                modelBuilder.Entity<MappedDataTypesWithIdentity>();
                modelBuilder.Entity<MappedNullableDataTypesWithIdentity>();

                modelBuilder.Entity<MappedSizedDataTypes>()
                    .Property(e => e.Id)
                    .ValueGeneratedNever();

                modelBuilder.Entity<MappedScaledDataTypes>()
                    .Property(e => e.Id)
                    .ValueGeneratedNever();

                modelBuilder.Entity<MappedPrecisionAndScaledDataTypes>()
                    .Property(e => e.Id)
                    .ValueGeneratedNever();

                MakeRequired<MappedDataTypes>(modelBuilder);
                MakeRequired<MappedDataTypesWithIdentity>(modelBuilder);

                modelBuilder.Entity<MappedSizedDataTypes>();
                modelBuilder.Entity<MappedScaledDataTypes>();
                modelBuilder.Entity<MappedPrecisionAndScaledDataTypes>();
                modelBuilder.Entity<MappedSizedDataTypesWithIdentity>();
                modelBuilder.Entity<MappedScaledDataTypesWithIdentity>();
                modelBuilder.Entity<MappedPrecisionAndScaledDataTypesWithIdentity>();
                modelBuilder.Entity<MappedSizedDataTypesWithIdentity>();
                modelBuilder.Entity<MappedScaledDataTypesWithIdentity>();
                modelBuilder.Entity<MappedPrecisionAndScaledDataTypesWithIdentity>();
            }

            public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
            {
                var options = base.AddOptions(builder)
                    .ConfigureWarnings(c => c.Log(ActianEventId.DecimalTypeDefaultWarning));

                new ActianDbContextOptionsBuilder(options).MinBatchSize(1);

                return options;
            }

            public override bool SupportsBinaryKeys => true;

            public override DateTime DefaultDateTime => new DateTime();
        }

        [Flags]
        protected enum StringEnum16 : short
        {
            Value1 = 1,
            Value2 = 2,
            Value4 = 4
        }

        [Flags]
        protected enum StringEnumU16 : ushort
        {
            Value1 = 1,
            Value2 = 2,
            Value4 = 4
        }

        protected class MappedDataTypes
        {
            [Column(TypeName = "int")]
            public int Int { get; set; }

            [Column(TypeName = "bigint")]
            public long LongAsBigInt { get; set; }

            [Column(TypeName = "smallint")]
            public short ShortAsSmallint { get; set; }

            [Column(TypeName = "tinyint")]
            public byte ByteAsTinyint { get; set; }

            [Column(TypeName = "int")]
            public uint UintAsInt { get; set; }

            [Column(TypeName = "bigint")]
            public ulong UlongAsBigint { get; set; }

            [Column(TypeName = "smallint")]
            public ushort UShortAsSmallint { get; set; }

            [Column(TypeName = "tinyint")]
            public sbyte SByteAsTinyint { get; set; }

            [Column(TypeName = "boolean")]
            public bool BoolAsBoolean { get; set; }

            [Column(TypeName = "money")]
            public decimal DecimalAsMoney { get; set; }

            [Column(TypeName = "float")]
            public double DoubleAsFloat { get; set; }

            [Column(TypeName = "float4")]
            public float FloatAsFloat4 { get; set; }

            [Column(TypeName = "ansidate")]
            public DateTime DateAsAnsiDate { get; set; }

            [Column(TypeName = "ingresdate")]
            public DateTime DateTimeAsIngresDate { get; set; }

            [Column(TypeName = "timestamp with time zone")]
            public DateTimeOffset DateTimeOffsetAsTimestampWithTimeZone { get; set; }

            [Column(TypeName = "timestamp")]
            public DateTime DateTimeAsTimestamp { get; set; }

            [Column(TypeName = "time")]
            public TimeSpan TimeSpanAsTime { get; set; }

            [Column(TypeName = "long varchar")]
            public string StringAsLongVarchar { get; set; }

            [Column(TypeName = "long nvarchar")]
            public string StringAsLongNVarchar { get; set; }

            [Column(TypeName = "long byte")]
            public byte[] BytesAsLongByte { get; set; }

            [Column(TypeName = "decimal")]
            public decimal Decimal { get; set; }

            [Column(TypeName = "numeric")]
            public decimal DecimalAsNumeric { get; set; }

            // TODO: Implement uuid
            //[Column(TypeName = "uniqueidentifier")]
            //public Guid GuidAsUniqueidentifier { get; set; }

            [Column(TypeName = "bigint")]
            public uint UintAsBigint { get; set; }

            [Column(TypeName = "decimal(20,0)")]
            public ulong UlongAsDecimal200 { get; set; }

            [Column(TypeName = "int")]
            public ushort UShortAsInt { get; set; }

            [Column(TypeName = "smallint")]
            public sbyte SByteAsSmallint { get; set; }

            [Column(TypeName = "long varchar")]
            public char CharAsLongVarchar { get; set; }

            [Column(TypeName = "long nvarchar")]
            public char CharAsLongNVarchar { get; set; }

            [Column(TypeName = "int")]
            public char CharAsInt { get; set; }

            [Column(TypeName = "long varchar")]
            public StringEnum16 EnumAsLongVarchar { get; set; }

            [Column(TypeName = "nvarchar(20)")]
            public StringEnumU16 EnumAsNVarchar20 { get; set; }
        }

        protected class MappedSizedDataTypes
        {
            public int Id { get; set; }

            [Column(TypeName = "char(3)")]
            public string StringAsChar3 { get; set; }

            [Column(TypeName = "varchar(3)")]
            public string StringAsVarchar3 { get; set; }

            [Column(TypeName = "nchar(3)")]
            public string StringAsNchar3 { get; set; }

            [Column(TypeName = "nvarchar(3)")]
            public string StringAsNVarchar3 { get; set; }

            [Column(TypeName = "byte(3)")]
            public byte[] BytesAsByte3 { get; set; }

            [Column(TypeName = "varbyte(3)")]
            public byte[] BytesAsVarByte3 { get; set; }

            [Column(TypeName = "varchar(3)")]
            public char? CharAsVarchar3 { get; set; }

            [Column(TypeName = "nvarchar(3)")]
            public char? CharAsNVarchar3 { get; set; }
        }

        protected class MappedScaledDataTypes
        {
            public int Id { get; set; }

            [Column(TypeName = "float(3)")]
            public float FloatAsFloat3 { get; set; }

            [Column(TypeName = "float(25)")]
            public float FloatAsFloat25 { get; set; }

            //[Column(TypeName = "timestamp(3) with time zone")]
            [Column(TypeName = "timestamp with time zone")]
            public DateTimeOffset DateTimeOffsetAsTimestampWithTimeZone3 { get; set; }

            [Column(TypeName = "timestamp(3)")]
            public DateTime DateTimeAsTimestamp3 { get; set; }

            [Column(TypeName = "decimal(3)")]
            public decimal DecimalAsDecimal3 { get; set; }

            [Column(TypeName = "numeric(3)")]
            public decimal DecimalAsNumeric3 { get; set; }
        }

        protected class MappedPrecisionAndScaledDataTypes
        {
            public int Id { get; set; }

            [Column(TypeName = "decimal(5,2)")]
            public decimal DecimalAsDecimal52 { get; set; }

            [Column(TypeName = "dec(5,2)")]
            public decimal DecimalAsDec52 { get; set; }

            [Column(TypeName = "numeric(5,2)")]
            public decimal DecimalAsNumeric52 { get; set; }
        }

        protected class MappedNullableDataTypes
        {
            [Column(TypeName = "int")]
            public int? Int { get; set; }

            [Column(TypeName = "bigint")]
            public long? LongAsBigint { get; set; }

            [Column(TypeName = "smallint")]
            public short? ShortAsSmallint { get; set; }

            [Column(TypeName = "tinyint")]
            public byte? ByteAsTinyint { get; set; }

            [Column(TypeName = "int")]
            public uint? UintAsInt { get; set; }

            [Column(TypeName = "bigint")]
            public ulong? UlongAsBigint { get; set; }

            [Column(TypeName = "smallint")]
            public ushort? UShortAsSmallint { get; set; }

            [Column(TypeName = "tinyint")]
            public sbyte? SbyteAsTinyint { get; set; }

            [Column(TypeName = "boolean")]
            public bool? BoolAsBoolean { get; set; }

            [Column(TypeName = "money")]
            public decimal? DecimalAsMoney { get; set; }

            [Column(TypeName = "float")]
            public double? DoubleAsFloat { get; set; }

            [Column(TypeName = "float4")]
            public float? FloatAsFloat4 { get; set; }

            [Column(TypeName = "ansidate")]
            public DateTime? DateTimeAsAnsiDate { get; set; }

            [Column(TypeName = "ingresdate")]
            public DateTime? DateTimeAsIngresDate { get; set; }

            [Column(TypeName = "timestamp with time zone")]
            public DateTimeOffset? DateTimeOffsetAsTimestampWithTimeZone { get; set; }

            [Column(TypeName = "timestamp")]
            public DateTime? DateTimeAsTimestamp { get; set; }

            [Column(TypeName = "time")]
            public TimeSpan? TimeSpanAsTime { get; set; }

            [Column(TypeName = "long varchar")]
            public string StringAsLongVarchar { get; set; }

            [Column(TypeName = "long nvarchar")]
            public string StringAsLongNVarchar { get; set; }

            [Column(TypeName = "long nvarchar")]
            public byte[] BytesAsLongByte { get; set; }

            [Column(TypeName = "decimal")]
            public decimal? Decimal { get; set; }

            [Column(TypeName = "numeric")]
            public decimal? DecimalAsNumeric { get; set; }

            // TODO: Implement uuid
            //[Column(TypeName = "uniqueidentifier")]
            //public Guid? GuidAsUniqueidentifier { get; set; }

            [Column(TypeName = "bigint")]
            public uint? UintAsBigint { get; set; }

            [Column(TypeName = "decimal(20,0)")]
            public ulong? UlongAsDecimal200 { get; set; }

            [Column(TypeName = "int")]
            public ushort? UShortAsInt { get; set; }

            [Column(TypeName = "smallint")]
            public sbyte? SByteAsSmallint { get; set; }

            [Column(TypeName = "long varchar")]
            public char? CharAsLongVarchar { get; set; }

            [Column(TypeName = "long nvarchar")]
            public char? CharAsLongNVarchar { get; set; }

            [Column(TypeName = "int")]
            public char? CharAsInt { get; set; }

            [Column(TypeName = "long varchar")]
            public StringEnum16? EnumAsLongVarchar { get; set; }

            [Column(TypeName = "nvarchar(20)")]
            public StringEnumU16? EnumAsNVarchar20 { get; set; }
        }

        protected class MappedDataTypesWithIdentity
        {
            public int Id { get; set; }

            [Column(TypeName = "int")]
            public int Int { get; set; }

            [Column(TypeName = "bigint")]
            public long LongAsBigint { get; set; }

            [Column(TypeName = "smallint")]
            public short ShortAsSmallint { get; set; }

            [Column(TypeName = "tinyint")]
            public byte ByteAsTinyint { get; set; }

            [Column(TypeName = "int")]
            public uint UintAsInt { get; set; }

            [Column(TypeName = "bigint")]
            public ulong UlongAsBigint { get; set; }

            [Column(TypeName = "smallint")]
            public ushort UShortAsSmallint { get; set; }

            [Column(TypeName = "tinyint")]
            public sbyte SbyteAsTinyint { get; set; }

            [Column(TypeName = "boolean")]
            public bool BoolAsBoolean { get; set; }

            [Column(TypeName = "money")]
            public decimal DecimalAsMoney { get; set; }

            [Column(TypeName = "float")]
            public double DoubleAsFloat { get; set; }

            [Column(TypeName = "float4")]
            public float FloatAsFloat4 { get; set; }

            [Column(TypeName = "double precision")]
            public double DoubleAsDoublePrecision { get; set; }

            [Column(TypeName = "ansidate")]
            public DateTime DateTimeAsAnsiDate { get; set; }

            [Column(TypeName = "ingresdate")]
            public DateTime DateTimeAsIngresDate { get; set; }

            [Column(TypeName = "timestamp with time zone")]
            public DateTimeOffset DateTimeOffsetAsTimestampWithTimeZone { get; set; }

            [Column(TypeName = "timestamp")]
            public DateTime DateTimeAsTimestamp { get; set; }

            [Column(TypeName = "time")]
            public TimeSpan TimeSpanAsTime { get; set; }

            [Column(TypeName = "long varchar")]
            public string StringAsLongVarchar { get; set; }

            [Column(TypeName = "long nvarchar")]
            public string StringAsLongNVarchar { get; set; }

            [Column(TypeName = "long byte")]
            public byte[] BytesAsLongByte { get; set; }

            [Column(TypeName = "decimal")]
            public decimal Decimal { get; set; }

            [Column(TypeName = "numeric")]
            public decimal DecimalAsNumeric { get; set; }

            // TODO: Implement uuid
            //[Column(TypeName = "uniqueidentifier")]
            //public Guid GuidAsUniqueidentifier { get; set; }

            [Column(TypeName = "bigint")]
            public uint UintAsBigint { get; set; }

            [Column(TypeName = "decimal(20,0)")]
            public ulong UlongAsDecimal200 { get; set; }

            [Column(TypeName = "int")]
            public ushort UShortAsInt { get; set; }

            [Column(TypeName = "smallint")]
            public sbyte SByteAsSmallint { get; set; }

            [Column(TypeName = "long varchar")]
            public char CharAsLongVarchar { get; set; }

            [Column(TypeName = "long nvarchar")]
            public char CharAsLongNVarchar { get; set; }

            [Column(TypeName = "int")]
            public char CharAsInt { get; set; }

            [Column(TypeName = "long varchar")]
            public StringEnum16 EnumAsLongVarchar { get; set; }

            [Column(TypeName = "nvarchar(20)")]
            public StringEnumU16 EnumAsNVarchar20 { get; set; }
        }

        protected class MappedSizedDataTypesWithIdentity
        {
            public int Id { get; set; }
            public int Int { get; set; }

            [Column(TypeName = "char(3)")]
            public string StringAsChar3 { get; set; }

            [Column(TypeName = "varchar(3)")]
            public string StringAsVarchar3 { get; set; }

            [Column(TypeName = "nchar(3)")]
            public string StringAsNchar3 { get; set; }

            [Column(TypeName = "nvarchar(3)")]
            public string StringAsNVarchar3 { get; set; }

            [Column(TypeName = "byte(3)")]
            public byte[] BytesAsByte3 { get; set; }

            [Column(TypeName = "varbyte(3)")]
            public byte[] BytesAsVarbyte3 { get; set; }

            [Column(TypeName = "varchar(3)")]
            public char? CharAsVarchar3 { get; set; }

            [Column(TypeName = "nvarchar(3)")]
            public char? CharAsNVarchar3 { get; set; }
        }

        protected class MappedScaledDataTypesWithIdentity
        {
            public int Id { get; set; }
            public int Int { get; set; }

            [Column(TypeName = "float(3)")]
            public float FloatAsFloat3 { get; set; }

            [Column(TypeName = "float(25)")]
            public float FloatAsFloat25 { get; set; }

            //[Column(TypeName = "timestamp(3) with time zone")]
            [Column(TypeName = "timestamp with time zone")]
            public DateTimeOffset DateTimeOffsetAsDatetimeoffset3 { get; set; }

            [Column(TypeName = "timestamp(3)")]
            public DateTime DateTimeAsDatetime23 { get; set; }

            [Column(TypeName = "decimal(3)")]
            public decimal DecimalAsDecimal3 { get; set; }

            [Column(TypeName = "dec(3)")]
            public decimal DecimalAsDec3 { get; set; }

            [Column(TypeName = "numeric(3)")]
            public decimal DecimalAsNumeric3 { get; set; }
        }

        protected class MappedPrecisionAndScaledDataTypesWithIdentity
        {
            public int Id { get; set; }
            public int Int { get; set; }

            [Column(TypeName = "decimal(5,2)")]
            public decimal DecimalAsDecimal52 { get; set; }

            [Column(TypeName = "dec(5,2)")]
            public decimal DecimalAsDec52 { get; set; }

            [Column(TypeName = "numeric(5,2)")]
            public decimal DecimalAsNumeric52 { get; set; }
        }

        protected class MappedNullableDataTypesWithIdentity
        {
            public int Id { get; set; }

            [Column(TypeName = "int")]
            public int? Int { get; set; }

            [Column(TypeName = "bigint")]
            public long? LongAsBigint { get; set; }

            [Column(TypeName = "smallint")]
            public short? ShortAsSmallint { get; set; }

            [Column(TypeName = "tinyint")]
            public byte? ByteAsTinyint { get; set; }

            [Column(TypeName = "int")]
            public uint? UintAsInt { get; set; }

            [Column(TypeName = "bigint")]
            public ulong? UlongAsBigint { get; set; }

            [Column(TypeName = "smallint")]
            public ushort? UshortAsSmallint { get; set; }

            [Column(TypeName = "tinyint")]
            public sbyte? SbyteAsTinyint { get; set; }

            [Column(TypeName = "boolean")]
            public bool? BoolAsBoolean { get; set; }

            [Column(TypeName = "money")]
            public decimal? DecimalAsMoney { get; set; }

            [Column(TypeName = "float")]
            public double? DoubleAsFloat { get; set; }

            [Column(TypeName = "float4")]
            public float? FloatAsFloat4 { get; set; }

            [Column(TypeName = "ansidate")]
            public DateTime? DateTimeAsAnsiDate { get; set; }

            [Column(TypeName = "ingresdate")]
            public DateTime? DateTimeAsIngresDate { get; set; }

            [Column(TypeName = "timestamp with time zone")]
            public DateTimeOffset? DateTimeOffsetAsTimestampWithTimeZone { get; set; }

            [Column(TypeName = "timestamp")]
            public DateTime? DateTimeAsTimestamp { get; set; }

            [Column(TypeName = "time")]
            public TimeSpan? TimeSpanAsTime { get; set; }

            [Column(TypeName = "long varchar")]
            public string StringAsLongVarchar { get; set; }

            [Column(TypeName = "long nvarchar")]
            public string StringAsLongNVarchar { get; set; }

            [Column(TypeName = "long byte")]
            public byte[] BytesAsLongByte { get; set; }

            [Column(TypeName = "decimal")]
            public decimal? Decimal { get; set; }

            [Column(TypeName = "numeric")]
            public decimal? DecimalAsNumeric { get; set; }

            // TODO: Implement uuid
            //[Column(TypeName = "uniqueidentifier")]
            //public Guid? GuidAsUniqueidentifier { get; set; }

            [Column(TypeName = "bigint")]
            public uint? UintAsBigint { get; set; }

            [Column(TypeName = "decimal(20,0)")]
            public ulong? UlongAsDecimal200 { get; set; }

            [Column(TypeName = "int")]
            public ushort? UShortAsInt { get; set; }

            [Column(TypeName = "smallint")]
            public sbyte? SByteAsSmallint { get; set; }

            [Column(TypeName = "long varchar")]
            public char? CharAsLongVarchar { get; set; }

            [Column(TypeName = "long nvarchar")]
            public char? CharAsLongNVarchar { get; set; }

            [Column(TypeName = "int")]
            public char? CharAsInt { get; set; }

            [Column(TypeName = "long varchar")]
            public StringEnum16? EnumAsLongVarchar { get; set; }

            [Column(TypeName = "nvarchar(20)")]
            public StringEnumU16? EnumAsNVarchar20 { get; set; }
        }

        public class ColumnInfo
        {
            public string TableName { get; set; }
            public string ColumnName { get; set; }
            public string DataType { get; set; }
            public bool? IsNullable { get; set; }
            public int? MaxLength { get; set; }
            public int? NumericPrecision { get; set; }
            public int? NumericScale { get; set; }
            public int? DateTimePrecision { get; set; }
        }
    }
}
