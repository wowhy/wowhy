namespace HyLibrary.ExtensionMethod
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HyLibrary.Lambda;

    public static class QueryableExtension
    {
        public static IEnumerable<TDest> Projection<TSource, TDest>(this IEnumerable<TSource> source)
        {
            return ProjectionExpression<TSource, TDest>.To(source);
        }

        public static IQueryable<TDest> Projection<TSource, TDest>(this IQueryable<TSource> source)
        {
            return ProjectionExpression<TSource, TDest>.To(source);
        }
    }
}