using System.Linq;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestModels.UpdatesModel;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class UpdatesActianTest : UpdatesRelationalTestBase<UpdatesActianFixture>
    {
        public UpdatesActianTest(UpdatesActianFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        public override void Mutation_of_tracked_values_does_not_mutate_values_in_store()
        {
            try
            {
                base.Mutation_of_tracked_values_does_not_mutate_values_in_store();
            }
            finally
            {
                LogSql();
            }
        }

        public override void Save_partial_update()
        {
            try
            {
                base.Save_partial_update();
            }
            finally
            {
                LogSql();
            }
        }

        [ActianTodo]
        public override void Save_partial_update_on_missing_record_throws()
        {
            try
            {
                base.Save_partial_update_on_missing_record_throws();
            }
            finally
            {
                LogSql();
            }
        }

        [ActianTodo]
        public override void Save_partial_update_on_concurrency_token_original_value_mismatch_throws()
        {
            try
            {
                base.Save_partial_update_on_concurrency_token_original_value_mismatch_throws();
            }
            finally
            {
                LogSql();
            }
        }

        [ActianTodo]
        public override void Update_on_bytes_concurrency_token_original_value_mismatch_throws()
        {
            try
            {
                base.Update_on_bytes_concurrency_token_original_value_mismatch_throws();
            }
            finally
            {
                LogSql();
            }
        }

        public override void Update_on_bytes_concurrency_token_original_value_matches_does_not_throw()
        {
            try
            {
                base.Update_on_bytes_concurrency_token_original_value_matches_does_not_throw();
            }
            finally
            {
                LogSql();
            }
        }

        [ActianTodo]
        public override void Remove_on_bytes_concurrency_token_original_value_mismatch_throws()
        {
            try
            {
                base.Remove_on_bytes_concurrency_token_original_value_mismatch_throws();
            }
            finally
            {
                LogSql();
            }
        }

        public override void Remove_on_bytes_concurrency_token_original_value_matches_does_not_throw()
        {
            try
            {
                base.Remove_on_bytes_concurrency_token_original_value_matches_does_not_throw();
            }
            finally
            {
                LogSql();
            }
        }

        public override void Can_remove_partial()
        {
            try
            {
                base.Can_remove_partial();
            }
            finally
            {
                LogSql();
            }
        }

        [ActianTodo]
        public override void Remove_partial_on_missing_record_throws()
        {
            try
            {
                base.Remove_partial_on_missing_record_throws();
            }
            finally
            {
                LogSql();
            }
        }

        [ActianTodo]
        public override void Remove_partial_on_concurrency_token_original_value_mismatch_throws()
        {
            try
            {
                base.Remove_partial_on_concurrency_token_original_value_mismatch_throws();
            }
            finally
            {
                LogSql();
            }
        }

        [ActianTodo]
        public override void Save_replaced_principal()
        {
            base.Save_replaced_principal();
            AssertSql(
                @"
                    SELECT FIRST 2 ""c"".""Id"", ""c"".""Name"", ""c"".""PrincipalId""
                    FROM ""Categories"" AS ""c""
                ",
                @"
                    @__category_PrincipalId_0='778' (Nullable = true)
                    
                    SELECT ""p"".""Id"", ""p"".""DependentId"", ""p"".""Name"", ""p"".""Price""
                    FROM ""Products"" AS ""p""
                    WHERE ""p"".""DependentId"" = @__category_PrincipalId_0
                ",
                @"
                    @p1='78'
                    @p0='New Category' (Size = 4000)
                    
                    SET NOCOUNT ON;
                    UPDATE ""Categories"" SET ""Name"" = @p0
                    WHERE ""Id"" = @p1;
                    SELECT @@ROWCOUNT;
                ",
                @"
                    SELECT FIRST 2 ""c"".""Id"", ""c"".""Name"", ""c"".""PrincipalId""
                    FROM ""Categories"" AS ""c""
                ",
                @"
                    @__category_PrincipalId_0='778' (Nullable = true)
                    
                    SELECT ""p"".""Id"", ""p"".""DependentId"", ""p"".""Name"", ""p"".""Price""
                    FROM ""Products"" AS ""p""
                    WHERE ""p"".""DependentId"" = @__category_PrincipalId_0
                "
            );
        }

        public override void SaveChanges_processes_all_tracked_entities()
        {
            try
            {
                base.SaveChanges_processes_all_tracked_entities();
            }
            finally
            {
                LogSql();
            }
        }

        public override void SaveChanges_false_processes_all_tracked_entities_without_calling_AcceptAllChanges()
        {
            try
            {
                base.SaveChanges_false_processes_all_tracked_entities_without_calling_AcceptAllChanges();
            }
            finally
            {
                LogSql();
            }
        }

        public override void Identifiers_are_generated_correctly()
        {
            using (var context = CreateContext())
            {
                var entityType = context.Model.FindEntityType(
                    typeof(
                        LoginEntityTypeWithAnExtremelyLongAndOverlyConvolutedNameThatIsUsedToVerifyThatTheStoreIdentifierGenerationLengthLimitIsWorkingCorrectly
                    ));
                Assert.Equal(
                    "LoginEntityTypeWithAnExtremelyLongAndOverlyConvolutedNameThatIsUsedToVerifyThatTheStoreIdentifierGenerationLengthLimitIsWorking~",
                    entityType.GetTableName());
                Assert.Equal(
                    "PK_LoginEntityTypeWithAnExtremelyLongAndOverlyConvolutedNameThatIsUsedToVerifyThatTheStoreIdentifierGenerationLengthLimitIsWork~",
                    entityType.GetKeys().Single().GetName());
                Assert.Equal(
                    "FK_LoginEntityTypeWithAnExtremelyLongAndOverlyConvolutedNameThatIsUsedToVerifyThatTheStoreIdentifierGenerationLengthLimitIsWork~",
                    entityType.GetForeignKeys().Single().GetConstraintName());
                Assert.Equal(
                    "IX_LoginEntityTypeWithAnExtremelyLongAndOverlyConvolutedNameThatIsUsedToVerifyThatTheStoreIdentifierGenerationLengthLimitIsWork~",
                    entityType.GetIndexes().Single().GetName());

                var entityType2 = context.Model.FindEntityType(
                    typeof(
                        LoginEntityTypeWithAnExtremelyLongAndOverlyConvolutedNameThatIsUsedToVerifyThatTheStoreIdentifierGenerationLengthLimitIsWorkingCorrectlyDetails
                    ));

                Assert.Equal(
                    "LoginEntityTypeWithAnExtremelyLongAndOverlyConvolutedNameThatIsUsedToVerifyThatTheStoreIdentifierGenerationLengthLimitIsWorkin~1",
                    entityType2.GetTableName());
                Assert.Equal(
                    "PK_LoginDetails",
                    entityType2.GetKeys().Single().GetName());
                Assert.Equal(
                    "ExtraPropertyWithAnExtremelyLongAndOverlyConvolutedNameThatIsUsedToVerifyThatTheStoreIdentifierGenerationLengthLimitIsWorkingCo~",
                    entityType2.GetProperties().ElementAt(1).GetColumnName());
                Assert.Equal(
                    "ExtraPropertyWithAnExtremelyLongAndOverlyConvolutedNameThatIsUsedToVerifyThatTheStoreIdentifierGenerationLengthLimitIsWorkingC~1",
                    entityType2.GetProperties().ElementAt(2).GetColumnName());
                Assert.Equal(
                    "IX_LoginEntityTypeWithAnExtremelyLongAndOverlyConvolutedNameThatIsUsedToVerifyThatTheStoreIdentifierGenerationLengthLimitIsWor~1",
                    entityType2.GetIndexes().Single().GetName());
            }
        }
    }
}
