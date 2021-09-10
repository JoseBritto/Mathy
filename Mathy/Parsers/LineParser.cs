using Mathy.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Mathy.Parsers
{
    public class LineParser
    {
        // currently no functions like sin cos tan are supported
        public static List<Token> Parse(string line)
        {
            var tokens = new List<Token>();

            string[] words = line.Split(new[] { ' ' } , StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {                
                if(words[i].TryGetAsKeywordToken( out var token))
                {
                    tokens.Add(token);
                    continue;
                }

                if (words[i].TryGetAsOperatorToken(out token))
                {
                    tokens.Add(token);
                    continue;
                }

                if(words[i].TryGetAsNumberToken(out token))
                {
                    tokens.Add(token);
                    continue;
                }

                if (words[i].IsValidIdentifier())
                {
                    tokens.Add(new Token
                    {
                        Type = TokenType.IDENTIFIER,
                        StringValue = words[i]
                    });
                    continue;
                }


                try
                {
                    tokens.AddRange(ComplexExpressionParser.ParseExpression(words[i]));
                }
                catch (InvalidSyntaxException)
                {
                    throw;
                }

            }

            return tokens;
        }
        
    }
}
