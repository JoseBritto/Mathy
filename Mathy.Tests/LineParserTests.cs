using Mathy.Exceptions;
using Mathy.Parsers;
using System.Collections.Generic;
using Xunit;

namespace Mathy.Tests
{
    public class LineParserTests
    {

        [Theory]
        [InlineData("x =9/ (-6969)+xtz32* 9")]
        [InlineData("x=9/(-6969)+\txtz32 *9")]
        [InlineData("x    = 9 /(  -6969)+xtz32*9")]
        [InlineData("x=9/(-6969)+xtz32*9")]
        public void ValidInput_ShouldParse(string input)
        {
            var tokens = getExpectedTokens();

            Assert.Equal<Token>(tokens, LineParser.Parse(input));
        }


        [Theory]
        [InlineData("8 + 9x")]
        [InlineData("8 + 9.3.2")]
        [InlineData("1+ 2k")]
        [InlineData("abcd+haswrong$ymbol")]
        [InlineData("abcd+ haswrong$ymbol with space")]
        [InlineData("xyz.5")]
        public void InvalidInput_ShouldThrowException(string testInput)
        {
            Assert.Throws<ParserException>(() => LineParser.Parse(testInput));
        }


        private List<Token> getExpectedTokens()
        {
            return new List<Token>
            {
                new Token
                {
                    Type = TokenType.IDENTIFIER,
                    StringValue = "x"
                },
                new Token
                {
                    Type = TokenType.EQUALS,
                },
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
