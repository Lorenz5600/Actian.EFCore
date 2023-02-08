using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

// ReSharper disable once CheckNamespace
namespace Actian.EFCore.Metadata.Conventions
{
    /// <summary>
    ///     A convention that configures the default model <see cref="ActianValueGenerationStrategy" /> as
    ///     <see cref="ActianValueGenerationStrategy.IdentityByDefaultColumn" />.
    /// </summary>
    public class ActianValueGenerationStrategyConvention : IModelInitializedConvention, IModelFinalizedConvention
    {
        public ActianValueGenerationStrategyConvention(
            [NotNull] ProviderConventionSetBuilderDependencies dependencies,
            [NotNull] RelationalConventionSetBuilderDependencies relationalDependencies)
        {
            Dependencies = dependencies;
        }

        protected virtual ProviderConventionSetBuilderDependencies Dependencies { get; }

        public virtual void ProcessModelInitialized(IConventionModelBuilder modelBuilder, IConventionContext<IConventionModelBuilder> context)
        {
            modelBuilder.HasValueGenerationStrategy(ActianValueGenerationStrategy.IdentityByDefaultColumn);
        }

        public virtual void ProcessModelFinalized(
            IConventionModelBuilder modelBuilder, IConventionContext<IConventionModelBuilder> context)
        {
            foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
            {
                foreach (var property in entityType.GetDeclaredProperties())
                {
                                var strategy = property.GetValueGenerationStrategy();
                    if (strategy != ActianValueGenerationStrategy.None)
                    {
                        property.Builder.HasValueGenerationStrategy(strategy);
                    }
                }
            }
        }
    }
}
