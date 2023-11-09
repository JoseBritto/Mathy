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
        public static List<Token> Parse(string input)
        {
            var tokens = new List<Token>();

            var numberCache = new StringBuilder();
            var identifierCache = new StringBuilder();
            bool shouldAddBracketAfterIdentifier = false;
            
            for (int i = 0; i < input.Length; i++)
            {
                if (identifierCache.Length == 0) //This means a character haven't appeared anywhere before this and we haven't hit a seperator yet.
                {
                    // Loop until we grab all the digits of the number
                    if (canBePartOfNumber(input[i]))
                    {
                        numberCache.Append(input[i]);
                        continue;
                    }
                    // Now we have hit something that cant possibly be a number. So we should push the number (if any) captured as a token.
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
                if (input[i].ToString().TryGetAsOperatorToken(out var token) || char.IsWhiteSpace(input[i]))
                {
                    // Since now we hit a separator push the last identifier(if any) into the list
                    if (identifierCache.Length != 0)
                    {
                        AddIdentifierToTokens(tokens, identifierCache);
                    }

                    //Here we can clear the number cache.
                    numberCache.Clear();

                    //If the token is a whitespace we can skip it
                    if (!char.IsWhiteSpace(input[i]))
                        tokens.Add(token);
                    continue;
                }

                // This code shouldn't be hit if it was series of numbers cuz that means it would have hit a continue above.
                if (canBePartOfIdentifier(input[i]))
                {
                    if (numberCache.Length > 0) // This means that the identifier has started with a number. Like 12ab or 4521xyz
                    {
                        //So push a * to the tokens and wrap in brackets
                        //Since we know last token was a number we add '(' as the second last token
                        //If length is 1 (the number token at index 0) count - 1 would be 0 (where we insert '(') and the num would shift to index 1
                        tokens.Insert(tokens.Count - 1, new Token
                        {
                            Type = TokenType.OPENING_BRACES
                        });
                        //And add '*' as the last token
                        tokens.Add(new Token
                        {
                            Type = TokenType.MULTIPLY
                        });

                        numberCache.Clear();
                        shouldAddBracketAfterIdentifier = true;
                    }

                    identifierCache.Append(input[i]);
                    continue;
                }

                // If code reaches here it means that something must have went wrong.
                throw new ParserException(input, 0);
            }

            
            if(identifierCache.Length > 0)
            {
                AddIdentifierToTokens(tokens, identifierCache);
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

            void AddIdentifierToTokens(List<Token> tokensList, StringBuilder identifierCacheString)
            {
                // The last token was an identifier
                tokensList.Add(new Token
                {
                    Type = TokenType.IDENTIFIER,
                    StringValue = identifierCacheString.ToString()
                });
                identifierCacheString.Clear();
                if (shouldAddBracketAfterIdentifier)
                {
                    tokensList.Add(new Token()
                    {
                        Type = TokenType.CLOSING_BRACES
                    });
                    shouldAddBracketAfterIdentifier = false;
                }
            }
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
