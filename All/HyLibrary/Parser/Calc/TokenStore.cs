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
        private class TokenReader
        {
            public char[] buffer;
            public int pos;

            public TokenReader(string str)
            {
                this.buffer = str.ToArray();
                this.pos = 0;
            }

            public char Read() { return buffer[pos++]; }

            public void Preview() { pos--; }

            public bool IsEnd()
            {
                return pos >= buffer.Length;
            }
        }

        public TokenStore(string source)
        {
            CodeCheck.NotNull(source, "source");

            var token = default(Token);
            var reader = new TokenReader(source);
            while ((token = GetToken(reader)).type != TokenType.End)
            {
                this.Add(token);
            }
        }

        private static Token GetToken(TokenReader reader)
        {
            char ch = default(char);
            do
            {
                if (reader.IsEnd())
                {
                    return new Token() { type = TokenType.End };
                }

                ch = reader.Read();
            } while (char.IsWhiteSpace(ch));

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
                    return new Token() { text = ch.ToString(), type = (TokenType)ch };

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
                    {
                        var sb = new StringBuilder();
                        sb.Append(ch);
                        while (!reader.IsEnd())
                        {
                            ch = reader.Read();
                            if (char.IsDigit(ch) || ch == '.')
                            {
                                sb.Append(ch);
                            }
                            else
                            {
                                reader.Preview();
                                break;
                            }
                        }

                        return new Token { type = TokenType.Number, text = sb.ToString() };
                    }

                default:
                    if (char.IsLetter(ch) || ch == '_')
                    {
                        // name
                        var sb = new StringBuilder();
                        sb.Append(ch);
                        while (!reader.IsEnd())
                        {
                            ch = reader.Read();
                            if (char.IsLetterOrDigit(ch) || ch == '_')
                            {
                                sb.Append(ch);
                            }
                            else
                            {
                                reader.Preview();
                                break;
                            }
                        }

                        return new Token() { text = sb.ToString(), type = TokenType.Name };
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