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
            var exp = parser.Parse(
@"(int k) => 
{
    if(k > 10)
    {
        Console.WriteLine(k);
    }
    else
    {
        Console.WriteLine(k * 2);
    }
}");
            Console.WriteLine(exp.ToString());
            ((Action<int>)exp.Compile())(10);
        }
    }
}