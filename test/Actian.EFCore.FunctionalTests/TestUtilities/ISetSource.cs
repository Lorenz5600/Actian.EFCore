using System.Linq;

namespace Actian.EFCore.TestUtilities
{
    internal interface ISetSource
    {
        IQueryable<TEntity> Set<TEntity>()
            where TEntity : class;
    }
}
