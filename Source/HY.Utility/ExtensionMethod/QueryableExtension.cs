﻿namespace HY.Utitily.ExtensionMethod
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HY.Utitily.Lambda;

    public static class QueryableExtension
    {
        public static IQueryable<TDest> Projection<TSource, TDest>(this IQueryable<TSource> source)
        {
            return ProjectionExpression<TSource, TDest>.To(source);
        }
    }
}