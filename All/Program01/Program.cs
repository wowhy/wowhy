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
    public class A
    {
        public int Id { get; set; }
        public string Name;
    }

    public struct B
    {
        public int Id { get; set; }
        public string Name;
    }

    class Program
    {
        public static object Set(object x, object v)
        {
            B b = (B)x;
            b.Id = (int)v;
            return b;
        }

        public static A StrongSetA(A x, int v)
        {
            x.Id = v;
            return x;
        }

        public static B StrongSetB(B x, string v)
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

            //var setter = ReflectionHelper.Instance.CreateMemberSetter<A, int>(typeof(A).GetProperty("Id"));
            //var obj = new A();

            //obj = setter(obj, 10);

            //Console.WriteLine(obj.Id);

            var fab = Recursion.R<int, int>((self, n) => n <= 2 ? 1 : self(n - 1) + self(n - 2));
            for (int i = 1; i <= 10; i++)
            {
                Console.Write(fab(i) + " ");
            }

            Console.ReadLine();
        }
    }
}
