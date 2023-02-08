using System;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class PropertyEntryActianTest : PropertyEntryTestBase<F1ActianFixture>, IDisposable
    {
        public PropertyEntryActianTest(F1ActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture.ListLoggerFactory, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void Dispose() => Helpers.LogSql();

        [ActianTodo]
        public override void Property_entry_original_value_is_set()
        {
            base.Property_entry_original_value_is_set();
            AssertSql(
                @"
                    SELECT FIRST 1 ""e"".""Id"", ""e"".""EngineSupplierId"", ""e"".""Name"", ""t"".""Id"", ""t"".""StorageLocation_Latitude"", ""t"".""StorageLocation_Longitude""
                    FROM ""Engines"" AS ""e""
                    LEFT JOIN (
                        SELECT ""e0"".""Id"", ""e0"".""StorageLocation_Latitude"", ""e0"".""StorageLocation_Longitude""
                        FROM ""Engines"" AS ""e0""
                        WHERE ""e0"".""StorageLocation_Longitude"" IS NOT NULL AND ""e0"".""StorageLocation_Latitude"" IS NOT NULL
                    ) AS ""t"" ON ""e"".""Id"" = ""t"".""Id""
                    ORDER BY ""e"".""Id""
                ",
                @"
                    @p1='1'
                    @p2='1'
                    @p0='FO 108X' (Size = 4000)
                    @p3='ChangedEngine' (Size = 4000)
                    @p4='47.64491'
                    @p5='-122.128101'
                    
                    SET NOCOUNT ON;
                    UPDATE ""Engines"" SET ""Name"" = @p0
                    WHERE ""Id"" = @p1 AND ""EngineSupplierId"" = @p2 AND ""Name"" = @p3 AND ""StorageLocation_Latitude"" = @p4 AND ""StorageLocation_Longitude"" = @p5;
                    SELECT @@ROWCOUNT;
                "
            );
        }
    }
}
