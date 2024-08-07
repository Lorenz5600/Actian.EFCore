using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Actian.EFCore.Infrastructure.Internal
{
    public interface IActianSingletonOptions : ISingletonOptions
    {

        int CompatibilityLevel { get; }

        int? CompatibilityLevelWithoutDefault { get; }
    }
}
