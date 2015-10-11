namespace HyLibrary.ExtensionMethod
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HyLibrary.Lambda;

    public static class EnumerableExtension
    {
        /// <summary>
        /// 投影
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDest"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<TDest> Projection<TSource, TDest>(this IEnumerable<TSource> source)
        {
            return ProjectionExpression<TSource, TDest>.To(source);
        }

        /// <summary>
        /// 判断是否为空
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source == null || source.FirstOrDefault() == null;
        }

        /// <summary>
        /// 当集合为NULL时创建空集合
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> EmptyIfNull<TSource>(this IEnumerable<TSource> source)
        {
            return source == null ? Enumerable.Empty<TSource>() : source;
        }
    }
}