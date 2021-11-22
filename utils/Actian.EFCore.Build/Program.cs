using System;
using System.Threading.Tasks;

namespace Actian.EFCore.Build
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            return await new BuildContext().Run(args);
        }
    }
}
