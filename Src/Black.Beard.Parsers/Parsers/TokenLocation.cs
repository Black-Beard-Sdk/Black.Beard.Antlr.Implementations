using Antlr4.Runtime;

namespace Bb.Parsers
{




    [System.Diagnostics.DebuggerDisplay("{StartIndex}, {StopIndex} : ({Line},{Column})")]
    public class TokenLocation
    {

        public TokenLocation()
        {

        }

        public TokenLocation(int start, int end)
        {
            StartIndex = start;
            StopIndex = end;
        }

        public TokenLocation(int start, int end, int line, int column)
        {
            StartIndex = start;
            StopIndex = end;
            Line = line;
            Column = column;
        }

        public TokenLocation(Antlr4.Runtime.IToken token)
        {
            if (token != null)
            {
                Line = token.Line;
                Column = token.Column;
                StartIndex = token.StartIndex;
                StopIndex = token.StopIndex;
            }
        }

        //public TokenLocation(LocationResult location)
        //{
        //    Line = location.StartLine;
        //    Column = location.StartColumn;
        //    StartIndex = location.StartCharacter;
        //    StopIndex = location.EndCharacter;
        //}

        public int Line { get; internal set; }

        public int Column { get; internal set; }

        public int StartIndex { get; internal set; }

        public int StopIndex { get; internal set; }

        public string Function { get; set; }
        public string ScriptFile { get; internal set; }

        public TokenLocation Clone()
        {
            return new TokenLocation(StartIndex, StopIndex, Line, Column) { Function = Function };
        }

    }

    public static class TokenLocationExtension
    {

        public static TokenLocation ToLocation(this IToken self)
        {
            return new TokenLocation(self);
        }

    }

}
