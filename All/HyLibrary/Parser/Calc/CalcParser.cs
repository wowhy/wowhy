namespace HyLibrary.Parser.Calc
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Calc
    {
        private StringReader reader;

        public Calc(string input)
        {
            this.reader = new StringReader(input);
        }

        private Token GetToken()
        {
            int ch;
            do
            {
                if ((ch = this.reader.Peek()) == -1)
                {
                    return Token.End;
                }
            } while (ch != '\n' && Char.IsWhiteSpace((char)ch));

            switch (ch)
            {
                case '\0':
                    this.reader.Read();
                    return Token.End;

                case ';':
                case '+':
                case '-':
                case '*':
                case '/':
                case '%':
                case '(':
                case ')':
                case '=':
                    return (Token)ch;

                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '.':
                    this.reader.Read();
                    return Token.Number;

                default:
                    throw new NotSupportedException();
            }
        }
    }
} 