using FluentAssertions;
using Sprache;
using Xunit;
using static Actian.EFCore.Parsing.Internal.ActianSqlGrammar;

namespace Actian.EFCore.Tests.Parsing.ActianSqlGrammar
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
            UndelimitedIdentifier.End().Parse(str).Should().Be(str);
        }

        [Theory]
        [InlineData("")]
        [InlineData("1table")]
        public void Can_not_parse(string str)
        {
            UndelimitedIdentifier.End().TryParse(str).WasSuccessful.Should().Be(false);
        }
    }
}
