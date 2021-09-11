﻿using Mathy.Evaluators;
using Mathy.Parsers;

namespace Mathy
{
    public class Mathy
    {
        /// <summary>
        /// Compute simple expression wiithout the use of any variables.
        /// </summary>
        /// <param name="input">The input</param>
        /// <returns></returns>
        public static double Compute(string input)
        {
            var tokens = LineParser.Parse(input);

            var result = PureExpressionEvaluator.Evaluate(tokens);

            return result;
        }
    }


}
