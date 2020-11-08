using Actian.EFCore.Parsing.Internal;
using FluentAssertions;
using Sprache;
using Xunit;

namespace Actian.EFCore.Tests.Parsing
{
    public class ActianSqlGrammar_DelimitedIdentifier
    {
        [Theory]
        [InlineData("\"æøå\"", "æøå")]
        [InlineData("\"1table\"", "1table")]
        [InlineData("\"this is a \"\"table\"\"\"", "this is a \"table\"")]
        public void Can_parse(string str, string expected)
        {
            var actual = ActianSqlGrammar.DelimitedIdentifier.End().TryParse(str);
            actual.WasSuccessful.Should().Be(true, actual.Message);
            actual.Value.Should().Be(expected);
        }

        [Theory]
        [InlineData("\"\"")]
        [InlineData("a")]
        [InlineData("_1table")]
        [InlineData("a2")]
        public void Can_not_parse(string str)
        {
            var actual = ActianSqlGrammar.DelimitedIdentifier.End().TryParse(str);
            actual.WasSuccessful.Should().Be(false);
        }
    }
}
