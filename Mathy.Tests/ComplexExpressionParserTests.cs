using Mathy.Exceptions;
using Mathy.Parsers;
using System.Collections.Generic;
using Xunit;

namespace Mathy.Tests
{
    public class ComplexExpressionParserTests
    {
        [Theory]
        [InlineData("..3")]
        [InlineData("has$")]
        [InlineData("123+9+$$*9")]
        [InlineData("123+9+$$")]
        [InlineData("123x+$$")]
        public void InvalidIdentifier_ShouldThrowException(string testInput)
        {
            Assert.Throws<ParserException>(() => ComplexExpressionParser.ParseExpression(testInput));
        }

        [Theory]
        [InlineData("1,23")]
        [InlineData("12300,0")]
        [InlineData(",.123")]
        [InlineData("001$")]
        public void InvalidNumber_ShouldThrowException(string testInput)
        {
            Assert.Throws<ParserException>(() => ComplexExpressionParser.ParseExpression(testInput));
        }

        [Theory]
        [MemberData(nameof(TestDataHelper.GetValidSampleData), MemberType = typeof(TestDataHelper))]
        public void ValidInput_ShouldParse(string input,  List<Token> expectedTokens)
        {
            Assert.Equal<Token>(expectedTokens, ComplexExpressionParser.ParseExpression(input));
        }

        [Fact]
        public void NumberAndLetter_ShouldAutoAddMultiplyOperator()
        {
            string input = "59.25_hello";
            var expected = new List<Token>
            {
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 59.25,
                },
                new Token
                {
                    Type = TokenType.TIMES,
                },
                new Token
                {
                    Type = TokenType.IDENTIFIER,
                    StringValue = "_hello",
                },
            };

            var actual = ComplexExpressionParser.ParseExpression(input);

            Assert.Equal(expected, actual);
        }

        private List<Token> getExpectedTokens()
        {
            return new List<Token>
            {
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 9
                },
                new Token
                {
                    Type = TokenType.BY,
                },
                new Token
                {
                    Type = TokenType.OPENING_BRACES,
                },
                new Token
                {
                    Type = TokenType.MINUS,
                },
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 6969
                },
                new Token
                {
                    Type = TokenType.CLOSING_BRACES,
                },
                new Token
                {
                    Type = TokenType.PLUS,
                },
                new Token
                {
                    Type = TokenType.IDENTIFIER,
                    StringValue = "xtz32",
                },
                new Token
                {
                    Type = TokenType.TIMES
                },
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 9,
                },
                new Token
                {
                    Type = TokenType.RAISE_TO,
                },
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 5.25,
                },

            };
        }
    }
}
