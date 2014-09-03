namespace Program01
{
    using Microsoft.CSharp;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public struct Test { public int id; }

    public interface IExecute { void Run(); }

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
        public void Run()
        {
            var list = new List<Test>();
            Console.WriteLine(list.Where(k=> k.id == 10));
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

            instance.Run();
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
