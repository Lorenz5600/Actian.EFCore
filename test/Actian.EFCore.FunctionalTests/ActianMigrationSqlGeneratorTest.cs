using Actian.EFCore.TestUtilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class ActianMigrationSqlGeneratorTest : MigrationSqlGeneratorTestBase
    {
        public ActianMigrationSqlGeneratorTest(ITestOutputHelper testOutputHelper)
            : base(ActianTestHelpers.Instance)
        {
            TestEnvironment.Log(this, testOutputHelper);
        }

        [ActianTodo]
        public override void CreateIndexOperation_with_filter_where_clause()
        {
            base.CreateIndexOperation_with_filter_where_clause();
            AssertSql(@"
                CREATE INDEX ""IX_People_Name"" ON ""People"" (""Name"") WHERE ""Name"" IS NOT NULL;
            ");
        }

        [ActianTodo]
        public override void CreateIndexOperation_with_filter_where_clause_and_is_unique()
        {
            base.CreateIndexOperation_with_filter_where_clause_and_is_unique();
            AssertSql(@"
                CREATE UNIQUE INDEX ""IX_People_Name"" ON ""People"" (""Name"") WHERE ""Name"" IS NOT NULL AND <> '';
            ");
        }

        public override void AddColumnOperation_with_defaultValue()
        {
            base.AddColumnOperation_with_defaultValue();
        }

        public override void AddColumnOperation_with_defaultValueSql()
        {
            base.AddColumnOperation_with_defaultValueSql();
        }

        public override void AddColumnOperation_without_column_type()
        {
            base.AddColumnOperation_without_column_type();
            AssertSql(@"
                ALTER TABLE ""People"" ADD ""Alias"" long nvarchar NOT NULL
                GO

                MODIFY ""People"" TO RECONSTRUCT
            ");
        }

        public override void AddColumnOperation_with_ansi()
        {
            base.AddColumnOperation_with_ansi();
            AssertSql(@"
                ALTER TABLE ""Person"" ADD ""Name"" long varchar WITH NULL
                GO

                MODIFY ""Person"" TO RECONSTRUCT
            ");
        }

        public override void AddColumnOperation_with_unicode_overridden()
        {
            base.AddColumnOperation_with_unicode_overridden();
            AssertSql(@"
                ALTER TABLE ""Person"" ADD ""Name"" long nvarchar WITH NULL
                GO

                MODIFY ""Person"" TO RECONSTRUCT
            ");
        }

        public override void AddColumnOperation_with_unicode_no_model()
        {
            base.AddColumnOperation_with_unicode_no_model();
            AssertSql(@"
                ALTER TABLE ""Person"" ADD ""Name"" long varchar WITH NULL
                GO

                MODIFY ""Person"" TO RECONSTRUCT
            ");
        }

        public override void AddColumnOperation_with_fixed_length()
        {
            base.AddColumnOperation_with_fixed_length();
        }

        public override void AddColumnOperation_with_fixed_length_no_model()
        {
            base.AddColumnOperation_with_fixed_length_no_model();
        }

        public override void AddColumnOperation_with_maxLength()
        {
            base.AddColumnOperation_with_maxLength();
            AssertSql(@"
                ALTER TABLE ""Person"" ADD ""Name"" nvarchar(30) WITH NULL
                GO

                MODIFY ""Person"" TO RECONSTRUCT
            ");
        }

        public override void AddColumnOperation_with_maxLength_overridden()
        {
            base.AddColumnOperation_with_maxLength_overridden();
            AssertSql(@"
                ALTER TABLE ""Person"" ADD ""Name"" nvarchar(32) WITH NULL
                GO

                MODIFY ""Person"" TO RECONSTRUCT
            ");
        }

        public override void AddColumnOperation_with_maxLength_no_model()
        {
            base.AddColumnOperation_with_maxLength_no_model();
        }

        public override void AddColumnOperation_with_maxLength_on_derived()
        {
            base.AddColumnOperation_with_maxLength_on_derived();
            AssertSql(@"
                ALTER TABLE ""Person"" ADD ""Name"" nvarchar(30) WITH NULL
                GO

                MODIFY ""Person"" TO RECONSTRUCT
            ");
        }

        public override void AddColumnOperation_with_shared_column()
        {
            base.AddColumnOperation_with_shared_column();
            AssertSql(@"
                ALTER TABLE ""Base"" ADD ""Foo"" long nvarchar WITH NULL
                GO

                MODIFY ""Base"" TO RECONSTRUCT
            ");
        }

        public override void AddForeignKeyOperation_with_name()
        {
            base.AddForeignKeyOperation_with_name();
        }

        public override void AddForeignKeyOperation_without_name()
        {
            base.AddForeignKeyOperation_without_name();
        }

        public override void AddForeignKeyOperation_without_principal_columns()
        {
            base.AddForeignKeyOperation_without_principal_columns();
        }

        public override void AddPrimaryKeyOperation_with_name()
        {
            base.AddPrimaryKeyOperation_with_name();
        }

        public override void AddPrimaryKeyOperation_without_name()
        {
            base.AddPrimaryKeyOperation_without_name();
        }

        public override void AddUniqueConstraintOperation_with_name()
        {
            base.AddUniqueConstraintOperation_with_name();
        }

        public override void AddUniqueConstraintOperation_without_name()
        {
            base.AddUniqueConstraintOperation_without_name();
        }

        public override void CreateCheckConstraintOperation_with_name()
        {
            base.CreateCheckConstraintOperation_with_name();
        }

        public override void AlterColumnOperation()
        {
            base.AlterColumnOperation();
            AssertSql(@"
                ALTER TABLE ""dbo"".""People"" ALTER COLUMN ""LuckyNumber"" int NOT NULL WITH DEFAULT 7
                GO

                set session authorization ""dbo""
                GO

                MODIFY ""dbo"".""People"" TO RECONSTRUCT
                GO

                set session authorization initial_user
            ");
        }

        public override void AlterColumnOperation_without_column_type()
        {
            base.AlterColumnOperation_without_column_type();
            AssertSql(@"
                ALTER TABLE ""People"" ALTER COLUMN ""LuckyNumber"" integer NOT NULL
                GO

                MODIFY ""People"" TO RECONSTRUCT
            ");
        }

        public override void AlterSequenceOperation_with_minValue_and_maxValue()
        {
            base.AlterSequenceOperation_with_minValue_and_maxValue();
        }

        public override void AlterSequenceOperation_without_minValue_and_maxValue()
        {
            base.AlterSequenceOperation_without_minValue_and_maxValue();
        }

        public override void RenameTableOperation_legacy()
        {
            base.RenameTableOperation_legacy();
            AssertSql(@"
                set session authorization ""dbo""
                GO
                
                ALTER TABLE ""dbo"".""People"" RENAME TO ""Person""
                GO
                
                MODIFY ""dbo"".""Person"" TO RECONSTRUCT
                GO
                
                set session authorization initial_user
            ");
        }

        public override void RenameTableOperation()
        {
            base.RenameTableOperation();
            AssertSql(@"
                set session authorization ""dbo""
                GO
                
                ALTER TABLE ""dbo"".""People"" RENAME TO ""Person""
                GO
                
                MODIFY ""dbo"".""Person"" TO RECONSTRUCT
                GO
                
                set session authorization initial_user
            ");
        }

        [ActianTodo]
        public override void CreateIndexOperation_unique()
        {
            base.CreateIndexOperation_unique();
            AssertSql(@"
                set session authorization ""dbo""
                GO

                CREATE UNIQUE INDEX ""IX_People_Name"" ON ""dbo"".""People"" (""FirstName"", ""LastName"")
                GO

                set session authorization initial_user
            ");
        }

        public override void CreateIndexOperation_nonunique()
        {
            base.CreateIndexOperation_nonunique();
            AssertSql(@"
                CREATE INDEX ""IX_People_Name"" ON ""People"" (""Name"")
            ");
        }

        [ActianTodo]
        public override void CreateIndexOperation_with_where_clauses()
        {
            base.CreateIndexOperation_with_where_clauses();
        }

        public override void CreateSequenceOperation_with_minValue_and_maxValue()
        {
            base.CreateSequenceOperation_with_minValue_and_maxValue();
        }

        public override void CreateSequenceOperation_with_minValue_and_maxValue_not_long()
        {
            base.CreateSequenceOperation_with_minValue_and_maxValue_not_long();
        }

        public override void CreateSequenceOperation_without_minValue_and_maxValue()
        {
            base.CreateSequenceOperation_without_minValue_and_maxValue();
        }

        [ActianTodo]
        public override void CreateTableOperation()
        {
            base.CreateTableOperation();
            AssertSql(@"
                CREATE TABLE ""dbo"".""People"" (
                    ""Id"" integer NOT NULL,
                    ""EmployerId"" integer WITH NULL,
                    ""SSN"" char(11) WITH NULL,
                    PRIMARY KEY (""Id""),
                    UNIQUE (""SSN""),
                    CHECK (SSN > 0),
                    FOREIGN KEY (""EmployerId"") REFERENCES ""Companies"" (""Id"")
                )
                COMMENT ON COLUMN ""dbo"".""People"".""EmployerId"" IS N'Employer ID comment'
                COMMENT ON TABLE ""dbo"".""People"" IS N'Table comment'
            ");
        }

        public override void CreateTableOperation_no_key()
        {
            base.CreateTableOperation_no_key();
            AssertSql(@"
                CREATE TABLE ""Anonymous"" (
                    ""Value"" integer NOT NULL
                )
            ");
        }

        public override void DropColumnOperation()
        {
            base.DropColumnOperation();
            AssertSql(@"
                set session authorization ""dbo""
                GO
                
                ALTER TABLE ""dbo"".""People"" DROP COLUMN ""LuckyNumber"" RESTRICT
                GO
                
                MODIFY ""dbo"".""People"" TO RECONSTRUCT
                GO
                
                set session authorization initial_user
            ");
        }

        public override void DropForeignKeyOperation()
        {
            base.DropForeignKeyOperation();
        }

        public override void DropIndexOperation()
        {
            base.DropIndexOperation();
            AssertSql(@"
                set session authorization ""dbo""
                GO
                
                DROP INDEX ""dbo"".""IX_People_Name""
                GO
                
                set session authorization initial_user
            ");
        }

        public override void DropPrimaryKeyOperation()
        {
            base.DropPrimaryKeyOperation();
        }

        public override void DropSequenceOperation()
        {
            base.DropSequenceOperation();
        }

        public override void DropTableOperation()
        {
            base.DropTableOperation();
        }

        public override void DropUniqueConstraintOperation()
        {
            base.DropUniqueConstraintOperation();
        }

        public override void DropCheckConstraintOperation()
        {
            base.DropCheckConstraintOperation();
        }

        public override void SqlOperation()
        {
            base.SqlOperation();
        }

        public override void InsertDataOperation()
        {
            base.InsertDataOperation();
            AssertSql(@"
                INSERT INTO ""People"" (""Id"", ""Full Name"")
                VALUES (0, NULL)
                INSERT INTO ""People"" (""Id"", ""Full Name"")
                VALUES (1, N'Daenerys Targaryen')
                INSERT INTO ""People"" (""Id"", ""Full Name"")
                VALUES (2, N'John Snow')
                INSERT INTO ""People"" (""Id"", ""Full Name"")
                VALUES (3, N'Arya Stark')
                INSERT INTO ""People"" (""Id"", ""Full Name"")
                VALUES (4, N'Harry Strickland')
            ");
        }

        public override void DeleteDataOperation_simple_key()
        {
            base.DeleteDataOperation_simple_key();
            AssertSql(@"
                DELETE FROM ""People""
                WHERE ""Id"" = 2
                DELETE FROM ""People""
                WHERE ""Id"" = 4
            ");
        }

        public override void DeleteDataOperation_composite_key()
        {
            base.DeleteDataOperation_composite_key();
            AssertSql(@"
                DELETE FROM ""People""
                WHERE ""First Name"" = N'Hodor' AND ""Last Name"" IS NULL
                DELETE FROM ""People""
                WHERE ""First Name"" = N'Daenerys' AND ""Last Name"" = N'Targaryen'
            ");
        }

        public override void UpdateDataOperation_simple_key()
        {
            base.UpdateDataOperation_simple_key();
            AssertSql(@"
                UPDATE ""People"" SET ""Full Name"" = N'Daenerys Stormborn'
                WHERE ""Id"" = 1
                UPDATE ""People"" SET ""Full Name"" = N'Homeless Harry Strickland'
                WHERE ""Id"" = 4
            ");
        }

        public override void UpdateDataOperation_composite_key()
        {
            base.UpdateDataOperation_composite_key();
            AssertSql(@"
                UPDATE ""People"" SET ""First Name"" = N'Hodor'
                WHERE ""Id"" = 0 AND ""Last Name"" IS NULL
                UPDATE ""People"" SET ""First Name"" = N'Harry'
                WHERE ""Id"" = 4 AND ""Last Name"" = N'Strickland'
            ");
        }

        public override void UpdateDataOperation_multiple_columns()
        {
            base.UpdateDataOperation_multiple_columns();
            AssertSql(@"
                UPDATE ""People"" SET ""First Name"" = N'Daenerys', ""Nickname"" = N'Dany'
                WHERE ""Id"" = 1
                UPDATE ""People"" SET ""First Name"" = N'Harry', ""Nickname"" = N'Homeless'
                WHERE ""Id"" = 4
            ");
        }

        protected new void AssertSql(string expected)
            => Sql.Should().NotDifferFrom(expected);
    }
}
