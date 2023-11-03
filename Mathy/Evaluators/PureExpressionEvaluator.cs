using Mathy.Exceptions;
using System;
using System.Collections.Generic;

namespace Mathy.Evaluators
{
    internal static class PureExpressionEvaluator
    {
        private const int NOT_FOUND = -1;

        // This doesnt support variables
        public static double Evaluate(List<Token> tokens, int maxDecimals)
        {            
            //Remove all braces
            while (tryExtractInBraces(tokens, out int start, out int end))
            {
                var result = Evaluate(tokens.GetRange(start, end - start + 1), maxDecimals);

                tokens.RemoveRange(start, end - start + 2); // Remove all things inside braces and the closing braces.

                // Overwirte the the opening braces with the result
                tokens[start - 1] = new Token
                {
                    Type = TokenType.NUMBER,
                    DoubleValue = result
                };
            }

            while (tryFindOperator(tokens, TokenType.RAISE_TO, false, false, out int index))
            {
                var op1 = tokens[index - 1];
                var op2 = tokens[index + 1];

                double result = pow(op1, op2);

                overwriteOperation(tokens, index, result);

            }

            while (tryFindOperator(tokens, TokenType.DIVIDE, false, false, out int index))
            {
                var op1 = tokens[index - 1];
                var op2 = tokens[index + 1];

                double result = divide(op1, op2);

                overwriteOperation(tokens, index, result);

            }

            while (tryFindOperator(tokens, TokenType.MULTIPLY, false, false, out int index))
            {
                var op1 = tokens[index - 1];
                var op2 = tokens[index + 1];

                double result = multiply(op1, op2);

                overwriteOperation(tokens, index, result);
            }

            // Now only + and -  operation is left.

            double answer = 0;
            int symbol = 1;

            // This however means that +++-+-+5 will be valid. ¯\_(ツ)_/¯
            foreach (var token in tokens)
            {
                if (token.Type == TokenType.PLUS)
                {
                    continue; // symbol * 1 = symbol                    
                }

                if (token.Type == TokenType.MINUS)
                {
                    symbol *= -1;
                    continue;                     
                }

                if (token.Type != TokenType.NUMBER)
                {
                    throw new UnexpectedTokenException(token);
                }

                answer += token.DoubleValue * symbol;
                symbol = 1;
            }

            // Check if the expression ended with a + or -
            if (tokens[tokens.Count - 1].Type != TokenType.NUMBER)
                throw new UnexpectedTokenException(tokens[tokens.Count - 1]);

            return Math.Round(answer, maxDecimals);

        }

        private static double pow(Token op1, Token op2)
        {
            if (op1.Type != TokenType.NUMBER)
                throw new UnexpectedTokenException(op1);

            if (op1.Type != TokenType.NUMBER)
                throw new UnexpectedTokenException(op2);
            try
            {
                double result = Math.Pow(op1.DoubleValue, op2.DoubleValue);

                return result;
            }
            catch (Exception e)
            {

                throw new MathException(e, op1.Line);
            }
        }

        private static double multiply(Token op1, Token op2)
        {
            if (op1.Type != TokenType.NUMBER)
                throw new UnexpectedTokenException(op1);

            if (op2.Type != TokenType.NUMBER)
                throw new UnexpectedTokenException(op1);

            double result;
            try
            {
                result = op1.DoubleValue * op2.DoubleValue;
            }
            catch (Exception e)
            {
                throw new MathException(e, op2.Line);
            }

            return result;
        }

        private static void overwriteOperation(List<Token> tokens, int operatorIndex, double result)
        {
            //Overwrite op1 with the result
            tokens[operatorIndex - 1] = new Token
            {
                Type = TokenType.NUMBER,
                DoubleValue = result
            };

            //Remove operator and op2
            tokens.RemoveRange(operatorIndex, 2);
        }

        private static double divide(Token op1, Token op2)
        {
            if (op1.Type != TokenType.NUMBER)
                throw new UnexpectedTokenException(op1);

            if (op2.Type != TokenType.NUMBER)
                throw new UnexpectedTokenException(op1);

            double result;
            try
            {
                result = op1.DoubleValue / op2.DoubleValue;
            }
            catch (Exception e)
            {
                throw new MathException(e, op2.Line);
            }

            return result;
        }

        private static bool tryFindOperator(List<Token> tokens, TokenType tokenType, bool allowAtStart, bool allowAtEnd, out int index)
        {
            index = NOT_FOUND;

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Type == tokenType)
                {
                    index = i;
                    
                    if (!allowAtStart && i == 0)
                        throw new UnexpectedTokenException(tokens[i]);
                    if (!allowAtEnd && i == tokens.Count - 1)
                        throw new UnexpectedTokenException(tokens[i]);
                    
                    break;
                }
            }

            if (index == NOT_FOUND)
                return false;

            return true;
        }


        /// <summary> <paramref name="startIndex"/> and <paramref name="endIndex"/> can be equal. 
        /// That means only one token is inside the braces. </summary>
        private static bool tryExtractInBraces(List<Token> tokens, out int startIndex, out int endIndex)
        {
            startIndex = endIndex = NOT_FOUND;

            for (int i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i];
                if (token.Type != TokenType.OPENING_BRACES && token.Type != TokenType.CLOSING_BRACES)
                    continue;
                if (token.Type == TokenType.OPENING_BRACES)
                {
                    if (endIndex != NOT_FOUND)
                    {
                        // This means a closing braces was found before which is unaceptable
                        // Also we don't need to check if endIndex is out of range cuz there is no way that kind of value can end up here.
                        throw new UnexpectedTokenException(tokens[endIndex]);
                    }
                    startIndex = i;
                    continue;
                }

                if (token.Type == TokenType.CLOSING_BRACES)
                {
                    if (startIndex == NOT_FOUND)
                    {
                        // Means no opening braces encountered before.
                        throw new UnexpectedTokenException(token);
                    }
                    endIndex = i;
                    break; // Since we found a pair we can exit out
                }
            }

            if (endIndex == NOT_FOUND && startIndex == NOT_FOUND)
            {
                return false;
            }

            if (endIndex == NOT_FOUND || startIndex == NOT_FOUND)
            {
                // Only one of them not found is invalid syntax
                throw new UnexpectedTokenException(endIndex != NOT_FOUND ? tokens[endIndex] : tokens[startIndex]);
            }

            if (endIndex == startIndex + 1)
            {
                // This means the input will be like this -> () Which is invalid
                throw new UnexpectedTokenException(tokens[endIndex]); // cuz technically the closing braces is the one in the wrong place.
            }

            // No check for end <= start cuz its handled in the loop

            endIndex--; startIndex++;   //Removing brackets

            return true;
        }
    }
}
