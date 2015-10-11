namespace HY.Utitily.Reflection
{
    using System;
    using System.Collections.Concurrent;
    using System.Reflection;
    using System.Reflection.Emit;

    public partial class ReflectionHelper
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

        private ConcurrentDictionary<MemberInfo, MemberGetter> getterCaches = new ConcurrentDictionary<MemberInfo, MemberGetter>();

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

        #region 快速实例化对象
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

        public ObjectCreater GetOrCreateObjectCreater(Type type)
        {
            return this.createrCaches.GetOrAdd(
                type,
                CreateCreater);
        }
        #endregion

        #region 快速对属性、字段赋值
        public MemberSetter GetOrCreateMemberSetter(MemberInfo member)
        {
            CodeCheck.NotNull(member, "member");

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

        public MemberSetter<TType, TMember> CreateMemberSetter<TType, TMember>(MemberInfo member)
        {
            CodeCheck.NotNull(member, "member");
            CodeCheck.IsTrue(member.DeclaringType == typeof(TType), "TType");

            if (member is PropertyInfo)
            {
                var prop = member as PropertyInfo;
                CodeCheck.IsTrue(prop.PropertyType == typeof(TMember), "TMember");
                return CreateProperySetter<TType, TMember>(prop);
            }

            if (member is FieldInfo)
            {
                var field = member as FieldInfo;
                CodeCheck.IsTrue(field.FieldType == typeof(TMember), "TMember");
                return CreateFieldSetter<TType, TMember>(field);
            }

            return null;
        }
        #endregion

        private static ObjectCreater CreateCreater(Type type)
        {
            var dm = default(DynamicMethod);
            var il = default(Emit.EmitHelper);
            if (type.IsClass)
            {
                dm = new DynamicMethod("test", type, null);
                il = new Emit.EmitHelper(dm.GetILGenerator());

                il.newobj(type.GetConstructor(Type.EmptyTypes))
                  .ret();
            }
            else
            {
                dm = new DynamicMethod("test", Types.Object, null);
                il = new Emit.EmitHelper(dm.GetILGenerator());

                var local = il.DeclareLocal(type);

                il.ldloca_s(local)
                  .initobj(type)
                  .ldloc_0
                  .box(type)
                  .ret();
            }

            return (ObjectCreater)dm.CreateDelegate(typeof(ObjectCreater));
        }

        #region 创建MemberSetter
        private MemberSetter GetOrCreatePropertySetter(PropertyInfo prop)
        {
            return this.setterCaches.GetOrAdd(
                prop,
                (key) =>
                {
                    return CreateProperySetter(prop);
                });
        }

        private MemberSetter GetOrCreateFieldSetter(FieldInfo field)
        {
            return this.setterCaches.GetOrAdd(
                field,
                (key) =>
                {
                    return CreateFieldSetter(field);
                });
        }

        private MemberSetter CreateProperySetter(PropertyInfo prop)
        {
            var ownerType = prop.DeclaringType;
            var method = prop.GetSetMethod();
            if (method == null)
                return null;

            var dm = new DynamicMethod(prop.Name, Types.Object, new Type[] { Types.Object, Types.Object });
            var il = new Emit.EmitHelper(dm.GetILGenerator());

            if (ownerType.IsClass)
            {
                il.ldarg_0
                  .castclass(ownerType)
                  .ldarg_1
                  .unbox_or_castclass(prop.PropertyType)
                  .callvirt(method)
                  .ldarg_0
                  .ret()
                  .end();
            }
            else
            {
                var local = il.DeclareLocal(ownerType);  // 声明变量
                il.ldarg_0
                  .unbox(ownerType)
                  .ldobj(ownerType)
                  .stloc_0
                  .ldloca_s(local)
                  .ldarg_1
                  .unbox_or_castclass(prop.PropertyType)
                  .call(method)
                  .ldloc_0
                  .box(ownerType)
                  .ret()
                  .end();
            }

            return (MemberSetter)dm.CreateDelegate(memberSetterType);
        }

        private MemberSetter<TType, TProperty> CreateProperySetter<TType, TProperty>(PropertyInfo prop)
        {
            var ownerType = prop.DeclaringType;
            var method = prop.GetSetMethod();
            if (method == null)
            {
                return null;
            }

            var dm = new DynamicMethod(prop.Name, ownerType, new Type[] { ownerType, prop.PropertyType });
            var il = new Emit.EmitHelper(dm.GetILGenerator());

            if (ownerType.IsClass)
            {
                il.ldarg_0
                  .ldarg_1
                  .callvirt(method)
                  .ldarg_0
                  .ret()
                  .end();
            }
            else
            {
                var local = il.DeclareLocal(ownerType);
                il.ldarga_s(local)
                  .ldarg_1
                  .call(method)
                  .ldarg_0
                  .ret()
                  .end();
            }

            return (MemberSetter<TType, TProperty>)dm.CreateDelegate(typeof(MemberSetter<TType, TProperty>));
        }

        private MemberSetter CreateFieldSetter(FieldInfo field)
        {
            var ownerType = field.DeclaringType;
            var dm = new DynamicMethod(field.Name, Types.Object, new Type[] { Types.Object, Types.Object });
            var il = new Emit.EmitHelper(dm.GetILGenerator());

            if (ownerType.IsClass)
            {
                il.ldarg_0
                  .castclass(ownerType)
                  .ldarg_1
                  .unbox_or_castclass(field.FieldType)
                  .stfld(field)
                  .ldarg_0
                  .ret()
                  .end();
            }
            else
            {
                var local = il.DeclareLocal(ownerType);  // 声明变量
                il.ldarg_0
                  .unbox(ownerType)
                  .ldobj(ownerType)
                  .stloc_0
                  .ldloca_s(local)
                  .ldarg_1
                  .unbox_or_castclass(field.FieldType)
                  .stfld(field)
                  .ldloc_0
                  .box(ownerType)
                  .ret()
                  .end();
            }

            return (MemberSetter)dm.CreateDelegate(memberSetterType);
        }

        private MemberSetter<TType, TField> CreateFieldSetter<TType, TField>(FieldInfo field)
        {
            var ownerType = field.DeclaringType;
            var dm = new DynamicMethod(field.Name, ownerType, new Type[] { ownerType, field.FieldType });
            var il = new Emit.EmitHelper(dm.GetILGenerator());

            if (ownerType.IsClass)
            {
                il.ldarg_0
                  .ldarg_1
                  .stfld(field)
                  .ldarg_0
                  .ret()
                  .end();
            }
            else
            {
                var local = il.DeclareLocal(ownerType);
                il.ldarga_s(local)
                  .ldarg_1
                  .stfld(field)
                  .ldarg_0
                  .ret()
                  .end();
            }

            return (MemberSetter<TType, TField>)dm.CreateDelegate(typeof(MemberSetter<TType, TField>));
        }
        #endregion

        #region 创建MemberGetter
        private MemberGetter GetOrCreatePropertyGetter(PropertyInfo porp)
        {
            throw new NotImplementedException();
        }

        private MemberGetter GetOrCreateFieldGetter(FieldInfo field)
        {
            throw new NotImplementedException();
        }

        private MemberGetter CreateProperyGetter(PropertyInfo porp)
        {
            throw new NotImplementedException();
        }

        private MemberGetter<TType, TProperty> CreateProperyGetter<TType, TProperty>(PropertyInfo porp)
        {
            throw new NotImplementedException();
        }

        private MemberGetter CreateFieldGetter(FieldInfo field)
        {
            throw new NotImplementedException();
        }

        private MemberGetter<TType, TField> CreateFieldGetter<TType, TField>(FieldInfo field)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}