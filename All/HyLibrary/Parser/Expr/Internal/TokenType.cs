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
    internal enum TokenType
    {
        NONE = 0,
        COMMENT,
        COMMENT_BLOCK,
        TEXT,
        INT,
        UINT,
        LONG,
        ULONG,
        FLOAT,
        DOUBLE,
        DECIMAL,
        BOOL,
        IDENTIFIER,
        OPERATOR
    }
}