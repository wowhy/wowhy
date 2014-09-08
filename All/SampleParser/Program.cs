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
        static void Main(string[] argv)
        {
            var lexer = new Lexer();
            var token = lexer.Scan();

            Console.WriteLine("token = {0}", token.ToString());
        }
    }
}