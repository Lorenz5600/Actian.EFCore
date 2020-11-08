using Actian.EFCore.Scaffolding.Internal;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Migrations;
using Sprache;
using Xunit;

namespace Actian.EFCore.Tests.Scaffolding
{
    public class ActianForeignKeyConstraintParser_Action
    {
        [Theory]
        [InlineData("cascade", ReferentialAction.Cascade)]
        [InlineData("set null", ReferentialAction.SetNull)]
        [InlineData("set    null", ReferentialAction.SetNull)]
        [InlineData("restrict", ReferentialAction.Restrict)]
        [InlineData("no action", ReferentialAction.NoAction)]
        [InlineData("no    action", ReferentialAction.NoAction)]
        [InlineData("set default", ReferentialAction.SetDefault)]
        [InlineData("set    default", ReferentialAction.SetDefault)]
        public void Can_parse(string str, ReferentialAction expected)
        {
            var actual = ActianForeignKeyConstraintParser.Action.End().TryParse(str);
            actual.WasSuccessful.Should().Be(true, actual.Message);
            actual.Value.Should().Be(expected);
        }

        [Theory]
        [InlineData("cascad")]
        [InlineData("cascadee")]
        [InlineData("setnull")]
        [InlineData("noaction")]
        [InlineData("setdefault")]
        public void Can_not_parse(string str)
        {
            var actual = ActianForeignKeyConstraintParser.Action.End().TryParse(str);
            actual.WasSuccessful.Should().Be(false);
        }
    }
}
