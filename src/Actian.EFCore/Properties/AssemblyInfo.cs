using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Design;

[assembly: DesignTimeProviderServices("Actian.EFCore.Design.Internal.ActianDesignTimeServices")]
[assembly: InternalsVisibleTo("Actian.EFCore.FunctionalTests")]
[assembly: InternalsVisibleTo("Actian.EFCore.Tests")]
