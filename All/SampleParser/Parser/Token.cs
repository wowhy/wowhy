namespace SampleParser.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Token
    {
        public int Tag { get; protected set; }
        public Token(int t) { this.Tag = t; }

        public override string ToString()
        {
            return string.Format("{{tag: {0}}}", this.TagName());
        }

        public string TagName()
        {
            if (Enum.IsDefined(typeof(KeyTag), this.Tag))
            {
                return ((KeyTag)this.Tag).ToString();
            }
            else if (this.Tag < 256)
            {
                if (this.Tag == ' ')
                    return "space";
                if (this.Tag == '\r')
                    return "\\r";
                else if (this.Tag == '\n')
                    return "\\n";
                else if (this.Tag == '\t')
                    return "\\t";
                else
                    return ((char)this.Tag) + "";
            }
            else
            {
                return this.Tag.ToString();
            }
        }
    }
}