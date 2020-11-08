using Actian.EFCore.Parsing.Internal;
using FluentAssertions;
using Sprache;
using Xunit;

namespace Actian.EFCore.Tests.Parsing
{
    public class ActianSqlGrammar_UndelimitedIdentifier
    {
        [Theory]
        [InlineData("a")]
        [InlineData("_1table")]
        [InlineData("a2")]
        [InlineData("æøå")]
        public void Can_parse(string str)
        {
            var actual = ActianSqlGrammar.UndelimitedIdentifier.End().TryParse(str);
            actual.WasSuccessful.Should().Be(true, actual.Message);
            actual.Value.Should().Be(str);
        }

        [Theory]
        [InlineData("")]
        [InlineData("1table")]
        public void Can_not_parse(string str)
        {
            var actual = ActianSqlGrammar.UndelimitedIdentifier.End().TryParse(str);
            actual.WasSuccessful.Should().Be(false);
        }
    }
}
