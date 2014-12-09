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
    public class ExprException : ApplicationException
    {
        public ExprException(string message) :
            base(message)
        {
        }

        public ExprException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}