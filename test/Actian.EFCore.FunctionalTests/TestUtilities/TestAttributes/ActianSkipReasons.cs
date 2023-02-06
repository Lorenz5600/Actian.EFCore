namespace Actian.EFCore.TestUtilities
{
    public static class ActianSkipReasons
    {
        public const string LongRunning = @"
            Long running
        ";

        public const string LongRunningAndCrashesDataAccessServer = @"
            Long running and crashes the Actian X Data Access Server
        ";

        public const string NoOrderByOffsetFirstAndFetchInSubselects = @"
            ORDER BY, OFFSET, FIRST and FETCH FIRST clauses cannot be used in subselects.
        ";

        public const string ExceptDoesNotAlwaysWorkCorrectlyWithNulls = @"
            EXCEPT does not always work correctly with nulls.
        ";

        public const string ConcatenatingNCharAndNVarcharResultsInFixedLengthString = @"
            Concatenating `nchar(...)` and `nvarchar(...)` in ActianX results in a fixed length string.
            This test expects a variable length string.
        ";
    }
}
