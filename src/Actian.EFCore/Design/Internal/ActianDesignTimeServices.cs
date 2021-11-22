using Actian.EFCore.Diagnostics.Internal;
using Actian.EFCore.Scaffolding.Internal;
using Actian.EFCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Actian.EFCore.Design.Internal
{
    public class ActianDesignTimeServices : IDesignTimeServices
    {
        public virtual void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
            => serviceCollection
                .AddSingleton<LoggingDefinitions, ActianLoggingDefinitions>()
                .AddSingleton<IRelationalTypeMappingSource, ActianTypeMappingSource>()
                .AddSingleton<IDatabaseModelFactory, ActianDatabaseModelFactory>()
                .AddSingleton<IProviderConfigurationCodeGenerator, ActianCodeGenerator>()
                .AddSingleton<IAnnotationCodeGenerator, ActianAnnotationCodeGenerator>();
    }
}
