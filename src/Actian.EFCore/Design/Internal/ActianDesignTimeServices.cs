using Actian.EFCore.Diagnostics.Internal;
using Actian.EFCore.Scaffolding.Internal;
using Actian.EFCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Actian.EFCore.Design.Internal
{
    public class ActianDesignTimeServices : IDesignTimeServices
    {
        public virtual void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddEntityFrameworkActian();

#pragma warning disable EF1001 // Internal EF Core API usage.
            new EntityFrameworkRelationalDesignServicesBuilder(serviceCollection)
                .TryAdd<IAnnotationCodeGenerator, ActianAnnotationCodeGenerator>()
#pragma warning restore EF1001 // Internal EF Core API usage.
                .TryAdd<IDatabaseModelFactory, ActianDatabaseModelFactory>()
                .TryAdd<IProviderConfigurationCodeGenerator, ActianCodeGenerator>()
                .TryAddCoreServices();
        }
    }
}
