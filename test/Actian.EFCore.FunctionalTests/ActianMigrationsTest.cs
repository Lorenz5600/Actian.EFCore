using System;
using System.Linq;
using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using FluentAssertions;
//using Identity30.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public partial class ActianMigrationsTest : MigrationsTestBase<ActianMigrationsFixture>
    {
        public ActianMigrationsTest(ActianMigrationsFixture fixture, ITestOutputHelper output)
            : base(fixture)
        {
            TestEnvironment.Log(this, output);
            Output = output;
        }

        protected override void GiveMeSomeTime(DbContext db)
        {
        }

        protected override Task GiveMeSomeTimeAsync(DbContext db)
        {
            return Task.CompletedTask;
        }

        private ITestOutputHelper Output { get; }
        protected new string Sql
        {
            get
            {
                var sql = base.Sql;
                Output.WriteLine(sql);
                return sql;
            }
        }


        public override void Can_apply_all_migrations()
        {
            base.Can_apply_all_migrations();
        }

        public override void Can_apply_one_migration()
        {
            base.Can_apply_one_migration();
        }

        public override void Can_revert_all_migrations()
        {
            base.Can_apply_one_migration();
        }

        public override void Can_revert_one_migrations()
        {
            base.Can_revert_one_migrations();
        }

        public override Task Can_apply_all_migrations_async()
        {
            return base.Can_apply_all_migrations_async();
        }

        public override void Can_generate_no_migration_script()
        {
            Clean();

            base.Can_generate_no_migration_script();

            Sql.Should().NotDifferFrom(@"
                CREATE TABLE ""__EFMigrationsHistory"" (
                    ""MigrationId"" nvarchar(150) NOT NULL,
                    ""ProductVersion"" nvarchar(32) NOT NULL,
                    CONSTRAINT ""PK___EFMigrationsHistory"" PRIMARY KEY (""MigrationId"")
                )
            ");
        }

        public override void Can_generate_migration_from_initial_database_to_initial()
        {
            Clean();

            base.Can_generate_migration_from_initial_database_to_initial();

            Sql.Should().NotDifferFrom(@"
                CREATE TABLE ""__EFMigrationsHistory"" (
                    ""MigrationId"" nvarchar(150) NOT NULL,
                    ""ProductVersion"" nvarchar(32) NOT NULL,
                    CONSTRAINT ""PK___EFMigrationsHistory"" PRIMARY KEY (""MigrationId"")
                )
            ");
        }

        public override void Can_generate_up_scripts()
        {
            Clean();

            base.Can_generate_up_scripts();

            Sql.Should().NotDifferFrom(@"
                CREATE TABLE ""__EFMigrationsHistory"" (
                    ""MigrationId"" nvarchar(150) NOT NULL,
                    ""ProductVersion"" nvarchar(32) NOT NULL,
                    CONSTRAINT ""PK___EFMigrationsHistory"" PRIMARY KEY (""MigrationId"")
                )

                CREATE TABLE ""Table1"" (
                    ""Id"" integer NOT NULL,
                    ""Foo"" integer NOT NULL,
                    CONSTRAINT ""PK_Table1"" PRIMARY KEY (""Id"")
                )

                INSERT INTO ""__EFMigrationsHistory"" (""MigrationId"", ""ProductVersion"")
                VALUES (N'00000000000001_Migration1', N'7.0.0-test')

                ALTER TABLE ""Table1"" RENAME COLUMN ""Foo"" TO ""Bar""

                MODIFY ""Table1"" TO RECONSTRUCT

                INSERT INTO ""__EFMigrationsHistory"" (""MigrationId"", ""ProductVersion"")
                VALUES (N'00000000000002_Migration2', N'7.0.0-test')

                INSERT INTO ""__EFMigrationsHistory"" (""MigrationId"", ""ProductVersion"")
                VALUES (N'00000000000003_Migration3', N'7.0.0-test')
            ");
        }

        public override void Can_generate_one_up_script()
        {
            Clean();

            base.Can_generate_one_up_script();

            Sql.Should().NotDifferFrom(@"
                ALTER TABLE ""Table1"" RENAME COLUMN ""Foo"" TO ""Bar""

                MODIFY ""Table1"" TO RECONSTRUCT

                INSERT INTO ""__EFMigrationsHistory"" (""MigrationId"", ""ProductVersion"")
                VALUES (N'00000000000002_Migration2', N'7.0.0-test')
            ");
        }

        public override void Can_generate_up_script_using_names()
        {
            Clean();

            base.Can_generate_up_script_using_names();

            Sql.Should().NotDifferFrom(@"
                ALTER TABLE ""Table1"" RENAME COLUMN ""Foo"" TO ""Bar""

                MODIFY ""Table1"" TO RECONSTRUCT

                INSERT INTO ""__EFMigrationsHistory"" (""MigrationId"", ""ProductVersion"")
                VALUES (N'00000000000002_Migration2', N'7.0.0-test')
            ");
        }

        [ActianTodo("Idempotent not supported (yet)")]
        public override void Can_generate_idempotent_up_scripts()
        {
            Clean();

            base.Can_generate_idempotent_up_scripts();

            Sql.Should().NotDifferFrom(@"
                CREATE TABLE ""__EFMigrationsHistory"" (
                    ""MigrationId"" nvarchar(150) NOT NULL,
                    ""ProductVersion"" nvarchar(32) NOT NULL,
                    CONSTRAINT ""PK___EFMigrationsHistory"" PRIMARY KEY (""MigrationId"")
                );

                IF NOT EXISTS (SELECT 1 FROM ""__EFMigrationsHistory"" WHERE ""MigrationId"" = N'00000000000001_Migration1')
                BEGIN
                    CREATE TABLE ""Table1"" (
                        ""Id"" integer NOT NULL,
                        ""Foo"" integer NOT NULL,
                        CONSTRAINT ""PK_Table1"" PRIMARY KEY (""Id"")
                    );
                END;

                IF NOT EXISTS (SELECT 1 FROM ""__EFMigrationsHistory"" WHERE ""MigrationId"" = N'00000000000001_Migration1')
                BEGIN
                    INSERT INTO ""__EFMigrationsHistory"" (""MigrationId"", ""ProductVersion"")
                    VALUES (N'00000000000001_Migration1', N'7.0.0-test');
                END;

                IF NOT EXISTS (SELECT 1 FROM ""__EFMigrationsHistory"" WHERE ""MigrationId"" = N'00000000000002_Migration2')
                BEGIN
                    ALTER TABLE ""Table1"" RENAME COLUMN ""Foo"" TO ""Bar"";
                    MODIFY ""Table1"" TO RECONSTRUCT;
                END;

                IF NOT EXISTS (SELECT 1 FROM ""__EFMigrationsHistory"" WHERE ""MigrationId"" = N'00000000000002_Migration2')
                BEGIN
                    INSERT INTO ""__EFMigrationsHistory"" (""MigrationId"", ""ProductVersion"")
                    VALUES (N'00000000000002_Migration2', N'7.0.0-test');
                END;

                IF NOT EXISTS (SELECT 1 FROM ""__EFMigrationsHistory"" WHERE ""MigrationId"" = N'00000000000003_Migration3')
                BEGIN
                    INSERT INTO ""__EFMigrationsHistory"" (""MigrationId"", ""ProductVersion"")
                    VALUES (N'00000000000003_Migration3', N'7.0.0-test');
                END;
            ");
        }

        public override void Can_generate_down_scripts()
        {
            Clean();

            base.Can_generate_down_scripts();

            Sql.Should().NotDifferFrom(@"
                ALTER TABLE ""Table1"" RENAME COLUMN ""Bar"" TO ""Foo""

                MODIFY ""Table1"" TO RECONSTRUCT

                DELETE FROM ""__EFMigrationsHistory""
                WHERE ""MigrationId"" = N'00000000000002_Migration2'

                DROP ""Table1""

                DELETE FROM ""__EFMigrationsHistory""
                WHERE ""MigrationId"" = N'00000000000001_Migration1'
            ");
        }

        public override void Can_generate_one_down_script()
        {
            Clean();

            base.Can_generate_one_down_script();

            Sql.Should().NotDifferFrom(@"
                ALTER TABLE ""Table1"" RENAME COLUMN ""Bar"" TO ""Foo""

                MODIFY ""Table1"" TO RECONSTRUCT

                DELETE FROM ""__EFMigrationsHistory""
                WHERE ""MigrationId"" = N'00000000000002_Migration2'
            ");
        }

        public override void Can_generate_down_script_using_names()
        {
            Clean();

            base.Can_generate_down_script_using_names();

            Sql.Should().NotDifferFrom(@"
                ALTER TABLE ""Table1"" RENAME COLUMN ""Bar"" TO ""Foo""

                MODIFY ""Table1"" TO RECONSTRUCT

                DELETE FROM ""__EFMigrationsHistory""
                WHERE ""MigrationId"" = N'00000000000002_Migration2'
            ");
        }

        [ActianTodo("Idempotent not supported (yet)")]
        public override void Can_generate_idempotent_down_scripts()
        {
            Clean();

            base.Can_generate_idempotent_down_scripts();

            Sql.Should().NotDifferFrom(@"
                IF EXISTS (SELECT 1 FROM ""__EFMigrationsHistory"" WHERE ""MigrationId"" = N'00000000000002_Migration2')
                BEGIN
                    ALTER TABLE ""Table1"" RENAME COLUMN ""Bar"" TO ""Foo"";
                    MODIFY ""Table1"" TO RECONSTRUCT;
                END;

                IF EXISTS (SELECT 1 FROM ""__EFMigrationsHistory"" WHERE ""MigrationId"" = N'00000000000002_Migration2')
                BEGIN
                    DELETE FROM ""__EFMigrationsHistory""
                    WHERE ""MigrationId"" = N'00000000000002_Migration2';
                END;

                IF EXISTS (SELECT 1 FROM ""__EFMigrationsHistory"" WHERE ""MigrationId"" = N'00000000000001_Migration1')
                BEGIN
                    DROP ""Table1"";
                END;

                IF EXISTS (SELECT 1 FROM ""__EFMigrationsHistory"" WHERE ""MigrationId"" = N'00000000000001_Migration1')
                BEGIN
                    DELETE FROM ""__EFMigrationsHistory""
                    WHERE ""MigrationId"" = N'00000000000001_Migration1';
                END;
            ");
        }

        public override void Can_get_active_provider()
        {
            base.Can_get_active_provider();
        }

        /// <summary>
        ///     Creating databases and executing DDL is slow. This oddly-structured test allows us to get the most amount of
        ///     coverage using the least amount of database operations.
        /// </summary>
        public override Task Can_execute_operations()
        {
            return base.Can_execute_operations();
        }

        [ActianTodo]
        public override void Can_diff_against_2_2_model()
        {
            using (var context = new ModelSnapshot22.BloggingContext())
            {
                DiffSnapshot(new ModelSnapshot22.BloggingContextModelSnapshot22(), context);
            }
        }

        [ActianTodo]
        public override void Can_diff_against_3_0_ASP_NET_Identity_model()
        {
            throw new NotImplementedException();
        }

        [ActianTodo]
        public override void Can_diff_against_2_2_ASP_NET_Identity_model()
        {
            throw new NotImplementedException();
        }

        [ActianTodo]
        public override void Can_diff_against_2_1_ASP_NET_Identity_model()
        {
            throw new NotImplementedException();
        }

        protected override Task ExecuteAsync(IServiceProvider services, Action<MigrationBuilder> buildMigration)
        {
            var generator = services.GetRequiredService<IMigrationsSqlGenerator>();
            var executor = new TestMigrationCommandExecutor(Output);
            var connection = services.GetRequiredService<IRelationalConnection>();
            var databaseProvider = services.GetRequiredService<IDatabaseProvider>();

            var migrationBuilder = new MigrationBuilder(databaseProvider.Name);
            buildMigration(migrationBuilder);
            var operations = migrationBuilder.Operations.ToList();

            var commandList = generator.Generate(operations);

            return executor.ExecuteNonQueryAsync(commandList, connection);
        }

        private void Clean()
        {
            using (var db = Fixture.CreateContext())
            {
                db.Database.EnsureClean();
            }
        }
    }
}
