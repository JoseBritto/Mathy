using Mathy.Evaluators;
using Mathy.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
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
                    Type = TokenType.BY
                },
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 0
                }
            };

            var actual = PureExpressionEvaluator.Evaluate(tokens);

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

            Assert.Throws<UnexpectedTokenException>(() => PureExpressionEvaluator.Evaluate(tokens));
        }


        [Fact]
        public void ValidOperation_ShouldCompute()
        {
            var tokens = getExpectedTokens();

            var actual = PureExpressionEvaluator.Evaluate(tokens);

            Assert.Equal(17, actual);
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

            var actual = PureExpressionEvaluator.Evaluate(tokens);

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
