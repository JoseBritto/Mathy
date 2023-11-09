using System;
using System.Collections.Generic;

namespace Mathy.Tests;

public static class TestDataHelper
{
    public static IEnumerable<object[]> GetValidSampleDataForParsing()
    {
        yield return new object[]
        {
            "(5+5)2",
            new List<Token>()
            {
                new Token()
                {
                    Type = TokenType.OPENING_BRACES
                },
                new Token()
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 5
                },
                new Token()
                {
                    Type = TokenType.PLUS
                },
                new Token()
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 5
                },
                new Token()
                {
                    Type = TokenType.CLOSING_BRACES
                },
                new Token()
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 2
                }
            }
        };
        
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
        //(2)5
        yield return new object[]
        {
            new List<Token>()
            {
                new Token()
                {
                    Type = TokenType.OPENING_BRACES
                },
                new Token()
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 2
                },
                new Token()
                {
                    Type = TokenType.CLOSING_BRACES
                },
                new Token()
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 5
                }
            },
            10
        };
        
        //5(2)
        yield return new object[]
        {
            new List<Token>()
            {
                new Token()
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 5
                },
                new Token()
                {
                    Type = TokenType.OPENING_BRACES
                },
                new Token()
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 2
                },
                new Token()
                {
                    Type = TokenType.CLOSING_BRACES
                }
            },
            10
        };
        
        //(5+5)2
        yield return new object[]
        {
            new List<Token>()
            {
                new Token()
                {
                    Type = TokenType.OPENING_BRACES
                },
                new Token()
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 5
                },
                new Token()
                {
                    Type = TokenType.PLUS
                },
                new Token()
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 5
                },
                new Token()
                {
                    Type = TokenType.CLOSING_BRACES
                },
                new Token()
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 2
                }
            },
            20
        };
        
        //3+3(5+5)
        yield return new object[]
        {
            new List<Token>()
            {
                new Token()
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 3
                },
                new Token()
                {
                    Type = TokenType.PLUS
                },
                new Token()
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 3
                },
                new Token()
                {
                    Type = TokenType.OPENING_BRACES
                },
                new Token()
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 5
                },
                new Token()
                {
                    Type = TokenType.PLUS
                },
                new Token()
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 5
                },
                new Token()
                {
                    Type = TokenType.CLOSING_BRACES
                }
            },
            33
        };
        
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

    public static IEnumerable<object[]> GetValidSampleDataWithSpacesForParsing()
    {
        yield return new object[]
        {
            "5.2 * 8.1 / ab",
            new List<Token>()
            {
                new Token()
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 5.2
                },
                new Token()
                {
                    Type = TokenType.MULTIPLY
                },
                new Token()
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 8.1
                },
                new Token()
                {
                    Type = TokenType.DIVIDE
                },
                new Token()
                {
                    Type = TokenType.IDENTIFIER,
                    StringValue = "ab"
                }
            }
        };

        yield return new object[]
        {
            "12ab +2c",
            new List<Token>()
            {
                new Token()
                {
                    Type = TokenType.OPENING_BRACES
                },
                new Token()
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 12
                },
                new Token()
                {
                    Type = TokenType.MULTIPLY
                },
                new Token()
                {
                    Type = TokenType.IDENTIFIER,
                    StringValue = "ab"
                },
                new Token()
                {
                    Type = TokenType.CLOSING_BRACES
                },
                new Token()
                {
                    Type = TokenType.PLUS
                },
                new Token()
                {
                    Type = TokenType.OPENING_BRACES
                },
                new Token()
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = 2
                },
                new Token()
                {
                    Type = TokenType.MULTIPLY
                },
                new Token()
                {
                    Type = TokenType.IDENTIFIER,
                    StringValue = "c"
                },
                new Token()
                {
                    Type = TokenType.CLOSING_BRACES
                },
            }
        };

    }
}
    
