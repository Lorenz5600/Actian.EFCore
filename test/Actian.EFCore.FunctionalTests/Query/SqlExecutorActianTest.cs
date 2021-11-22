using System.Data.Common;
using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Ingres.Client;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Actian.EFCore.Query
{
    public class SqlExecutorActianTest : SqlExecutorTestBase<NorthwindQueryActianFixture<NoopModelCustomizer>>
    {
        public SqlExecutorActianTest(NorthwindQueryActianFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        [ActianTodo]
        public override void Executes_stored_procedure()
        {
            base.Executes_stored_procedure();
            AssertSql(@"dbo"".""Ten Most Expensive Products""");
        }

        [ActianTodo]
        public override void Executes_stored_procedure_with_parameter()
        {
            base.Executes_stored_procedure_with_parameter();
            AssertSql(@"
                @CustomerID='ALFKI' (Nullable = false) (Size = 5)
                
                ""dbo"".""CustOrderHist"" @CustomerID
            ");
        }

        [ActianTodo]
        public override void Executes_stored_procedure_with_generated_parameter()
        {
            base.Executes_stored_procedure_with_generated_parameter();
            AssertSql(@"
                @p0='ALFKI' (Size = 4000)
                
                ""dbo"".""CustOrderHist"" @CustomerID = @p0
            ");
        }

        [ActianTodo]
        public override void Throws_on_concurrent_command()
        {
            base.Throws_on_concurrent_command();
        }

        [ActianTodo]
        public override void Query_with_parameters()
        {
            base.Query_with_parameters();
            AssertSql(@"
                @p0='London' (Size = 4000)
                @p1='Sales Representative' (Size = 4000)
                
                SELECT COUNT(*) FROM ""Customers"" WHERE ""City"" = @p0 AND ""ContactTitle"" = @p1
            ");
        }

        [ActianTodo]
        public override void Query_with_dbParameter_with_name()
        {
            base.Query_with_dbParameter_with_name();
            AssertSql(@"
                @city='London' (Nullable = false) (Size = 6)
                
                SELECT COUNT(*) FROM ""Customers"" WHERE ""City"" = @city
            ");
        }

        [ActianTodo]
        public override void Query_with_positional_dbParameter_with_name()
        {
            base.Query_with_positional_dbParameter_with_name();
            AssertSql(@"
                @city='London' (Nullable = false) (Size = 6)
                
                SELECT COUNT(*) FROM ""Customers"" WHERE ""City"" = @city
            ");
        }

        [ActianTodo]
        public override void Query_with_positional_dbParameter_without_name()
        {
            base.Query_with_positional_dbParameter_without_name();
            AssertSql(@"
                @p0='London' (Nullable = false) (Size = 6)
                
                SELECT COUNT(*) FROM ""Customers"" WHERE ""City"" = @p0
            ");
        }

        [ActianTodo]
        public override void Query_with_dbParameters_mixed()
        {
            base.Query_with_dbParameters_mixed();
            AssertSql(
                @"
                    @p0='London' (Size = 4000)
                    @contactTitle='Sales Representative' (Nullable = false) (Size = 20)
                    
                    SELECT COUNT(*) FROM ""Customers"" WHERE ""City"" = @p0 AND ""ContactTitle"" = @contactTitle
                ",
                @"
                    @city='London' (Nullable = false) (Size = 6)
                    @p0='Sales Representative' (Size = 4000)
                    
                    SELECT COUNT(*) FROM ""Customers"" WHERE ""City"" = @city AND ""ContactTitle"" = @p0
                "
            );
        }

        [ActianTodo]
        public override void Query_with_parameters_interpolated()
        {
            base.Query_with_parameters_interpolated();
            AssertSql(@"
                @p0='London' (Size = 4000)
                @p1='Sales Representative' (Size = 4000)
                
                SELECT COUNT(*) FROM ""Customers"" WHERE ""City"" = @p0 AND ""ContactTitle"" = @p1
            ");
        }

        [ActianTodo]
        public override Task Executes_stored_procedure_async()
        {
            return base.Executes_stored_procedure_async();
        }

        [ActianTodo]
        public override Task Executes_stored_procedure_with_parameter_async()
        {
            return base.Executes_stored_procedure_with_parameter_async();
        }

        [ActianTodo]
        public override Task Executes_stored_procedure_with_generated_parameter_async()
        {
            return base.Executes_stored_procedure_with_generated_parameter_async();
        }

        [ActianTodo]
        public override Task Throws_on_concurrent_command_async()
        {
            return base.Throws_on_concurrent_command_async();
        }

        [ActianTodo]
        public override async Task Query_with_parameters_async()
        {
            await base.Query_with_parameters_async();
            AssertSql(@"
                @p0='London' (Size = 4000)
                @p1='Sales Representative' (Size = 4000)
                
                SELECT COUNT(*) FROM ""Customers"" WHERE ""City"" = @p0 AND ""ContactTitle"" = @p1
            ");
        }

        [ActianTodo]
        public override async Task Query_with_parameters_interpolated_async()
        {
            await base.Query_with_parameters_interpolated_async();
            AssertSql(@"
                @p0='London' (Size = 4000)
                @p1='Sales Representative' (Size = 4000)
                
                SELECT COUNT(*) FROM ""Customers"" WHERE ""City"" = @p0 AND ""ContactTitle"" = @p1
            ");
        }

        protected override DbParameter CreateDbParameter(string name, object value)
            => new IngresParameter { ParameterName = name, Value = value };

        protected override string TenMostExpensiveProductsSproc => @"""dbo"".""Ten Most Expensive Products""";

        protected override string CustomerOrderHistorySproc => @"""dbo"".""CustOrderHist"" @CustomerID";

        protected override string CustomerOrderHistoryWithGeneratedParameterSproc => @"""dbo"".""CustOrderHist"" @CustomerID = {0}";
    }
}
