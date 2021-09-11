using Mathy.Evaluators;
using Mathy.Parsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathy
{
    public class Mathy
    {
        
        public static double Compute(string input)
        {
           var tokens =  LineParser.Parse(input);

            var result = PureExpressionEvaluator.Evaluate(tokens);

            return result.DoubleValue;            
        }
    }


}
