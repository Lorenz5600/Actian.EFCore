using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Update;

namespace Actian.EFCore.Update.Internal
{
    public class ActianModificationCommandBatch : AffectedCountModificationCommandBatch
    {
        public ActianModificationCommandBatch(
            [NotNull] ModificationCommandBatchFactoryDependencies dependencies,
            int? maxBatchSize)
            : base(dependencies)
        {
            MaxBatchSize = maxBatchSize;
        }

        public int? MaxBatchSize { get; }

        protected override bool CanAddCommand([NotNull] ModificationCommand modificationCommand)
        {
            throw new NotImplementedException();
        }

        protected override bool IsCommandTextValid()
        {
            throw new NotImplementedException();
        }
    }
}
