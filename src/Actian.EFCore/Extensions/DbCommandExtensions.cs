using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Actian.EFCore.Extensions;

namespace Actian.EFCore
{
    public static class DbCommandExtensions
    {
        public static T ExecuteScalar<T>(this DbCommand command)
        {
            return command.ExecuteScalar().ChangeType<T>();
        }

        public static async Task<T> ExecuteScalarAsync<T>(this DbCommand command, CancellationToken cancellationToken = default)
        {
            var value = await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
            return value.ChangeType<T>();
        }
    }
}
