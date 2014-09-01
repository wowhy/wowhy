using Microsoft.CSharp;
using SampleParser.Parser;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SampleParser
{
    public class Program
    {
        static void Main(string[] argv)
        {
            var parser = new ExprParser();
            var exp = parser.Parse("(int k) => 2 * k");
            Console.WriteLine(exp.ToString());
            Console.WriteLine(((Func<int, int>)exp.Compile())(10));
        }
    }
}
