using System.Threading.Tasks;
using Mathy.Parsers;
using Mathy.TokenEvaluators;

namespace Mathy.Evaluators
{
    public class SimpleExpressionEvaluator
    {
        private readonly int _maxDecimals;

        /// <summary>
        /// Compute simple single line expression without the use of any variables.
        /// </summary>
        /// <param name="maxDecimals">The maximum number of decimals to be used in the result</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when maxDecimals is less than 0 or greater than 15</exception>
        public SimpleExpressionEvaluator(int maxDecimals = 5)
        {
            if (maxDecimals < 0)
                throw new System.ArgumentOutOfRangeException(nameof(maxDecimals),
                    "maxDecimals must be greater than or equal to 0");
            if(maxDecimals > 15)
                throw new System.ArgumentOutOfRangeException(nameof(maxDecimals),
                    "maxDecimals must be less than or equal to 15");
            _maxDecimals = maxDecimals;
        }
        
        public double Evaluate(string input)
        {
            var tokens = ComplexExpressionParser.Parse(input);
            var result = PureExpressionTokenEvaluator.Evaluate(tokens, _maxDecimals);
            return result;
        }
        
        public async Task<double> EvaluateAsync(string input)
        {
            return await Task.Run( () => Evaluate(input));
        }
    }
}