﻿/*
using Mathy.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Mathy.Parsers
{
    internal static class LineParser
    {
        // currently no functions like sin cos tan are supported
        public static List<Token> Parse(string line)
        {
            var tokens = new List<Token>();
            
            string[] words = line.Split(new[] { ' ', '\t' } , StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                try
                {
                    tokens.AddRange(ComplexExpressionParser.ParseExpression(words[i]));
                }
                catch (ParserException)
                {
                    throw;
                }
            }

            return tokens;
        }
        
    }
}
*/
