using System;
using System.Collections.Generic;
using System.Text;

namespace Mathy.Exceptions
{
    public class UnexpectedTokenException : Exception
    {
        public UnexpectedTokenException(Token token) 
            : base($"Unexpected {token.Type} in line {token.Line}")
        {

        }
    }


}
