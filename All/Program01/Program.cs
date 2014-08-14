using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyLibrary.ExtensionMethod;

namespace Program01
{
    public class A
    {
        public int Id { get; set; }
    }

    class Program
    {
        public static void SetA(object x, object v)
        {
            ((A)x).Id = (int)v;
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

            Console.ReadKey();
        }
    }
}
