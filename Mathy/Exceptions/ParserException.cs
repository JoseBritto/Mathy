using System;
using System.Collections.Generic;
using System.Text;

namespace Mathy.Exceptions
{
    public class ParserException : Exception
    {
        public ParserException(string word, int line) : base ($"Invalid token in line {line}. {word} can't be parsed.") {  }
    }
}
