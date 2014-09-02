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
using HyLibrary.Parser.Expr2;

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
            var exp = ExpressionParserHelper.ParseLambda<Test, bool>("Id > 10");
            Console.WriteLine(exp.ToString());
        }
    }
}