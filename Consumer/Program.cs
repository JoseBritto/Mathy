using Mathy.Parsers;
using System;
using Mathy.Evaluators;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var expression = Console.ReadLine();
                var evaluator = new SimpleExpressionEvaluator();
                try
                {
                    //var tokens = LineParser.Parse(expression);

                    /*foreach (var token in tokens)
                    {
                        Console.Write(token.Type + "\t");
                        Console.Write(token.StringValue ?? token.DoubleValue.ToString());
                        Console.WriteLine();
                    }*/
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                try
                {
                    var result = evaluator.Evaluate(expression);

                    Console.WriteLine(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
            
        }
    }
}
