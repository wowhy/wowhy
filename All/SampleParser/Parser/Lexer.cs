namespace SampleParser.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Lexer
    {
        private char peek = ' ';
        private char n1 = ' ';

        private Dictionary<string, Word> words = new Dictionary<string, Word>();

        public Lexer()
        {
            this.Line = 0;

            this.Reserve(new Word((int)KeyTag.TRUE, "true"));
            this.Reserve(new Word((int)KeyTag.FALSE, "false"));
        }

        public int Line { get; set; }

        public Token Scan()
        {
            // 跳过空白符
            this.SkipWhiteSpace();

            // 跳过注释
            this.SkipComments();

            if (char.IsDigit(peek))
            {
                var builder = new StringBuilder();
                do
                {
                    builder.Append(peek);
                    peek = (char)this.Read();
                }
                while (char.IsDigit(peek));

                return new Num(int.Parse(builder.ToString()));
            }

            if (char.IsLetter(peek) || peek == '_')
            {
                var builder = new StringBuilder();
                do
                {
                    builder.Append(peek);
                    peek = (char)this.Read();
                } while (char.IsLetterOrDigit(peek) || peek == '_');

                var s = builder.ToString();
                var word = default(Word);
                if (!this.words.TryGetValue(s, out word))
                {
                    word = new Word((int)KeyTag.ID, s);
                    this.words.Add(s, word);
                }

                return word;
            }

            if (Op.OpCodes.Contains(peek))
            {
                char code = peek;
                peek = ' ';
                return new Op(code);
            }
            
            var token = new Token(peek);
            peek = ' ';
            return token;
        }

        private void SkipWhiteSpace()
        {
            for (; ; peek = (char)this.Read())
            {
                if (peek == ' ' || peek == '\t' || peek == '\r')
                {
                    continue;
                }
                else if (peek == '\n') 
                {
                    Line += 1;
                }
                else
                {
                    break;
                }
            }
        }

        private void SkipComments()
        {
            if (peek == '/')
            {
                n1 = (char)this.Read();
                if (n1 == '/')
                {
                    peek = n1 = ' ';

                    while ((peek = (char)this.Read()) != '\r')
                    {
                        // nothing to do
                    }

                    SkipWhiteSpace();
                    SkipComments();
                }

                if (n1 == '*')
                {
                    peek = n1 = ' ';

                    while (true)
                    {
                        peek = (char)this.Read();

                        if (peek == '*')
                        {
                            n1 = (char)this.Read();
                            if (n1 == '/')
                            {
                                peek = n1 = ' ';
                                break;
                            }
                        }
                    }

                    SkipWhiteSpace();
                    SkipComments();
                }
            }
        }

        private int Read()
        {
            if (n1 != ' ')
            {
                var tmp = n1;
                n1 = ' ';
                return tmp;
            }

            return Console.Read();
        }

        private void Reserve(Word word)
        {
            words.Add(word.Lexeme, word);
        }
    }
}