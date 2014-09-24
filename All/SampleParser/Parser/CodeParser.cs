namespace SampleParser.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CodeParser
    {
        private Lexer lexer = new Lexer();

        public CodeParser()
        { 
        }

        public void Parser()
        {
            Token token;
            while ((token = lexer.Scan()) != null)
            {
                if (token.Tag == 65535)
                {
                    Console.WriteLine("end");
                    break;
                }

                Console.WriteLine(token);
            }
        }
    }
}