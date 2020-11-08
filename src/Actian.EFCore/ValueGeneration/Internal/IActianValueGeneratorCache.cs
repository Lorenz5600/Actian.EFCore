using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Actian.EFCore.ValueGeneration.Internal
{
    public interface IActianValueGeneratorCache : IValueGeneratorCache
    {
        ActianSequenceValueGeneratorState GetOrAddSequenceState(
            [NotNull] IProperty property,
            [NotNull] IRelationalConnection connection);
    }
}
