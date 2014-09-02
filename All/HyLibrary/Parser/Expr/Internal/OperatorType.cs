namespace HyLibrary.Parser.Expr
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    internal enum OperatorType
    {
        UNKNOWN = 0,
        OPEN,   // (,[,{
        CLOSE,  // ),],}
        PREFIX_UNARY,  // +,-,++,--
        POST_UNARY, // ++, --
        BINARY,  // +,-,*,/
        CONDITIONAL,    // (c)?x:y
        ASSIGN, // =, +=
        PRIMARY   // , ;
    }
}