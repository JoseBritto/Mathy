using Mathy.TokenEvaluators;
using Mathy.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Mathy.Parsers;
using Xunit;

namespace Mathy.Tests
{
    public class PureExpressionEvaluatorTests
    {
        [Fact]
        public void DivideByZero_ShouldReturnInfinity()
        {
            var tokens = new List<Token>
            {
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 54540
                },
                new Token
                {
                    Type = TokenType.DIVIDE
                },
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 0
                }
            };

            var actual = PureExpressionTokenEvaluator.Evaluate(tokens, 10);

            Assert.Equal(double.PositiveInfinity, actual);


        }

        [Fact]
        public void EmptyBraces_ShouldThrow()
        {
            var tokens = new List<Token>
            {
                new Token
                {
                    Type = TokenType.OPENING_BRACES
                },
                new Token
                {
                    Type = TokenType.CLOSING_BRACES
                }
            };

            Assert.Throws<UnexpectedTokenException>(() => PureExpressionTokenEvaluator.Evaluate(tokens ,10));
        }


        [Theory]
        [MemberData(nameof(TestDataHelper.GetValidSampleDataForEvaluation), MemberType = typeof(TestDataHelper))]
        public void ValidOperation_ShouldCompute(List<Token> tokens, double expected)
        {
            var actual = PureExpressionTokenEvaluator.Evaluate(tokens, 10);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void PlusAndMinusInSeries_ShouldEvaluate()
        {
            var tokens = new List<Token>
            {
                new Token
                {
                    Type = TokenType.PLUS,
                },
                new Token
                {
                    Type = TokenType.PLUS,
                },
                new Token
                {
                    Type = TokenType.MINUS,
                },
                new Token
                {
                    Type = TokenType.PLUS,
                },
                new Token
                {
                    Type = TokenType.MINUS,
                },
                new Token
                {
                    Type = TokenType.PLUS,
                },
                new Token
                {
                    Type = TokenType.MINUS,
                },
                new Token
                {
                    Type = TokenType.PLUS,
                },
                new Token
                {
                    Type = TokenType.MINUS,
                },
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 5
                }
            };

            var actual = PureExpressionTokenEvaluator.Evaluate(tokens, 10);

            Assert.Equal(5, actual);
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
                    Type = TokenType.DIVIDE,
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
                    DoubleValue = 9
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
                    Type = TokenType.NUMBER,
                    DoubleValue = 2,
                },
                new Token
                {
                    Type = TokenType.MULTIPLY
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
