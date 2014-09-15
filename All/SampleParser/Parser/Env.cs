namespace SampleParser.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Env
    {
        private Dictionary<string, Symbol> table;

        protected Env prev;

        public Env(Env p)
        {
            table = new Dictionary<string, Symbol>();
            prev = p;
        }

        public Symbol Get(string s) 
        {
            for (var e = this; e != null; e = e.prev) 
            {
                var found = e.table[s];
                if (found != null)
                    return found;
            }

            return null;
        }
    }
}