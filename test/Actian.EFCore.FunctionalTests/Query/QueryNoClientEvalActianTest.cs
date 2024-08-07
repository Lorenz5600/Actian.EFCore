using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Actian.EFCore.Query
{
    public class QueryNoClientEvalActianTest : QueryNoClientEvalTestBase<NorthwindQueryActianFixture<NoopModelCustomizer>>
    {
        public QueryNoClientEvalActianTest(NorthwindQueryActianFixture<NoopModelCustomizer> fixture)
            : base(fixture)
        {
        }
    }
}
