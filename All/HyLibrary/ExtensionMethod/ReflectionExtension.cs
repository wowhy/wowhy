namespace HyLibrary.ExtensionMethod
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
    }
}
