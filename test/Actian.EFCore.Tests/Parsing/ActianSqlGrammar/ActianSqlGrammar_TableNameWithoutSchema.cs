using FluentAssertions;
using Sprache;
using Xunit;
using static Actian.EFCore.Parsing.Internal.ActianSqlGrammar;

namespace Actian.EFCore.Tests.Parsing.ActianSqlGrammar
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
            TableNameWithoutSchema.End().Parse(str).Should().BeEquivalentTo(((string)null, expectedTable));
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
            TableNameWithoutSchema.End().TryParse(str).WasSuccessful.Should().Be(false);
        }
    }
}
