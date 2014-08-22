namespace Program01
{
    using HyLibrary.ExtensionMethod;
    using HyLibrary.Reflection.Emit;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class Program
    {
        static void Main(string[] args)
        {
            var builder = new AssemblyBuilderHelper(@"test.dll");
            builder.DefineType("Test");
            builder.Save();
        }
    }
}
