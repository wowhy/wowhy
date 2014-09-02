namespace HyLibrary.Parser.Expr
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    internal class JumpInfo
    {
        public LabelExpression breakLabel;

        public LabelExpression continueLabel;
    }
}
