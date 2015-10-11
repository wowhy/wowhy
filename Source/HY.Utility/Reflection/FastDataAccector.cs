namespace HY.Utitily.Reflection
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class FastDataAccector
    {
        #region  核心代码
        private static ConcurrentDictionary<Type, Func<object>> creaters = new ConcurrentDictionary<Type,Func<object>>();
        private static ConcurrentDictionary<MemberInfo, Func<object, object>> getters = new ConcurrentDictionary<MemberInfo,Func<object,object>>();
        private static ConcurrentDictionary<MemberInfo, Action<object, object>> setters = new ConcurrentDictionary<MemberInfo, Action<object, object>>();

        public static object FastCreateInstance(Type type)
        {
            var creater = creaters.GetOrAdd(type, (key) => 
            {
                var constructor = type.GetConstructor(new Type[0]);
                var exp = Expression.New(constructor);
                return Expression.Lambda<Func<object>>(exp).Compile();
            });
            return creater();
        }

        public static void FastSetValue(Type type, MemberInfo member, object instance, object value)
        {
            var setter = setters.GetOrAdd(member, (key) => 
            {
                var memberType = member is FieldInfo ? ((FieldInfo)member).FieldType : ((PropertyInfo)member).PropertyType;

                var p1 = Expression.Parameter(typeof(object), "p1");
                var p2 = Expression.Parameter(typeof(object), "p2");

                var exp = Expression.Block(
                    Expression.Assign(
                        Expression.PropertyOrField(Expression.Convert(p1, type), member.Name), 
                        Expression.Convert(p2, memberType)));

                return Expression.Lambda<Action<object, object>>(exp, p1, p2).Compile();
            });

            setter(instance, value);
        }

        public static void FastSetValue(string name, object instance, object value)
        {
            var type = instance.GetType();
            var member = type.GetMember(name)[0];
            FastSetValue(type, member, instance, value);
        }

        public static object FastGetValue(Type type, MemberInfo member, object instance)
        {
            var getter = getters.GetOrAdd(member, new Func<MemberInfo,Func<object,object>>((k) =>
            {
                var p = Expression.Parameter(typeof(object), "p");
                return Expression.Lambda<Func<object, object>>(
                    Expression.Convert(Expression.PropertyOrField(Expression.Convert(p, type), member.Name), typeof(object)),
                    p).Compile();
            }));

            return getter(instance);
        }

        public static object FastGetValue(string name, object instance)
        {
            var type = instance.GetType();
            var member = type.GetMember(name)[0];
            return FastGetValue(type, member, instance);
        }
        #endregion

        public FastDataAccector(Type type)
            : this(type, FastCreateInstance(type))
        {
        }

        public FastDataAccector(object instance)
            : this(instance.GetType(), instance)
        {
        }

        public FastDataAccector(Type type, object instance)
        {
            this.Type = type;
            this.Instance = instance;
        }

        public Type Type { get; set; }

        public object Instance { get; private set; }

        public object this[string name]
        {
            get
            {
                return FastGetValue(name, this.Instance);
            }
            set
            {
                FastSetValue(name, this.Instance, value);
            }
        }

        public object Get(string name)
        {
            return FastGetValue(name, this.Instance);
        }

        public T Get<T>(string name)
        {
            return (T)FastGetValue(name, this.Instance);
        }

        public void Set(string name, object value)
        {
            FastSetValue(name, this.Instance, value);
        }
    }

    public class FastDataAccector<T> : FastDataAccector
    {
        public T TInstance 
        { 
            get 
            {
                return (T)base.Instance;
            } 
        }

        public FastDataAccector()
            : base(typeof(T))
        {
        }

        public FastDataAccector(T instance)
            : base(typeof(T), instance)
        {
        }
    }
}