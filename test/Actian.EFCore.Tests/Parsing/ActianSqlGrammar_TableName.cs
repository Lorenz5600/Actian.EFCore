using Actian.EFCore.Parsing.Internal;
using FluentAssertions;
using Sprache;
using Xunit;

namespace Actian.EFCore.Tests.Parsing
{
    public class ActianSqlGrammar_TableName
    {
        [Theory]
        [InlineData("table", null, "table")]
        [InlineData("\"table\"", null, "table")]
        [InlineData("\"my table\"", null, "my table")]
        [InlineData("\"my \"\"table\"\"\"", null, "my \"table\"")]
        [InlineData("schema.table", "schema", "table")]
        [InlineData("\"schema\".\"table\"", "schema", "table")]
        [InlineData("\"my schema\".table", "my schema", "table")]
        [InlineData("\"my \"\"schema\"\"\".table", "my \"schema\"", "table")]
        public void Can_parse(string str, string expectedSchema, string expectedTable)
        {
            var actual = ActianSqlGrammar.TableName.End().TryParse(str);
            actual.WasSuccessful.Should().Be(true, actual.Message);
            actual.Value.Should().BeEquivalentTo((expectedSchema, expectedTable));
        }

        [Theory]
        [InlineData("()")]
        [InlineData("my schema.table")]
        [InlineData("my table")]
        public void Can_not_parse(string str)
        {
            var actual = ActianSqlGrammar.TableName.End().TryParse(str);
            actual.WasSuccessful.Should().Be(false);
        }
    }
}
