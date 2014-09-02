using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HyLibrary.Parser.Expr;
using HyLibrary.ExtensionMethod;

namespace SampleParser
{
    public class Test
    {
        public int Id { get; set; }

        public Test() { }

        public Test(int id) { this.Id = id; }

        public override string ToString()
        {
            return string.Format("{{ Id:{0} }}", this.Id);
        }
    }

    public class Program
    {
        static void Main(string[] argv)
        {
            ExprParser.Using.Add("SampleParser");

            var parser = new ExprParser();
            var exp = parser.Parse<Func<Test, bool>>(
@"(Test k) => 
{
    return k.Id > 1;
}");
            Console.WriteLine(exp.ToString());
            
            var list = new List<Test>() 
            {
                new Test { Id = 1 },
                new Test { Id = 2 },
                new Test { Id = 3 },
                new Test { Id = 4 },
            };

            var result = list.Where(exp.Compile()).Select(k => k.Id.ToString()).Join(',');
            Console.WriteLine(result);

            var exp2 = parser.Parse<Func<int, Test>>(@"(int id)=> new Test(id)");
            Console.WriteLine(exp2.ToString());
            Console.WriteLine(exp2.Compile()(10));
        }
    }
}