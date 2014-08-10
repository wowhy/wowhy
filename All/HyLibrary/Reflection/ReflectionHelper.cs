namespace HyLibrary.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Reflection.Emit;

    public class ReflectionHelper
    {
        public readonly static ReflectionHelper Instance = new ReflectionHelper();

        /// <summary>
        /// 程序集名称缓存
        /// </summary>
        private ConcurrentDictionary<Type, string> assemblyNameCaches = new ConcurrentDictionary<Type, string>();

        /// <summary>
        /// 类型Type缓存
        /// </summary>
        private ConcurrentDictionary<string, Type> typeCaches = new ConcurrentDictionary<string, Type>();

        /// <summary>
        /// 类型的构造函数缓存
        /// </summary>
        private ConcurrentDictionary<Type, ObjectCreater> createrCaches = new ConcurrentDictionary<Type, ObjectCreater>();

        public string GetTypeAssemblyName(Type type)
        {
            var val = string.Empty;
            if (this.assemblyNameCaches.TryGetValue(type, out val))
            {
                return val;
            }
            else
            {
                var name = type.AssemblyQualifiedName;
                this.assemblyNameCaches.TryAdd(type, name);
                return name;
            }
        }

        public Type GetType(string typeName)
        {
            return this.typeCaches.GetOrAdd(typeName, (key) => { return Type.GetType(key); });
        }

        public T FastCreateInstance<T>()
        {
            return (T)FastCreateInstance(typeof(T));
        }

        public object FastCreateInstance(Type type)
        {
            try
            {
                var creater = this.createrCaches.GetOrAdd(
                    type,
                    CreateCreater);
                return creater();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format("无法创建类型'{0}'的实例", type.FullName),
                    ex);
            }
        }

        public ObjectCreater GetObjectCreater(Type type)
        {
            return this.createrCaches.GetOrAdd(
                type,
                CreateCreater);
        }

        private static ObjectCreater CreateCreater(Type type)
        {
            var method = default(DynamicMethod);
            var il = default(ILGenerator);
            if (type.IsClass)
            {
                method = new DynamicMethod("test", type, null);
                il = method.GetILGenerator();

                il.Emit(OpCodes.Newobj, type.GetConstructor(Type.EmptyTypes));
                il.Emit(OpCodes.Ret);
            }
            else
            {
                method = new DynamicMethod("test", Types.Object, null);
                il = method.GetILGenerator();

                var local = il.DeclareLocal(type);

                il.Emit(OpCodes.Ldloca_S, local);
                il.Emit(OpCodes.Initobj, type);
                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Box, type);
                il.Emit(OpCodes.Ret);
            }

            return (ObjectCreater)method.CreateDelegate(typeof(ObjectCreater));
        }
    }
}