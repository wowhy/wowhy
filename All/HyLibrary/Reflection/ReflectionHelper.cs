namespace HyLibrary.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Reflection.Emit;
    using System.Reflection;

    public class ReflectionHelper
    {
        public readonly static ReflectionHelper Instance = new ReflectionHelper();

        private static Type memberSetterType = typeof(MemberSetter);

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

        private ConcurrentDictionary<MemberInfo, MemberSetter> setterCaches = new ConcurrentDictionary<MemberInfo, MemberSetter>();

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

        public MemberSetter GetMemberSetter(MemberInfo member)
        {
            if (setterCaches.ContainsKey(member))
            {
                return setterCaches[member];
            }

            if (member is PropertyInfo)
            {
                return GetOrCreatePropertySetter((PropertyInfo)member);
            }

            if (member is FieldInfo)
            {
                return GetOrCreateFieldSetter((FieldInfo)member);
            }

            throw new NotSupportedException(member.GetType().FullName);
        }

        private static ObjectCreater CreateCreater(Type type)
        {
            var dm = default(DynamicMethod);
            var il = default(ILGenerator);
            if (type.IsClass)
            {
                dm = new DynamicMethod("test", type, null);
                il = dm.GetILGenerator();

                il.Emit(OpCodes.Newobj, type.GetConstructor(Type.EmptyTypes));
                il.Emit(OpCodes.Ret);
            }
            else
            {
                dm = new DynamicMethod("test", Types.Object, null);
                il = dm.GetILGenerator();

                var local = il.DeclareLocal(type);

                il.Emit(OpCodes.Ldloca_S, local);
                il.Emit(OpCodes.Initobj, type);
                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Box, type);
                il.Emit(OpCodes.Ret);
            }

            return (ObjectCreater)dm.CreateDelegate(typeof(ObjectCreater));
        }

        private MemberSetter GetOrCreatePropertySetter(PropertyInfo prop)
        {
            return this.setterCaches.GetOrAdd(
                prop,
                (key) =>
                {
                    var ownerType = prop.DeclaringType;
                    var method = prop.GetSetMethod();
                    if (method == null)
                        return null;

                    var dm = new DynamicMethod(prop.Name, null, new Type[] { Types.Object, Types.Object });
                    var il = dm.GetILGenerator();

                    if (ownerType.IsClass)
                    {
                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Castclass, ownerType);
                        il.Emit(OpCodes.Ldarg_1);
                        if (!prop.PropertyType.IsClass)
                            il.Emit(OpCodes.Unbox_Any, prop.PropertyType);
                        else
                            il.Emit(OpCodes.Castclass, prop.PropertyType);
                        il.Emit(OpCodes.Callvirt, method);
                    }
                    else
                    {
                        // 结构体
                        var local = il.DeclareLocal(ownerType);  // 声明变量
                        il.Emit(OpCodes.Ldarg_0);           // 压栈
                        il.Emit(OpCodes.Unbox_Any, ownerType);   // 拆箱
                        il.Emit(OpCodes.Stloc_0);           // 保存到局部变量
                        il.Emit(OpCodes.Ldloca_S, local);
                        il.Emit(OpCodes.Ldarg_1);
                        if (!prop.PropertyType.IsClass)
                            il.Emit(OpCodes.Unbox_Any, prop.PropertyType);
                        else
                            il.Emit(OpCodes.Castclass, prop.PropertyType);
                        il.Emit(OpCodes.Call, method);
                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Ldloc_0);
                        il.Emit(OpCodes.Box, ownerType);
                        il.Emit(OpCodes.Stind_Ref);
                    }

                    il.Emit(OpCodes.Ret);
                    return (MemberSetter)dm.CreateDelegate(memberSetterType);
                });
        }

        private MemberSetter GetOrCreateFieldSetter(FieldInfo field)
        {
            return this.setterCaches.GetOrAdd(
                field,
                (key) =>
                {
                    var ownerType = field.DeclaringType;
                    var dm = new DynamicMethod(field.Name, null, new Type[] { Types.Object, Types.Object });
                    var il = dm.GetILGenerator();

                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Castclass, ownerType);
                    if (ownerType.IsValueType)
                    {
                        il.Emit(OpCodes.Unbox, ownerType);
                    }

                    il.Emit(OpCodes.Ldarg_1);
                    if (field.FieldType.IsClass)
                        il.Emit(OpCodes.Castclass, field.FieldType);
                    else
                        il.Emit(OpCodes.Unbox_Any, field.FieldType);
                    il.Emit(OpCodes.Stfld, field);
                    il.Emit(OpCodes.Ret);

                    return (MemberSetter)dm.CreateDelegate(memberSetterType);
                });
        }
    }
}