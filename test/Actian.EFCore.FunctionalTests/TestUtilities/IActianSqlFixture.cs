using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Actian.EFCore.TestUtilities
{
    public interface IActianSqlFixture
    {
        TestSqlLoggerFactory TestSqlLoggerFactory { get; }
    }
}
