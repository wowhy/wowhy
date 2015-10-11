using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyLibrary.Reflection
{
    public delegate object ObjectCreater();

    public delegate TOut ObjectCreater<TOut>();
    public delegate TOut ObjectCreater<T1, TOut>(T1 v1);
    public delegate TOut ObjectCreater<T1, T2, TOut>(T1 v1, T2 v2);
    public delegate TOut ObjectCreater<T1, T2, T3, TOut>(T1 v1, T2 v2, T3 v3);
    public delegate TOut ObjectCreater<T1, T2, T3, T4, TOut>(T1 v1, T2 v2, T3 v3, T4 v4);
}
