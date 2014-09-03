using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HyLibrary.ExtensionMethod;
using HyLibrary.Parser.Calc;
using HyLibrary.Parser.Expr;

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

    public class Sample
    {
        public int a;
        public int b;
        public int c;
    }

    public class Program
    {
        public static void Fuck(Sample k)
        {
            Console.WriteLine("fuck " + k.a);
        }

        static void Main(string[] argv)
        {
            //var exp = ExpressionParserHelper.ParseLambda<Test, bool>("Id > 10");
            //Console.WriteLine(exp.ToString());

            ExprParser.Using.Add("SampleParser");
            
            var parser = new ExprParser();
            var exp = parser.Parse(
@"(Sample k) => 
{
    Sample x = new Sample();
    x.a = 20;
    Console.WriteLine(k.a + k.b + k.c + x.a);
    Program.Fuck(x);
    return 10;
}");

            Console.WriteLine(exp.ToString());
            Console.WriteLine(((Func<Sample, int>)exp.Compile())(new Sample() { a = 10, b = 20, c = 30 }));
        }
    }
}