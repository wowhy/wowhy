﻿namespace HyLibrary.ExtensionMethod
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using HyLibrary.Reflection;

    public static class ReflectionExtension
    {
        public static object FastCreateInstance(this Type type)
        {
            return ReflectionHelper.Instance.FastCreateInstance(type);
        }

        public static T FastCreateInstance<T>(this Type type)
        {
            return ReflectionHelper.Instance.FastCreateInstance<T>();
        }

        public static string GetAssemblyName(this Type type)
        {
            return ReflectionHelper.Instance.GetTypeAssemblyName(type);
        }

        public static MemberSetter GetMemberSetter(this MemberInfo member)
        {
            return ReflectionHelper.Instance.GetOrCreateMember(member);
        }

        public static MemberSetter GetProperySetter(this Type type, string propName)
        {
            return ReflectionHelper.Instance.GetOrCreateMember(type.GetProperty(propName));
        }

        public static MemberSetter GetFieldSetter(this Type type, string fieldName)
        {
            return ReflectionHelper.Instance.GetOrCreateMember(type.GetField(fieldName));
        }
    }
}