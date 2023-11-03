using System.Collections.Generic;

namespace Mathy.Tests;

public  static class TestDataHelper
{
    public static IEnumerable<object[]> GetValidSampleDataForParsing()
    {
        yield return new object[]
        {
            "9/(-6969)+xtz32*9^5.25",
            new List<Token>
            {
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 9,
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
                    DoubleValue = 6969,
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
                    Type = TokenType.MULTIPLY,
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
            },
        };
        yield return new object[]
        {
            "59.25_hello",
            new List<Token>
            {
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
            },
        };

        yield return new object[]
        {
            "5.2*8.1/78*6+5.2+45+9",
            new List<Token>
            {
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 5.2,
                },
                new Token
                {
                    Type = TokenType.MULTIPLY,
                },
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 8.1,
                },
                new Token
                {
                    Type = TokenType.DIVIDE,
                },
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 78,
                },
                new Token
                {
                    Type = TokenType.MULTIPLY,
                },
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 6,
                },
                new Token
                {
                    Type = TokenType.PLUS,
                },
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 5.2,
                },
                new Token
                {
                    Type = TokenType.PLUS,
                },
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 45,
                },
                new Token
                {
                    Type = TokenType.PLUS,
                },
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 9,
                }
            }
        };
    }

    public static IEnumerable<object[]> GetValidSampleDataForEvaluation()
    {
        //17+45*2-9/3
        yield return new object[]
        { 
            new List<Token>()
            {
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 17,
                },
                new Token
                {
                    Type = TokenType.PLUS,
                },
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 45,
                },
                new Token
                {
                    Type = TokenType.MULTIPLY,
                },
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 2,
                },
                new Token
                {
                    Type = TokenType.MINUS,
                },
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 9,
                },
                new Token
                {
                    Type = TokenType.DIVIDE,
                },
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 3,
                },
            },
            104
        };
        
        //9/(-6969)*9^5.25
        yield return new object[]
        {
            new List<Token>()
            {
                new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 9,
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
                    DoubleValue = 6969,
                },
                new Token
                {
                    Type = TokenType.CLOSING_BRACES,
                },
                new Token
                {
                    Type = TokenType.MULTIPLY,
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
                }

            },
            132.0824814500 * -1
        };
    }
}
    
