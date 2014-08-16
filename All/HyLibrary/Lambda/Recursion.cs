namespace HyLibrary.Lambda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Lambda递归方法辅助类
    /// </summary>
    public static class Recursion
    {
        public static Func<T, TOut> R<T, TOut>(Func<Func<T, TOut>, T, TOut> func)
        {
            return x => func(R(func), x);
        }

        public static Func<T0, T1, TOut> R<T0, T1, TOut>(Func<Func<T0, T1, TOut>, T0, T1, TOut> func)
        {
            return (x0, x1) => func(R(func), x0, x1);
        }

        public static Func<T0, T1, T2, TOut> R<T0, T1, T2, TOut>(Func<Func<T0, T1, T2, TOut>, T0, T1, T2, TOut> func)
        {
            return (x0, x1, x2) => func(R(func), x0, x1, x2);
        }

        public static Func<T0, T1, T2, T3, TOut> R<T0, T1, T2, T3, TOut>(Func<Func<T0, T1, T2, T3, TOut>, T0, T1, T2, T3, TOut> func)
        {
            return (x0, x1, x2, x3) => func(R(func), x0, x1, x2, x3);
        }
    }
}