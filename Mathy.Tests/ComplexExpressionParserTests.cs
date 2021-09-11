using Mathy.Exceptions;
using Mathy.Parsers;
using System.Collections.Generic;
using Xunit;

namespace Mathy.Tests
{
    public class ComplexExpressionParserTests
    {
        [Theory]
        [InlineData("123xa")]
        [InlineData("..3")]
        [InlineData("has$")]
        [InlineData("1xr")]
        [InlineData("123x++")]
        [InlineData("123+9+2x*9")]
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

        [Fact]
        public void ValidInput_ShouldParse1()
        {
            var tokens = getExpectedTokens();

            var input = "9/(-6969)+xtz32*9";

            Assert.Equal<Token>(tokens, ComplexExpressionParser.ParseExpression(input));
        }

        [Fact]
        public void ValidInput_ShouldParse2()
        {
            var tokens = getExpectedTokens();

            tokens.RemoveRange(8, 2);

            var input = "9/(-6969)+xtz32";

            Assert.Equal<Token>(tokens, ComplexExpressionParser.ParseExpression(input));
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

            };
        }
    }
}
