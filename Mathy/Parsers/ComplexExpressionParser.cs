using Mathy.Exceptions;
using System.Collections.Generic;
using System.Text;

namespace Mathy.Parsers
{
    internal static class ComplexExpressionParser
    {
        /// <summary>
        /// Expression can contain numbers, variables and operators only.
        /// </summary>
        /// <param name="input">Input expression without spaces</param>
        /// <returns><see langword="true"/> if successful</returns>
        /// <exception cref="ParserException"></exception>
        public static List<Token> ParseExpression(string input)
        {
            var tokens = new List<Token>();

            var numberCache = new StringBuilder();
            var identifierCache = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                if (identifierCache.Length == 0) //This means a character haven't appeared anywhere before this and we haven't hit a seperator yet.
                {
                    if (canBePartOfNumber(input[i]))
                    {
                        numberCache.Append(input[i]);
                        continue;
                    }

                    if (numberCache.Length != 0)
                    {
                        if (double.TryParse(numberCache.ToString(), out var number))
                            tokens.Add(new Token
                            {
                                Type = TokenType.NUMBER,
                                DoubleValue = number
                            });
                        else
                            throw new ParserException(numberCache.ToString(), 0);

                        //We should not yet clear the number cache. It should only be cleared if we hit a seperator
                        //If we cleared it now the identifiers starting witha number like 123xyz can't be identified.
                    }
                }

                // This needs to be changed for multi-char operators. For now 1 operator can only be 1 char in length.
                if (input[i].ToString().TryGetAsOperatorToken(out var token))
                {
                    // Since now we hit a seperator push the last identifier(if any) into the list
                    if (identifierCache.Length != 0)
                    {
                        tokens.Add(new Token
                        {
                            Type = TokenType.IDENTIFIER,
                            StringValue = identifierCache.ToString()
                        });
                        identifierCache.Clear();

                    }

                    //Here we can clear the number cache.
                    numberCache.Clear();

                    tokens.Add(token);
                    continue;
                }

                // This code shouldn't be hit if it was series of numbers cuz that means it would have hit a continue above.
                if (canBePartOfIdentifier(input[i]))
                {
                    if (numberCache.Length > 0) // This means that the identifier has started with a number. Like 12ab or 4521xyz
                    {
                        //So push the number cache into the tokens with a * at the end

                        if (double.TryParse(numberCache.ToString(), out var num))                            
                            tokens.Add(new Token
                            {
                                Type = TokenType.NUMBER,
                                DoubleValue = num
                            });
                        else
                            throw new ParserException(numberCache.ToString(), 0);

                        tokens.Add(new Token
                        {
                            Type = TokenType.TIMES
                        });
                    }

                    identifierCache.Append(input[i]);
                    continue;
                }

                // If code reaches here it means that someting must have went wrong.
                throw new ParserException(input, 0);
            }

            
            if(identifierCache.Length > 0)
            {
                // The last token was an identifier
                tokens.Add(new Token
                {
                    Type = TokenType.IDENTIFIER,
                    StringValue = identifierCache.ToString()
                });
                identifierCache.Clear();
            }
            else if(numberCache.Length > 0)
            {
                // The last token was an number
                if (double.TryParse(numberCache.ToString(), out var number))
                    tokens.Add(new Token
                    {
                        Type = TokenType.NUMBER,
                        DoubleValue = number
                    });
                else
                    throw new ParserException(numberCache.ToString(), 0);

                numberCache.Clear();
            }

            return tokens;
        }

        private static bool canBePartOfIdentifier(char c)
            => char.IsLetterOrDigit(c) || c == '_';


        private static bool canBePartOfNumber(char c)
        {
            return
                char.IsDigit(c) || c == '.';
        }
    }
}
