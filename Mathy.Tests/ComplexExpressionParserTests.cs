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
            Assert.Throws<ParserException>(() => ComplexExpressionParser.Parse(testInput));
        }

        [Theory]
        [InlineData("1,23")]
        [InlineData("12300,0")]
        [InlineData(",.123")]
        [InlineData("001$")]
        [InlineData("0.0.1")]
        public void InvalidNumber_ShouldThrowException(string testInput)
        {
            Assert.Throws<ParserException>(() => ComplexExpressionParser.Parse(testInput));
        }

        [Theory]
        [InlineData("1.23")]
        [InlineData("123")]
        [InlineData(".123")]
        [InlineData("123.")]
        public void ValidNumber_ShouldParse(string input)
        {
            Assert.Equal(ComplexExpressionParser.Parse(input), new List<Token>()
            {
                new()
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = double.Parse(input, System.Globalization.CultureInfo.InvariantCulture)
                }
            });
        }
        
        [Theory]
        [MemberData(nameof(TestDataHelper.GetValidSampleDataForParsing), MemberType = typeof(TestDataHelper))]
        public void ValidInput_ShouldParse(string input,  List<Token> expectedTokens)
        {
            Assert.Equal<Token>(expectedTokens, ComplexExpressionParser.Parse(input));
        }

        [Fact]
        public void NumberAndLetter_ShouldAutoAddMultiplyOperator()
        {
            string input = "59.25_hello";
            var expected = new List<Token>
            {
                new Token()
                {
                    Type = TokenType.OPENING_BRACES
                },
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 59.25,
                },
                new Token
                {
                    Type = TokenType.MULTIPLY,
                },
                new Token
                {
                    Type = TokenType.IDENTIFIER,
                    StringValue = "_hello",
                },
                new Token()
                {
                    Type = TokenType.CLOSING_BRACES
                },
                new Token
                {
                    Type = TokenType.PLUS,
                },
                new Token()
                {
                    Type = TokenType.OPENING_BRACES
                },
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 12,
                },
                new Token
                {
                    Type = TokenType.MULTIPLY,
                },
                new Token
                {
                    Type = TokenType.IDENTIFIER,
                    StringValue = "ab",
                },
                new Token()
                {
                    Type = TokenType.CLOSING_BRACES
                },
            };

            var actual = ComplexExpressionParser.Parse(input);

            Assert.Equal(expected, actual);
        }


        [Theory]
        [MemberData(nameof(TestDataHelper.GetValidSampleDataWithSpacesForParsing), MemberType = typeof(TestDataHelper))]
        public void SpacesInExpression_ShouldWork(string input, List<Token> expectedTokens)
        {
            Assert.Equal<Token>(expectedTokens, ComplexExpressionParser.Parse(input));
        }
        
        [Theory]
        [InlineData("8 + '9'")]
        [InlineData("8 + 9.3.2")]
        [InlineData("abcd+haswrong$ymbol")]
        [InlineData("abcd+ haswrong$ymbol with space")]
        [InlineData("xyz.5")]
        public void InvalidInput_ShouldThrowException(string testInput)
        {
            Assert.Throws<ParserException>(() => ComplexExpressionParser.Parse(testInput));
        }

        
        [Fact]
        public void Equals_ShouldWork()
        {
            Assert.Equal(new List<Token>()
            {
                new Token()
                {
                    Type = TokenType.IDENTIFIER,
                    StringValue = "x"
                },
                new Token()
                {
                    Type = TokenType.EQUALS
                },
                new Token()
                {
                    Type = TokenType.MINUS
                },
                new Token()
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 9
                },
                new Token()
                {
                    Type = TokenType.DIVIDE
                },
                new Token()
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 5
                }
            },ComplexExpressionParser.Parse(" x =\t-9/5 "));
        }
    }
    
}
