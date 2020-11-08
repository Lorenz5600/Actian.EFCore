using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Actian.EFCore.Infrastructure.Internal
{
    public interface IActianOptions : ISingletonOptions
    {
        ///// <summary>
        ///// Reflects the option set by <see cref="ActianDbContextOptionsBuilder.UseRowNumberForPaging" />.
        ///// </summary>
        //bool RowNumberPagingEnabled { get; }
    }
}
