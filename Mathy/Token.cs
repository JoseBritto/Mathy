using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Mathy
{
    public struct Token
    {
        public TokenType Type;

        public double DoubleValue;
        public string StringValue;

        public int Line;

        public override string ToString()
        {
            if (Type == TokenType.IDENTIFIER)
                return Type + " " + StringValue;
            else if (Type == TokenType.NUMBER)
                return Type + " " + DoubleValue;
            else
                return Type + "";
        }
    }

    

    public enum TokenType
    {
        EQUALS,
        PLUS,
        MINUS,
        MULTIPLY,
        DIVIDE,
        RAISE_TO,

        OPENING_BRACES,
        CLOSING_BRACES,

        IDENTIFIER,
        NUMBER,
    }
}
