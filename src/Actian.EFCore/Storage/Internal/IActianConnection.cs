using Microsoft.EntityFrameworkCore.Storage;

namespace Actian.EFCore.Storage.Internal
{
    /// <inheritdoc />
    public interface IActianConnection : IRelationalConnection
    {
        /// <summary>
        /// Creates a new connection to the iidbdb database for the current connection
        /// </summary>
        /// <returns>A new connection to the iidbdb database for the current connection</returns>
        IActianConnection CreateIIDbDbConnection();
    }
}
