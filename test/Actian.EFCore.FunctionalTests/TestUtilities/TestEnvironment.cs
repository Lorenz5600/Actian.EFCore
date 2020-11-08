using System;
using System.Data.Common;

namespace Actian.EFCore.TestUtilities
{
    public static class TestEnvironment
    {
        public static string TestEnvironmentName
        {
            get
            {
                var name = Environment.GetEnvironmentVariable("ACTIAN_EFCORE_ENVIRONMENT");
                if (string.IsNullOrWhiteSpace(name))
                    return "DEFAULT";
                return name;
            }
        }

        public static string ConnectionString
        {
            get
            {
                var connectionString = Environment.GetEnvironmentVariable($"ACTIAN_EFCORE_{TestEnvironmentName}");
                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new Exception($"Connection string not found for test environment {TestEnvironmentName}");
                return connectionString;
            }
        }

        public static string Database
        {
            get
            {
                var builder = new DbConnectionStringBuilder
                {
                    ConnectionString = ConnectionString
                };
                return builder.TryGetValue("Database", out var database)
                    ? database as string
                    : throw new Exception($"Database not found for test environment {TestEnvironmentName}");
            }
        }

        public static string UserId
        {
            get
            {
                var builder = new DbConnectionStringBuilder
                {
                    ConnectionString = ConnectionString
                };
                return builder.TryGetValue("User ID", out var userId)
                    ? userId as string
                    : throw new Exception($"User id not found for test environment {TestEnvironmentName}");
            }
        }
    }
}
