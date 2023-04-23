#nullable disable

namespace Bb.SqlServer.Asts
{
    using System;
    using Bb.Parsers;
    using Antlr4.Runtime;
    using Antlr4.Runtime.Tree;


    public partial class AstOnOff : AstTerminalKeyword
    {

        public static implicit operator AstOnOff(bool value)
        {
            if (value)
                return AstOnOff.On();
            return AstOnOff.Off();
        }


    }


    public partial class AstEnableDisable : AstTerminalKeyword
    {

        public static implicit operator AstEnableDisable(bool value)
        {
            if (value)
                return AstEnableDisable.Enable();
            return AstEnableDisable.Disable();
        }


    }


}
