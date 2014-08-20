using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyLibrary.ExtensionMethod;
using HyLibrary.Reflection;
using HyLibrary.Lambda;

namespace Program01
{
    public interface IA { }
    public interface IB : IA { }
    public interface IC : IB { }

    public class Test : IC
    {
    }

    class Program
    {
        private static readonly Dictionary<string, Type> _cache = new Dictionary<string, Type>();

        internal static Type GetAppServiceInterfaceType(Type typeAppService)
        {
            string typeName = typeAppService.FullName;
            Type typeInterface = null;
            if (_cache.ContainsKey(typeName))
            {
                typeInterface = _cache[typeName];
            }
            else
            {
                foreach (Type type in typeAppService.GetInterfaces())
                {
                    Type t = type.GetInterface("IA");
                    if (t != null)
                    {
                        _cache.Add(typeName, type);
                        typeInterface = type;
                        break;
                    }
                }
            }
            return typeInterface;
        }

        static void Main(string[] args)
        {
            var type = typeof(Test);
            var interfaces = type.GetInterfaces();

            Console.WriteLine(GetAppServiceInterfaceType(type));

            Console.ReadLine();
        }
    }
}
