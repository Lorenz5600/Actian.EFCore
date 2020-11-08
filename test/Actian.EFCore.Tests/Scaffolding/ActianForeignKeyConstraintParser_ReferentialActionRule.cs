using Actian.EFCore.Scaffolding.Internal;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Migrations;
using Sprache;
using Xunit;

namespace Actian.EFCore.Tests.Scaffolding
{
    public class ActianForeignKeyConstraintParser_ReferentialActionRule
    {
        [Theory]
        [InlineData("on update cascade", ActianForeignKeyConstraintParser.RuleWhen.Update, ReferentialAction.Cascade)]
        [InlineData("on update set null", ActianForeignKeyConstraintParser.RuleWhen.Update, ReferentialAction.SetNull)]
        [InlineData("on update restrict", ActianForeignKeyConstraintParser.RuleWhen.Update, ReferentialAction.Restrict)]
        [InlineData("on update no action", ActianForeignKeyConstraintParser.RuleWhen.Update, ReferentialAction.NoAction)]
        [InlineData("on update set default", ActianForeignKeyConstraintParser.RuleWhen.Update, ReferentialAction.SetDefault)]
        [InlineData("on delete cascade", ActianForeignKeyConstraintParser.RuleWhen.Delete, ReferentialAction.Cascade)]
        [InlineData("on delete set null", ActianForeignKeyConstraintParser.RuleWhen.Delete, ReferentialAction.SetNull)]
        [InlineData("on delete restrict", ActianForeignKeyConstraintParser.RuleWhen.Delete, ReferentialAction.Restrict)]
        [InlineData("on delete no action", ActianForeignKeyConstraintParser.RuleWhen.Delete, ReferentialAction.NoAction)]
        [InlineData("on delete set default", ActianForeignKeyConstraintParser.RuleWhen.Delete, ReferentialAction.SetDefault)]
        public void Can_parse(string str, ActianForeignKeyConstraintParser.RuleWhen when, ReferentialAction action)
        {
            var actual = ActianForeignKeyConstraintParser.ReferentialActionRule.End().TryParse(str);
            actual.WasSuccessful.Should().Be(true, actual.Message);
            actual.Value.Should().Be((when, action));
        }

        [Theory]
        [InlineData("on insert cascade")]
        public void Can_not_parse(string str)
        {
            var actual = ActianForeignKeyConstraintParser.ReferentialActionRule.End().TryParse(str);
            actual.WasSuccessful.Should().Be(false);
        }
    }
}
