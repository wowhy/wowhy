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
        public string Name { get; set; }
    }

    public class Program
    {
        static void Main(string[] argv)
        {
            //var exp1 = "10 + 20 + 30 + 40 + 50";
            //Console.WriteLine(new CalcParser(exp1).Compute());

            //var exp2 = "(10 + 20) * 100 - 20/5 + 1.2";
            //Console.WriteLine(new CalcParser(exp2).Compute());

            Expression<Func<Test>> lambda = () => new Test { Name = "100" };
            //Console.Write(lambda);

            Output(lambda);
        }

        public static void Output(Expression exp)
        {
            if (exp == null)
                return;

            var lambda = exp as LambdaExpression;
            if (lambda != null)
            {
                Output(lambda.Body);
            }

            var binary = exp as BinaryExpression;
            if (binary != null)
            {
                Console.WriteLine("left => ");
                Output(binary.Left);

                Console.WriteLine("right => ");
                Output(binary.Right);
            }

            var init = exp as MemberInitExpression;
            if (init != null)
            {
                Output(init.NewExpression);
            }

            Console.WriteLine(exp);
        }
    }
}