using System.Collections.Generic;
using Actian.EFCore.TestUtilities;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore.Tests.TextTests
{
    public class TextNormalize
    {
        public TextNormalize(ITestOutputHelper testOutputHelper)
        {
            TestEnvironment.Log(this, testOutputHelper);
        }

        [Theory(DisplayName = "Normalize")]
        [MemberData(nameof(GetNormalizeTestCases))]
        public void Normalize(NormalizeTestCase test) => test.Test();

        public static IEnumerable<object[]> GetNormalizeTestCases()
        {
            yield return NormalizeTestCase.Create(
                "null should return empty string",
                null,
                ""
            );

            yield return NormalizeTestCase.Create(
                "empty string should return empty string",
                "",
                ""
            );

            yield return NormalizeTestCase.Create(
                "empty lines should return empty string",
                @"

     
             

                ",
                ""
            );


            yield return NormalizeTestCase.Create(
                "indents should be normalized",
                @"

                    select *
                      from table1

                     where a = 'b'
             

                ",
                @"select *
  from table1

 where a = 'b'"
            );
        }

        public class NormalizeTestCase
        {
            public static object[] Create(string displayName, string value, string expected)
            {
                return new object[] { new NormalizeTestCase(displayName, value, expected) };
            }

            private NormalizeTestCase(string displayName, string value, string expected)
            {
                DisplayName = displayName;
                Value = value;
                Expected = expected;
            }

            public string DisplayName { get; }
            public string Value { get; }
            public string Expected { get; }

            public void Test()
            {
                Text.Normalize(Value).Should().Be(Expected);
            }

            public override string ToString()
            {
                return DisplayName;
            }
        }
    }
}
