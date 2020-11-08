using Actian.EFCore.Parsing.Internal;
using FluentAssertions;
using Sprache;
using Xunit;

namespace Actian.EFCore.Tests.Parsing
{
    public class ActianSqlGrammar_Keys
    {
        [Theory]
        [InlineData("(id)", new[] { "id" })]
        [InlineData("(\"æøå\", id)", new[] { "æøå", "id" })]
        public void Can_parse(string str, string[] expected)
        {
            var actual = ActianSqlGrammar.Keys.End().TryParse(str);
            actual.WasSuccessful.Should().Be(true, actual.Message);
            actual.Value.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("()")]
        [InlineData(@"(a\u0022bc, id)")]
        public void Can_not_parse(string str)
        {
            var actual = ActianSqlGrammar.Keys.End().TryParse(str);
            actual.WasSuccessful.Should().Be(false);
        }
    }
}
