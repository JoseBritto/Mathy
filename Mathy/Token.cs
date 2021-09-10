using System;
using System.Collections.Generic;
using System.Text;

namespace Mathy
{
    public struct Token
    {
        public TokenType Type;

        public double DoubleValue;
        public string StringValue;

        public int Line;
    }

    

    public enum TokenType
    {
        LET,

        EQUALS,
        PLUS,
        MINUS,
        TIMES,
        BY,

        OPENING_BRACES,
        CLOSING_BRACES,

        IDENTIFIER,
        NUMBER
    }
}
