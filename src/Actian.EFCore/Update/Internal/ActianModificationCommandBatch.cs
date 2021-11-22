using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Update;

namespace Actian.EFCore.Update.Internal
{
    public class ActianModificationCommandBatch : AffectedCountModificationCommandBatch
    {
        private const int DefaultBatchSize = 1;
        private const int MaxParameterCount = 2100;
        private readonly int _maxBatchSize;
        private int _parameterCount = 1; // Implicit parameter for the command text

        public ActianModificationCommandBatch(
            [NotNull] ModificationCommandBatchFactoryDependencies dependencies,
            int? maxBatchSize)
            : base(dependencies)
        {
            if (maxBatchSize.HasValue && maxBatchSize.Value <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxBatchSize), RelationalStrings.InvalidMaxBatchSize);

            _maxBatchSize = maxBatchSize ?? DefaultBatchSize;
        }

        protected override bool CanAddCommand(ModificationCommand modificationCommand)
        {
            return false;
            //if (ModificationCommands.Count >= _maxBatchSize)
            //    return false;

            //var additionalParameterCount = CountParameters(modificationCommand);

            //if (_parameterCount + additionalParameterCount >= MaxParameterCount)
            //{
            //    return false;
            //}

            //_parameterCount += additionalParameterCount;
            //return true;
        }

        protected override bool IsCommandTextValid()
            => true;

        protected override int GetParameterCount()
            => _parameterCount;

        private static int CountParameters(ModificationCommand modificationCommand)
        {
            var parameterCount = 0;

            for (var columnIndex = 0; columnIndex < modificationCommand.ColumnModifications.Count; columnIndex++)
            {
                var columnModification = modificationCommand.ColumnModifications[columnIndex];
                if (columnModification.UseCurrentValueParameter)
                {
                    parameterCount += 1;
                }

                if (columnModification.UseOriginalValueParameter)
                {
                    parameterCount += 1;
                }
            }

            return parameterCount;
        }
    }
}
