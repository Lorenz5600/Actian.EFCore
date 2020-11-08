using Actian.EFCore.Parsing.Internal;
using FluentAssertions;
using Sprache;
using Xunit;

namespace Actian.EFCore.Tests.Parsing
{
    public class ActianSqlGrammar_TableNameWithoutSchema
    {
        [Theory]
        [InlineData("table", "table")]
        [InlineData("\"table\"", "table")]
        [InlineData("\"my table\"", "my table")]
        [InlineData("\"my \"\"table\"\"\"", "my \"table\"")]
        public void Can_parse(string str, string expectedTable)
        {
            var actual = ActianSqlGrammar.TableNameWithoutSchema.End().TryParse(str);
            actual.WasSuccessful.Should().Be(true, actual.Message);
            actual.Value.Should().BeEquivalentTo(((string)null, expectedTable));
        }

        [Theory]
        [InlineData("()")]
        [InlineData("my schema.table")]
        [InlineData("schema.table")]
        [InlineData("\"schema\".\"table\"")]
        [InlineData("\"my schema\".table")]
        [InlineData("\"my \"\"schema\"\"\".table")]
        public void Can_not_parse(string str)
        {
            var actual = ActianSqlGrammar.TableNameWithoutSchema.End().TryParse(str);
            actual.WasSuccessful.Should().Be(false);
        }
    }
}
