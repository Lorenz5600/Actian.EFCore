using Xunit;

namespace Actian.EFCore
{
    public class SkippableFactAttribute: FactAttribute
    {
        public SkippableFactAttribute()
        {
            if (true)
            {
                Skip = "Ignored because it takes too long to complete...";
            }
        }
    }
}
