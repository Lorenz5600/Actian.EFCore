using FluentAssertions;
using Sprache;
using Xunit;
using static Actian.EFCore.Parsing.Internal.ActianSqlGrammar;


namespace Actian.EFCore.Tests.Parsing.ActianSqlGrammar
{
    public class ActianSqlGrammar_DelimitedIdentifier
    {
        [Theory]
        [InlineData("\"æøå\"", "æøå")]
        [InlineData("\"1table\"", "1table")]
        [InlineData("\"this is a \"\"table\"\"\"", "this is a \"table\"")]
        public void Can_parse(string str, string expected)
        {
            DelimitedIdentifier.End().Parse(str).Should().Be(expected);
        }

        [Theory]
        [InlineData("\"\"")]
        [InlineData("a")]
        [InlineData("_1table")]
        [InlineData("a2")]
        public void Can_not_parse(string str)
        {
            DelimitedIdentifier.End().TryParse(str).WasSuccessful.Should().Be(false);
        }
    }
}
