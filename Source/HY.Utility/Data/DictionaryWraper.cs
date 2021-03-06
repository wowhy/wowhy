﻿namespace HY.Utitily.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class DictionaryWraper<TSource> : Dictionary<string, object>
    {
        private static readonly Type SourceType = typeof(TSource);
        private static Dictionary<string, Func<TSource, object>> Getters;

        static DictionaryWraper()
        {
            var properties = SourceType.GetProperties(
                        BindingFlags.Instance
                        | BindingFlags.Public
                        | BindingFlags.GetProperty
                        | BindingFlags.SetProperty);

            var param = Expression.Parameter(SourceType, "k");

            Getters = new Dictionary<string, Func<TSource, object>>();

            foreach (var property in properties)
            {
                var func = Expression.Lambda<Func<TSource, object>>(
                            Expression.Convert(
                                Expression.PropertyOrField(param, property.Name),
                                typeof(object)),
                            param).Compile();

                Getters.Add(property.Name, func);
            }
        }

        public DictionaryWraper(TSource source)
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
                dic.Add(getter.Key, getter.Value(source));
            }

            return dic;
        }

        public static IEnumerable<KeyValuePair<string, object>> Query(TSource source)
        {
            foreach (var getter in Getters)
            {
                yield return new KeyValuePair<string, object>(getter.Key, getter.Value(source));
            }
        }
    }
}
