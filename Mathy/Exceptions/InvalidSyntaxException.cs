using System;
using System.Collections.Generic;
using System.Text;

namespace Mathy.Exceptions
{
    public class InvalidSyntaxException : Exception
    {
        public InvalidSyntaxException(string word, int line) : base ($"Invalid syntax in line {line}. {word} can't be parsed.") {  }
    }
}
