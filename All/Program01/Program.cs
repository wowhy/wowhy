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

    public class A
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime JoinDate { get; set; }
        public int Count { get; set; }
    }

    public class ChangeValue
    {
        public object Origin { get; private set; }

        public object Value { get; private set; }

        public Type ValueType { get; private set; }

        public ChangeValue(object origin, object value, Type valueType)
        {
            this.Origin = origin;
            this.Value = value;
            this.ValueType = valueType;
        }
    }

    public class ChangeTracker<T> : IEnumerable<KeyValuePair<string, ChangeValue>>
        where T : class
    {
        #region Static
        public static Type TrackType { get; private set; }

        public static Dictionary<string, PropertyInfo> Caches { get; private set; }

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
                return new object { };
            });

            ChangeTracker<T>.TrackType = type;
            ChangeTracker<T>.Caches = properties.ToDictionary(k => k.Name);
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

        public int Counts { get { return this.Properties.Count; } }
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
                var property = cache.Value;

                var v1 = property.GetValue(Origin);
                var v2 = property.GetValue(Changed);

                if (!object.Equals(v1, v2))
                {
                    this.Properties.Add(name);
                    this.ChangeValues[name] = new ChangeValue(v1, v2, property.PropertyType);
                }
            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Expression<Func<A,Guid>> lambda = (A k) => k.Id;


            A a1 = new A { Id = Guid.Empty, Code = "XX", Name = "测试1", Count = 10, JoinDate = new DateTime(1999, 1, 1) };
            A a2 = new A { Id = Guid.Empty, Code = "XX", Name = "测试2", Count = 20, JoinDate = new DateTime(2000, 1, 1) };


            int count = 100000;
            Stopwatch watch = new Stopwatch();
            watch.Start();

            for (int i = 0; i < count; i++)
            {
                ChangeTracker<A> tracker = new ChangeTracker<A>(a1, a2);
                tracker.TrackValueChanged();
            }

            Console.WriteLine(watch.ElapsedMilliseconds);

            //foreach (var value in tracker)
            //{
            //    Console.WriteLine("{0}: {1} ====> {2}",
            //        value.Key, value.Value.Origin, value.Value.Value);
            //}
        }
    }
}