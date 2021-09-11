using System;
using System.Collections.Generic;
using System.Text;

namespace Mathy.Exceptions
{
    public class MathException : Exception
    {
        public MathException(Exception exception, int line)
            : base($"An exception occured while evaluating line {line}" ,exception)
        {

        }
    }
}
