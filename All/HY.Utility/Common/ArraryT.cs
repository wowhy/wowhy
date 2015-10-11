namespace HyLibrary.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Array<T>
    {
        public static readonly T[] Empty = new T[0];

        public static T[] New(int length)
        {
            return new T[length];
        }
    }
}