namespace Program01
{
    using Microsoft.CSharp;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using HyLibrary;

    public struct Test 
    { 
        public int id;

        public override string ToString()
        {
            return "id = " + id.ToString();
        }
    }

    public interface IExecute { int Run(); }

    public class Program
    {
        private static string source =
@"
namespace Program01
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GenerateClass : IExecute
    {
        public int Run()
        {
            try
            {
                var list = new List<Test>();
                Console.WriteLine(list.Where(k=> k.id == 10).FirstOrDefault());
                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}
";

        static void Main(string[] args)
        {
            var provider = new CSharpCodeProvider();
            var parameters = new CompilerParameters()
            {
                GenerateInMemory = true
            };

            parameters.ReferencedAssemblies.AddRange(GetExecuteAssemblies());
            parameters.ReferencedAssemblies.Add("Program01.exe");

            var result = provider.CompileAssemblyFromSource(
                parameters,
                source);

            var type = result.CompiledAssembly.GetTypes()[0];
            var instance = (IExecute)Activator.CreateInstance(type);

            var ret = instance.Run();

            CodeCheck.IsTrue(ret == 1, "ret");
        }

        private static string[] GetExecuteAssemblies()
        {
            return Assembly.GetExecutingAssembly()
                .GetReferencedAssemblies()
                .Select(k => k.Name + ".dll")
                .ToArray();
        }
    }
}
