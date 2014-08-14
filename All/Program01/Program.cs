using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyLibrary.ExtensionMethod;
using HyLibrary.Reflection;

namespace Program01
{
    public class A
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public struct B
    {
        public int Id;
        public string Name { get; set; }
    }

    class Program
    {
        public static object SetA(object x, object v)
        {
            ((A)x).Id = (int)v;
            return x;
        }

        public static A StrongSetA(A x, string v)
        {
            x.Name = v;
            return x;
        }

        static void Main(string[] args)
        {
            //var list = new List<A>();
            //var count = 10000;
            //var type = typeof(A);
            //var setter = type.GetProperySetter("Id");
            //for (int i = 0; i < count; i++)
            //{
            //    list.Add(type.FastCreateInstance<A>());
            //    setter(list[i], i);
            //}

            var setter = ReflectionHelper.Instance.CreateMemberSetter<A, string>(typeof(A).GetProperty("Name"));
            var a = new A();

            setter(a, "Hello, World!");

            Console.WriteLine(a.Name);

            Console.ReadKey();
        }
    }
}
