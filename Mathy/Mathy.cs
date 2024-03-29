﻿using Mathy.TokenEvaluators;
using Mathy.Parsers;

namespace Mathy
{
    public class Mathy
    {
        /// <summary>
        /// Compute simple expression without the use of any variables.
        /// </summary>
        /// <param name="input">The input</param>
        /// <returns></returns>
        public static double Compute(string input, int maxDecimals = 2)
        {
            var tokens = ComplexExpressionParser.Parse(input);

            var result = PureExpressionTokenEvaluator.Evaluate(tokens,maxDecimals);

            return result;
        }
    }


}
