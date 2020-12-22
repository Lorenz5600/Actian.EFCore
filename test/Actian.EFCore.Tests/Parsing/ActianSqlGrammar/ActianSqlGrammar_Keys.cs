using FluentAssertions;
using Sprache;
using Xunit;
using static Actian.EFCore.Parsing.Internal.ActianSqlGrammar;

namespace Actian.EFCore.Tests.Parsing.ActianSqlGrammar
{
    public class ActianSqlGrammar_Keys
    {
        [Theory]
        [InlineData("(id)", new[] { "id" })]
        [InlineData("(\"æøå\", id)", new[] { "æøå", "id" })]
        public void Can_parse(string str, string[] expected)
        {
            Keys.End().Parse(str).Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("()")]
        [InlineData(@"(a\u0022bc, id)")]
        public void Can_not_parse(string str)
        {
            Keys.End().TryParse(str).WasSuccessful.Should().Be(false);
        }
    }
}
