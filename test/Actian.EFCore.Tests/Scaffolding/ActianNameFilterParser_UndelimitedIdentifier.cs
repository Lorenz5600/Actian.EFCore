using Actian.EFCore.Scaffolding.Internal;
using FluentAssertions;
using Sprache;
using Xunit;

namespace Actian.EFCore.Tests.Scaffolding
{
    public class ActianNameFilterParser_UndelimitedIdentifier
    {
        [Theory]
        [InlineData("a")]
        [InlineData("_1table")]
        [InlineData("a2")]
        [InlineData("1table")]
        [InlineData("æøå")]
        [InlineData(@"a""")]
        public void Can_parse(string str)
        {
            var actual = ActianNameFilterParser.UndelimitedIdentifier.End().TryParse(str);
            actual.WasSuccessful.Should().Be(true, actual.Message);
            actual.Value.Should().Be(str);
        }

        [Theory]
        [InlineData("")]
        [InlineData(@"""a")]
        [InlineData(@"""a""")]
        public void Can_not_parse(string str)
        {
            var actual = ActianNameFilterParser.UndelimitedIdentifier.End().TryParse(str);
            actual.WasSuccessful.Should().Be(false);
        }
    }
}
