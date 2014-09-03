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
    public class Program
    {
        static void Main(string[] argv)
        {
            var exp1 = "10 + 20 + 30 + 40 + 50";
            Console.WriteLine(new CalcParser(exp1).Run());

            var exp2 = "(10 + 20) * 100 - 20/5 + 1.2";
            Console.WriteLine(new CalcParser(exp2).Run());
        }
    }
}