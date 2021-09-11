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

        EQUALS,
        PLUS,
        MINUS,
        TIMES,
        BY,
        RAISE_TO,

        OPENING_BRACES,
        CLOSING_BRACES,

        IDENTIFIER,
        NUMBER,

    }
}
