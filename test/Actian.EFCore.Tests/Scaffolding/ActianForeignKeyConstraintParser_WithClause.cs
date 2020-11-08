using Actian.EFCore.Scaffolding.Internal;
using FluentAssertions;
using Sprache;
using Xunit;

namespace Actian.EFCore.Tests.Scaffolding
{
    public class ActianForeignKeyConstraintParser_WithClause
    {
        [Theory]
        [InlineData("with anything can come after with")]
        public void Can_parse(string str)
        {
            var actual = ActianForeignKeyConstraintParser.WithClause.End().TryParse(str);
            actual.WasSuccessful.Should().Be(true, actual.Message);
        }

        [Theory]
        [InlineData("withanything can come after with, but a space must come after with")]
        public void Can_not_parse(string str)
        {
            var actual = ActianForeignKeyConstraintParser.WithClause.End().TryParse(str);
            actual.WasSuccessful.Should().Be(false);
        }
    }
}
