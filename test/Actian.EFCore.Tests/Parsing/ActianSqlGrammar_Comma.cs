using Actian.EFCore.Parsing.Internal;
using FluentAssertions;
using Sprache;
using Xunit;
using static Sprache.Parse;

namespace Actian.EFCore.Tests.Parsing
{
    public class ActianSqlGrammar_Comma
    {
        [Theory]
        [InlineData(",")]
        [InlineData("   ,")]
        [InlineData(",   ")]
        [InlineData("   ,   ")]
        public void Can_parse(string str)
        {
            var actual = ActianSqlGrammar.Comma.End().TryParse(str);
            actual.WasSuccessful.Should().Be(true, actual.Message);
            actual.Value.Should().Be(',');
        }

        [Theory]
        [InlineData("")]
        [InlineData("x")]
        [InlineData(" x  ,")]
        [InlineData(", x  ")]
        [InlineData(" x  , x  ")]
        public void Can_not_parse(string str)
        {
            var actual = ActianSqlGrammar.Comma.End().TryParse(str);
            actual.WasSuccessful.Should().Be(false);
        }
    }
}
