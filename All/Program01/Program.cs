namespace Program01
{
    using Microsoft.CSharp;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using HyLibrary;
    using System.Diagnostics.Contracts;
    using System.Collections;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class A
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime JoinDate { get; set; }
        public int Count { get; set; }
    }

    public class B
    {
        public string Name { get; set; }
    }

    public class ChangeValue
    {
        public object Origin { get; private set; }

        public object Value { get; private set; }

        public PropertyInfo PropertyInfo { get; private set; }

        public ChangeValue(object origin, object value, PropertyInfo propertyInfo)
        {
            this.Origin = origin;
            this.Value = value;
            this.PropertyInfo = propertyInfo;
        }
    }

    public class ChangeTracker<T> : IEnumerable<KeyValuePair<string, ChangeValue>>
        where T : class
    {
        #region Static
        public static Type TrackType { get; private set; }

        public static Dictionary<string, Tuple<PropertyInfo, Func<T, object>>> Caches { get; private set; }

        static ChangeTracker()
        {
            var type = typeof(T);
            var properties = type.GetProperties(
                BindingFlags.Instance
                | BindingFlags.Public
                | BindingFlags.GetProperty
                | BindingFlags.SetProperty)
                .Where(k => Support(k.PropertyType));

            var lambda = properties.Select(k =>
            {
                var method = k.GetGetMethod();
                var param = Expression.Parameter(type, "k");
                var getter = Expression.Property(param, method);
                var cast = Expression.Convert(getter, typeof(object));
                return new Tuple<string, PropertyInfo, Func<T, object>>(
                    k.Name,
                    k,
                    Expression.Lambda<Func<T, object>>(cast, param).Compile());
            });

            ChangeTracker<T>.TrackType = type;
            ChangeTracker<T>.Caches = lambda.ToDictionary(
                k => k.Item1,
                k => new Tuple<PropertyInfo, Func<T, object>>(k.Item2, k.Item3));
        }

        private static bool Support(Type type)
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
        #endregion

        public ChangeTracker(T origin, T changed)
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

        public T Origin { get; protected set; }
        public T Changed { get; protected set; }

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

        public virtual void TrackValueChanged()
        {
            if (this.Origin == this.Changed)
                return;

            foreach (var cache in Caches)
            {
                var name = cache.Key;
                var property = cache.Value.Item1;

                var method = cache.Value.Item2;
                var v1 = method(this.Origin);
                var v2 = method(this.Changed);

                //var v1 = property.GetValue(this.Origin);
                //var v2 = property.GetValue(this.Changed);

                if (!object.Equals(v1, v2))
                {
                    this.Properties.Add(name);
                    this.ChangeValues[name] = new ChangeValue(v1, v2, property);
                }
            }
        }
    }

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
                .Where(k => !Skip(k) && SupportType(k.PropertyType))
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

        private static bool Skip(PropertyInfo property)
        {
            switch (property.Name)
            {
                //case "Id":
                //case "CreatedById":
                //case "CreatedOn":
                //case "ModifiedById":
                //case "ModifiedOn":
                //case "IsTransient":
                //case "VersionNumber":
                //    return true;

                default:
                    return false;
            }
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
            var method = property.GetGetMethod();
            var param = Expression.Parameter(typeof(T), "k");
            var getter = Expression.Property(param, method);
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

        public virtual void TrackValueChanged()
        {
            if (this.Origin == this.Changed)
                return;

            foreach (var cache in Caches)
            {
                var name = cache.Key;
                var property = cache.Value.Item1;

                var m1 = cache.Value.Item2;
                var m2 = cache.Value.Item3;

                var v1 = m1(this.Origin);
                var v2 = m2(this.Changed);

                //var v1 = property.GetValue(this.Origin);
                //var v2 = property.GetValue(this.Changed);

                if (!object.Equals(v1, v2))
                {
                    this.Properties.Add(name);
                    this.ChangeValues[name] = new ChangeValue(v1, v2, property);
                }
            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            A a1 = new A { Id = Guid.Empty, Code = "XX", Name = "测试1", Count = 10, JoinDate = new DateTime(1999, 1, 1) };
            A a2 = new A { Id = Guid.Empty, Code = "XX", Name = "测试2", Count = 20, JoinDate = new DateTime(2000, 1, 1) };

            int count = 100000;
            Stopwatch watch = new Stopwatch();
            watch.Start();

            //for (int i = 0; i < count; i++)
            //{
            //    ChangeTracker<A> tracker = new ChangeTracker<A>(a1, a2);
            //    tracker.TrackValueChanged();
            //}

            //Parallel.Invoke(
            //    () =>
            //    {
            //        var tracker = new ChangeTracker<A>(new A() { Count = 1 }, new A() { Count = 2 });
            //        tracker.TrackValueChanged();
            //        Print(tracker);
            //    },
            //    () =>
            //    {
            //        var tracker = new ChangeTracker<B>(new B() { Name = "321" }, new B() { Name = "123" });
            //        tracker.TrackValueChanged();
            //        Print(tracker);
            //    });

            var tracker = new ChangeTracker<A, B>(a1, new B() { Name = "123" });
            tracker.TrackValueChanged();
            Print(tracker);


            Console.WriteLine(watch.ElapsedMilliseconds);

            // Print(tracker);
        }

        static void Print<T>(ChangeTracker<T> tracker)
            where T : class
        {
            foreach (var value in tracker)
            {
                Console.WriteLine("{0}: {1} ====> {2}",
                    value.Key, value.Value.Origin, value.Value.Value);
            }
        }

        static void Print<T1, T2>(ChangeTracker<T1, T2> tracker)
            where T1 : class
            where T2 : class
        {
            foreach (var value in tracker)
            {
                Console.WriteLine("{0}: {1} ====> {2}",
                    value.Key, value.Value.Origin, value.Value.Value);
            }
        }
    }
}