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
            for (; ; peek = (char)this.Read())
            {
                if (peek == ' ' || peek == '\t')
                    continue;
                else if (peek == '\n')
                    Line += 1;
                else
                    break;
            }

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

            if (char.IsLetter(peek))
            {
                var builder = new StringBuilder();
                do
                {
                    builder.Append(peek);
                    peek = (char)this.Read();
                } while (char.IsLetterOrDigit(peek));

                var s = builder.ToString();
                var word = default(Word);
                if (!this.words.TryGetValue(s, out word))
                {
                    word = new Word((int)KeyTag.ID, s);
                    this.words.Add(s, word);
                }

                return word;
            }

            var token = new Token(peek);
            peek = ' ';
            return token;
        }

        private int Read()
        {
            return Console.Read();
        }

        private void Reserve(Word word)
        {
            words.Add(word.Lexeme, word);
        }
    }
}