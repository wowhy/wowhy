namespace HyLibrary.Utility
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.InteropServices;

    public class ChangeTracker<TEntity, TDTO> : IEnumerable<KeyValuePair<string, ChangeValue>>
        where TEntity : class
        where TDTO : class
    {
        #region Static
        public static Type TrackType { get; private set; }

        public static Dictionary<string, Tuple<PropertyInfo, Func<TEntity, object>, Func<TDTO, object>>> Caches { get; private set; }

        static ChangeTracker()
        {
            var type = typeof(TEntity);
            var dto = typeof(TDTO).GetProperties().ToDictionary(x => x.Name);
            var properties = type.GetProperties(
                BindingFlags.Instance
                | BindingFlags.Public
                | BindingFlags.GetProperty
                | BindingFlags.SetProperty)
                .Where(k => SupportType(k.PropertyType))
                .Where(k => dto.ContainsKey(k.Name))
                .Select(k => new Tuple<PropertyInfo, PropertyInfo>(k, dto[k.Name]));

            var lambda = properties.Select(k =>
            {
                return new Tuple<string, PropertyInfo, Func<TEntity, object>, Func<TDTO, object>>(
                    k.Item1.Name,
                    k.Item1,
                    CreateLambda<TEntity>(k.Item1),
                    CreateLambda<TDTO>(k.Item2));
            });

            TrackType = type;
            Caches = lambda.ToDictionary(
                k => k.Item1,
                k => new Tuple<PropertyInfo, Func<TEntity, object>, Func<TDTO, object>>(k.Item2, k.Item3, k.Item4));
        }

        private static bool SupportType(Type type)
        {
            if (type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = type.GetGenericArguments()[0];
            }

            if (type.IsPrimitive ||
                type.IsEnum ||
                type == typeof(string) ||
                type == typeof(decimal) ||
                type == typeof(DateTime) ||
                type == typeof(TimeSpan) ||
                type == typeof(Guid))
            {
                return true;
            }

            return false;
        }

        private static Func<T, object> CreateLambda<T>(PropertyInfo property)
        {
            //var method = property.GetGetMethod();
            var param = Expression.Parameter(typeof(T), "k");
            var getter = Expression.PropertyOrField(param, property.Name);
            var cast = Expression.Convert(getter, typeof(object));
            return Expression.Lambda<Func<T, object>>(cast, param).Compile();
        }
        #endregion

        public ChangeTracker(TEntity origin, TDTO changed)
        {
            Contract.Assert(origin != null, "origin");
            Contract.Assert(changed != null, "changed");

            this.Origin = origin;
            this.Changed = changed;

            this.Properties = new List<string>();
            this.ChangeValues = new Dictionary<string, ChangeValue>();
        }

        public ChangeValue this[string property]
        {
            get
            {
                return this.ChangeValues[property];
            }
        }

        public TEntity Origin { get; protected set; }
        public TDTO Changed { get; protected set; }

        public int Count { get { return this.Properties.Count; } }
        public List<string> Properties { get; protected set; }

        protected Dictionary<string, ChangeValue> ChangeValues { get; set; }

        public IEnumerator<KeyValuePair<string, ChangeValue>> GetEnumerator()
        {
            return this.ChangeValues.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.ChangeValues.GetEnumerator();
        }

        public virtual void TrackValueChanged(params string[] filter)
        {
            if (this.Origin == this.Changed)
                return;

            var hashset = new HashSet<string>();
            if (filter != null && filter.Length > 0)
            {
                foreach (var property in filter.Distinct())
                {
                    hashset.Add(property);
                }
            }

            foreach (var cache in Caches)
            {
                var name = cache.Key;
                var property = cache.Value.Item1;

                if (hashset.Contains(name))
                {
                    continue;
                }

                var m1 = cache.Value.Item2;
                var m2 = cache.Value.Item3;

                var v1 = m1(this.Origin);
                var v2 = m2(this.Changed);

                if (v1 == null && v2 == null)
                {
                    continue;
                }

                if (Marshal.ReferenceEquals(v1, v2))
                {
                    continue;
                }

                if ((v1 == null || v2 == null)
                    || !object.Equals(v1, v2))
                {
                    this.Properties.Add(name);
                    this.ChangeValues[name] = new ChangeValue(v1, v2, property);
                }
            }
        }
    }

    public interface IChangeTracker<TEntity, TDTO>
        where TEntity : class
        where TDTO : class
    {
        ChangeTracker<TEntity, TDTO> Tracker { get; }
    }
}
