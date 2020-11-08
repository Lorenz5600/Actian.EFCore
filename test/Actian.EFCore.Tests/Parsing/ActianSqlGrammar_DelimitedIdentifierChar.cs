using Actian.EFCore.Parsing.Internal;
using FluentAssertions;
using Sprache;
using Xunit;

namespace Actian.EFCore.Tests.Parsing
{
    public class ActianSqlGrammar_DelimitedIdentifierChar
    {
        [Theory]
        [InlineData("æ", 'æ')]
        [InlineData("\"\"", '"')]
        public void Can_parse(string str, char expected)
        {
            var actual = ActianSqlGrammar.DelimitedIdentifierChar.End().TryParse(str);
            actual.WasSuccessful.Should().Be(true, actual.Message);
            actual.Value.Should().Be(expected);
        }

        [Theory]
        [InlineData("")]
        [InlineData("\"")]
        public void Can_not_parse(string str)
        {
            var actual = ActianSqlGrammar.DelimitedIdentifierChar.End().TryParse(str);
            actual.WasSuccessful.Should().Be(false);
        }
    }
}
