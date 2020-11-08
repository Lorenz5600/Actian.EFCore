using Actian.EFCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Actian.EFCore.Internal
{
    public class ActianOptions : IActianOptions
    {
        public virtual void Initialize(IDbContextOptions options)
        {
            //var actianOptions = options.FindExtension<ActianOptionsExtension>() ?? new ActianOptionsExtension();
        }

        public virtual void Validate(IDbContextOptions options)
        {
        }
    }
}
