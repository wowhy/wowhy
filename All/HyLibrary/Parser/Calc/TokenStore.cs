namespace HyLibrary.Parser.Calc
{
    using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

    internal class TokenStore : List<Token>
    {
        public TokenStore(string source)
        {
            CodeCheck.NotNull(source, "source");

            using (var reader = new StringReader(source))
            {
                while (reader.Peek() != -1)
                {
                    this.Add(GetToken(reader));
                }
            }
        }

        private static Token GetToken(StringReader reader)
        {
            char ch = default(char);

            do
            {
                // skip whitespace
            } while (false);

            switch (ch)
            {
                case ';':
                case '+':
                case '-':
                case '*':
                case '/':
                case '%':
                case '(':
                case ')':
                case '=':
                    // operator
                    throw new NotImplementedException();

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
                    // number
                    throw new NotImplementedException();

                default:
                    if (char.IsLetter(ch) || ch == '_')
                    {
                        // name
                        throw new NotImplementedException();
                    }

                    throw new ArgumentException("错误的表达式");
            }

            throw new NotImplementedException();
        }
        
        //private static TokenType GetTokenType(StringReader reader)
        //{
        //    char ch = '1';
        //    switch (ch)
        //    {
        //        case ';':
        //        case '+':
        //        case '-':
        //        case '*':
        //        case '/':
        //        case '%':
        //        case '(':
        //        case ')':
        //        case '=':
        //            return (TokenType)str[0];

        //        case '0':
        //        case '1':
        //        case '2':
        //        case '3':
        //        case '4':
        //        case '5':
        //        case '6':
        //        case '7':
        //        case '8':
        //        case '9':
        //        case '.':
        //            return TokenType.Number;

        //        default:
        //            if (char.IsLetter(str[0]) ||
        //                str[0] == '_')
        //            {
        //                return TokenType.Name;
        //            }

        //            throw new NotSupportedException();
        //    }
        //}
    }
}