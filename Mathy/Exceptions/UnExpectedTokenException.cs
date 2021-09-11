using System;
using System.Collections.Generic;
using System.Text;

namespace Mathy.Exceptions
{
    public class UnExpectedTokenException : Exception
    {
        public UnExpectedTokenException(Token token) 
            : base($"Unexpected {token.Type} in line {token.Line}")
        {

        }
    }


}
