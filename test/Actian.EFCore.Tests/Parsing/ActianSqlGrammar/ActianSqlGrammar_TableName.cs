using FluentAssertions;
using Sprache;
using Xunit;
using static Actian.EFCore.Parsing.Internal.ActianSqlGrammar;

namespace Actian.EFCore.Tests.Parsing.ActianSqlGrammar
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
            TableName.End().Parse(str).Should().BeEquivalentTo((expectedSchema, expectedTable));
        }

        [Theory]
        [InlineData("()")]
        [InlineData("my schema.table")]
        [InlineData("my table")]
        public void Can_not_parse(string str)
        {
            TableName.End().TryParse(str).WasSuccessful.Should().Be(false);
        }
    }
}
