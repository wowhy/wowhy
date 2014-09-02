namespace HyLibrary.Parser.Expr
{
    using System.Linq.Expressions;
    using System.Reflection;

    internal delegate Expression d1m(Expression exp, MethodInfo mi);

    internal delegate Expression d2(Expression exp1, Expression exp2);

    internal delegate Expression d2m(Expression exp1, Expression exp2, MethodInfo mi);

    internal delegate Expression d2bm(Expression exp1, Expression exp2, bool LiftToNull, MethodInfo mi);
}