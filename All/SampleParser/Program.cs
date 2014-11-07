using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HyLibrary.ExtensionMethod;
using SampleParser.Parser;

namespace SampleParser
{
    class TestParser 
    {
        private int lookahead;

        public TestParser() 
        {
            lookahead = Console.Read();
        }

        public void Expr()
        {
            Term();
            while (true)
            {
                SkipWhiteSpace();
                if (lookahead == '+')
                {
                    Match('+'); Term(); Console.Write('+');
                }
                else if (lookahead == '-')
                {
                    Match('-'); Term(); Console.Write('-');
                }
                else
                {
                    return;
                }
            }
        }

        private void Term()
        {
            SkipWhiteSpace();
            if (char.IsDigit((char)lookahead))
            {
                Console.Write((char)lookahead); Match(lookahead);
                return;
            }

            throw new ApplicationException("syntax error");
        }

        private void Match(int t)
        {
            if (lookahead == t)
            {
                lookahead = Console.Read();
                return;
            }

            throw new ApplicationException("syntax error");
        }

        private void SkipWhiteSpace()
        {
            while (char.IsWhiteSpace((char)lookahead) && (char)lookahead != '\n')
            {
                Match(lookahead);
            }
        }
    }

    public class Program
    {
        public class A
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        static void Main(string[] argv)
        {
            var a = new A { Id = Guid.NewGuid(), Name = "test" };

            var count = 10000000;
            var method1 = Takes(() =>
            {
                var property = typeof(A).GetProperty("Name");
                for (var i = 0; i < count; i++)
                {
                    var tmp = property.GetValue(a);
                    if ((string)tmp != "test")
                    {
                        throw new ArgumentException();
                    }
                }
            });

            var method2 = Takes(() =>
            {
                var param = Expression.Parameter(typeof(A), "k");
                var property = Expression.PropertyOrField(param, "Name");

                var func = Expression.Lambda<Func<A, object>>(
                            Expression.Convert(property, typeof(object)), 
                            param).Compile();
                for (var i = 0; i < count; i++)
                {
                    var tmp = func(a);
                    if ((string)tmp != "test")
                    {
                        throw new ArgumentException();
                    }
                }
            });

            var method3 = Takes(() =>
            {
                for (var i = 0; i < count; i++)
                {
                    var tmp = a.Name;
                    if (tmp != "test")
                    {
                        throw new ArgumentException();
                    }
                }
            });

            Console.WriteLine("method1: {0}ms", method1);
            Console.WriteLine("method2: {0}ms", method2);
            Console.WriteLine("method3: {0}ms", method3);
        }

        private static long Takes(Action action)
        {
            var watch = new Stopwatch();
            watch.Start();

            action();

            return watch.ElapsedMilliseconds;
        }
    }
}