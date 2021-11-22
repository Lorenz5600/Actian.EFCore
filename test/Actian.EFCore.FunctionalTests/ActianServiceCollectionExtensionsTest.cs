using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Actian.EFCore
{
    public class ActianServiceCollectionExtensionsTest : RelationalServiceCollectionExtensionsTestBase
    {
        public ActianServiceCollectionExtensionsTest(ITestOutputHelper testOutputHelper)
            : base(ActianTestHelpers.Instance)
        {
            TestEnvironment.Log(this, testOutputHelper);
        }

        public override void Repeated_calls_to_add_do_not_modify_collection()
        {
            base.Repeated_calls_to_add_do_not_modify_collection();
        }

        public override void Required_services_are_registered_with_expected_lifetimes()
        {
            base.Required_services_are_registered_with_expected_lifetimes();
        }
    }
}
