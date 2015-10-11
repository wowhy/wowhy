namespace HY.Utitily.ExtensionMethod
{
    using HY.Utitily.Reflection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class TypeExtension
    {
        public static bool IsDerivedFromType(this Type objectType, Type type)
        {
            CodeCheck.NotNull(objectType, "objectType");
            CodeCheck.NotNull(type, "type");

            return _IsDerivedFromType(objectType, type);
        }
        
        public static bool IsNullableType(this Type type)
        {
            CodeCheck.NotNull(type, "type");

            return type.IsGenericType && type.GetGenericTypeDefinition() == Types.Nullable;
        }

        public static bool IsEnumType(this Type type)
        {
            CodeCheck.NotNull(type, "type");
            return GetEnumType(type) != null;
        }

        public static bool IsPrimitiveType(this Type type)
        {
            var test = Nullable.GetUnderlyingType(type) ?? type;
            return test.IsPrimitive //
                || Types.String == test
                || Types.Decimal == test
                || Types.Guid == test
                || Types.DateTime == test
                || Types.DBNull == test
                || IsEnumType(test);
        }

        /// <summary>
        /// 得到枚举类型
        /// </summary>
        /// <param name="type">枚举类型或Nullable枚举类型</param>
        /// <returns>返回枚举类型</returns>
        public static Type GetEnumType(this Type type)
        {
            CodeCheck.NotNull(type, "type");

            var enumType = type;
            if (type.IsNullableType())
            {
                enumType = type.GetGenericArguments()[0];
            }

            return enumType.IsEnum ? enumType : default(Type);
        }

        private static bool _IsDerivedFromType(Type objectType, Type type)
        {
            if (objectType == null ||
                type == null)
            {
                return false;
            }

            if (objectType == type ||
                (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == type))
            {
                return true;
            }

            if (objectType.GetInterfaces().Any(x => x == type))
            {
                return true;
            }

            if (objectType.BaseType == null)
            {
                return false;
            }

            return _IsDerivedFromType(objectType.BaseType, type);
        }
    }
}