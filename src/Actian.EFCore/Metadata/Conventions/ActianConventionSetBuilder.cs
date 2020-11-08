using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

// TODO: Conventions
namespace Actian.EFCore.Metadata.Conventions
{
    public class ActianConventionSetBuilder : RelationalConventionSetBuilder
    {
        private readonly ISqlGenerationHelper _sqlGenerationHelper;

        /// <summary>
        /// Creates a new <see cref="ActianConventionSetBuilder" /> instance.
        /// </summary>
        /// <param name="dependencies"> The core dependencies for this service. </param>
        /// <param name="relationalDependencies"> The relational dependencies for this service. </param>
        /// <param name="sqlGenerationHelper"> The SQL generation helper to use. </param>
        public ActianConventionSetBuilder(
            [NotNull] ProviderConventionSetBuilderDependencies dependencies,
            [NotNull] RelationalConventionSetBuilderDependencies relationalDependencies,
            [NotNull] ISqlGenerationHelper sqlGenerationHelper)
            : base(dependencies, relationalDependencies)
        {
            _sqlGenerationHelper = sqlGenerationHelper;
        }

        /// <summary>
        /// Builds and returns the convention set for the current database provider.
        /// </summary>
        /// <returns> The convention set for the current database provider. </returns>
        public override ConventionSet CreateConventionSet()
        {
            var conventionSet = base.CreateConventionSet();

            //var valueGenerationStrategyConvention = new ActianValueGenerationStrategyConvention(Dependencies, RelationalDependencies);
            //conventionSet.ModelInitializedConventions.Add(valueGenerationStrategyConvention);
            //conventionSet.ModelInitializedConventions.Add(
            //    new RelationalMaxIdentifierLengthConvention(128, Dependencies, RelationalDependencies));

            //ValueGenerationConvention valueGenerationConvention =
            //    new ActianValueGenerationConvention(Dependencies, RelationalDependencies);
            //var sqlServerIndexConvention = new ActianIndexConvention(Dependencies, RelationalDependencies, _sqlGenerationHelper);
            //ReplaceConvention(conventionSet.EntityTypeBaseTypeChangedConventions, valueGenerationConvention);
            //conventionSet.EntityTypeBaseTypeChangedConventions.Add(sqlServerIndexConvention);

            //var sqlServerInMemoryTablesConvention = new ActianMemoryOptimizedTablesConvention(Dependencies, RelationalDependencies);
            //conventionSet.EntityTypeAnnotationChangedConventions.Add(sqlServerInMemoryTablesConvention);
            //ReplaceConvention(
            //    conventionSet.EntityTypeAnnotationChangedConventions, (RelationalValueGenerationConvention)valueGenerationConvention);

            //ReplaceConvention(conventionSet.EntityTypePrimaryKeyChangedConventions, valueGenerationConvention);

            //conventionSet.KeyAddedConventions.Add(sqlServerInMemoryTablesConvention);

            //ReplaceConvention(conventionSet.ForeignKeyAddedConventions, valueGenerationConvention);

            //ReplaceConvention(conventionSet.ForeignKeyRemovedConventions, valueGenerationConvention);

            //conventionSet.IndexAddedConventions.Add(sqlServerInMemoryTablesConvention);
            //conventionSet.IndexAddedConventions.Add(sqlServerIndexConvention);

            //conventionSet.IndexUniquenessChangedConventions.Add(sqlServerIndexConvention);

            //conventionSet.IndexAnnotationChangedConventions.Add(sqlServerIndexConvention);

            //conventionSet.PropertyNullabilityChangedConventions.Add(sqlServerIndexConvention);

            //StoreGenerationConvention storeGenerationConvention =
            //    new ActianStoreGenerationConvention(Dependencies, RelationalDependencies);
            //conventionSet.PropertyAnnotationChangedConventions.Add(sqlServerIndexConvention);
            //ReplaceConvention(conventionSet.PropertyAnnotationChangedConventions, storeGenerationConvention);
            //ReplaceConvention(
            //    conventionSet.PropertyAnnotationChangedConventions, (RelationalValueGenerationConvention)valueGenerationConvention);

            //ConventionSet.AddBefore(
            //    conventionSet.ModelFinalizedConventions,
            //    valueGenerationStrategyConvention,
            //    typeof(ValidatingConvention));
            //ReplaceConvention(conventionSet.ModelFinalizedConventions, storeGenerationConvention);

            return conventionSet;
        }

        /// <summary>
        /// <para>
        ///     Call this method to build a <see cref="ConventionSet" /> for SQL Server when using
        ///     the <see cref="ModelBuilder" /> outside of <see cref="DbContext.OnModelCreating" />.
        /// </para>
        /// <para>
        ///     Note that it is unusual to use this method.
        ///     Consider using <see cref="DbContext" /> in the normal way instead.
        /// </para>
        /// </summary>
        /// <returns> The convention set. </returns>
        public static ConventionSet Build()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkActian()
                .AddDbContext<DbContext>(o => o.UseActian("Server=."))
                .BuildServiceProvider();

            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DbContext>())
                {
                    return ConventionSet.CreateConventionSet(context);
                }
            }
        }
    }
}
