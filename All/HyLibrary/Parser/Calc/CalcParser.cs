namespace HyLibrary.Parser.Calc
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CalcParser
    {
        private TokenStore store;
        private int pos;
        private decimal result;

        public CalcParser(string source)
        {
            this.pos = 0;
            this.store = new TokenStore(source);
        }

        internal Token CurrentToken
        {
            get
            {
                return this.store[this.pos];
            }
        }

        public decimal Compute()
        {
            if (!this.IsEnd())
            {
                this.result = this.Expr(false);
            }

            return this.result;
        }

        private decimal Expr(bool get)
        {
            var left = this.Term(get);
            while (!this.IsEnd())
            {
                switch (this.CurrentToken.type)
                {
                    case TokenType.Plus:
                        left += this.Term(true);
                        break;

                    case TokenType.Minus:
                        left -= this.Term(true);
                        break;

                    default:
                        return left;
                }
            }

            return left;
        }

        private decimal Term(bool get)
        {
            var left = Prim(get);
            while (!this.IsEnd())
            {
                switch (this.CurrentToken.type)
                {
                    case TokenType.Mul:
                        left *= Prim(true);
                        break;

                    case TokenType.Div:
                        left /= Prim(true);
                        break;

                    default:
                        return left;
                }
            }

            return left;
        }

        private decimal Prim(bool get)
        {
            if (get)
            {
                this.Next();
            }

            decimal left;
            switch (this.CurrentToken.type)
            {
                case TokenType.Number:
                    left = decimal.Parse(this.CurrentToken.text);
                    this.Next();
                    return left;

                case TokenType.Minus:
                    return -Prim(true);

                case TokenType.Name:
                    left = this.Table(this.CurrentToken.text);
                    return left;

                case TokenType.LP:
                    left = this.Expr(true);
                    if (this.CurrentToken.type != TokenType.RP)
                    {
                        throw new AggregateException("无法识别的表达式");
                    }

                    this.Next();
                    return left;

                default:
                    throw new AggregateException("无法识别的表达式");
            }
        }

        private decimal Table(string name)
        {
            throw new NotImplementedException(name);
        }

        private void Next()
        {
            this.pos++;
        }

        private bool IsEnd()
        {
            return this.pos >= this.store.Count;
        }
    }
}