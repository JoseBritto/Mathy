using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Mathy.Parsers
{
    internal static class ParserHelperStringExtensions
    {
        public static bool TryGetAsNumberToken(this string input, out Token token)
        {
            if (double.TryParse(input, out var result))
            {
                token = new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = result
                };

                return true;
            }

            token = default;
            return false;

        }

        public static bool TryGetAsOperatorToken(this string input, out Token token)
        {
            //just for now
            if (input.Length != 1)
            {
                token = default;
                return false;
            }

            switch (input[0])
            {
                case '+':
                    token = new Token
                    {
                        Type = TokenType.PLUS,
                    };
                    return true;
                case '-':
                    token = new Token
                    {
                        Type = TokenType.MINUS
                    };
                    return true;
                case '*':
                    token = new Token
                    {
                        Type = TokenType.TIMES
                    };
                    return true;

                case '/':
                case '÷':
                    token = new Token
                    {
                        Type = TokenType.BY
                    };
                    return true;

                case '(':
                    token = new Token
                    {
                        Type = TokenType.OPENING_BRACES
                    };
                    return true;

                case ')':
                    token = new Token
                    {
                        Type = TokenType.CLOSING_BRACES
                    };
                    return true;

                case '=':
                    token = new Token
                    {
                        Type = TokenType.EQUALS
                    };
                    return true;

                default:
                    token = default;
                    return false;

            }
        }

        [Obsolete]
        public static bool TryGetAsKeywordToken(this string input, out Token token)
        {

            //There are no keywords!

            token = default;
            return false;

            /*switch (input)
            {
                case "let":
                    token = new Token
                    {
                        Type = TokenType.LET
                    };
                    return true;

            }

            token = default;
            return false;*/
        }

        private static Regex validIdentifier = new Regex(@"[_a-zA-Z][_a-zA-Z0-9]");

        public static bool IsValidIdentifier(this string input) 
            => string.IsNullOrWhiteSpace(input) == false && validIdentifier.Replace(input, "") == "";
    }
}
