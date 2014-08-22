namespace Program01
{
    using HyLibrary.ExtensionMethod;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class Program
    {
        public class A 
        {
            public string Name { get; set; }
        }

        static void Main(string[] args)
        {
            var a = typeof(A).FastCreateInstance<A>();
            typeof(A).GetProperySetter("Name")(a, "hello, world!");
            Console.WriteLine(a.Name);
        }
    }
}
