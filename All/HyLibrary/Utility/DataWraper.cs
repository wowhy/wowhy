namespace HyLibrary.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class DataWraper<TSource> : Dictionary<string, object>
    {
        private static readonly Type SourceType = typeof(TSource);
        private static List<Tuple<string, Func<TSource, object>>> Getters;

        static DataWraper()
        {
            var properties = SourceType.GetProperties(
                        BindingFlags.Instance
                        | BindingFlags.Public
                        | BindingFlags.GetProperty
                        | BindingFlags.SetProperty);

            var param = Expression.Parameter(SourceType, "k");

            Getters = properties.Select(k =>
            {
                return new Tuple<string, Func<TSource, object>>(
                    k.Name,
                    Expression.Lambda<Func<TSource, object>>(
                        Expression.Convert(
                            Expression.PropertyOrField(param, k.Name),
                            typeof(object))).Compile());
            }).ToList();
        }

        public DataWraper(TSource source)
        {
            foreach (var item in Query(source))
            {
                this.Add(item.Key, item.Value);
            }
        }

        public static Dictionary<string, object> ToDictionary(TSource source)
        {
            var dic = new Dictionary<string, object>();
            foreach (var getter in Getters)
            {
                dic.Add(getter.Item1, getter.Item2(source));
            }

            return dic;
        }

        public static IEnumerable<KeyValuePair<string, object>> Query(TSource source)
        {
            foreach (var getter in Getters)
            {
                yield return new KeyValuePair<string, object>(getter.Item1, getter.Item2(source));
            }
        }
    }
}
