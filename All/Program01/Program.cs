namespace Program01
{
    using Microsoft.CSharp;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using HyLibrary;
    using System.Diagnostics.Contracts;
    using System.Collections;

    public class A
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime JoinDate { get; set; }
        public int Count { get; set; }
    }

    public class ChangeValue
    {
    }

    public class ChangeTracker <T> : IEnumerable<KeyValuePair<string, ChangeValue>>
        where T : class
    {
        #region Static
        #endregion

        public ChangeTracker(T origin, T changed)
        {
            Contract.Assert(origin != null, "origin");
            Contract.Assert(changed != null, "changed");

            this.Origin = origin;
            this.Changed = changed;
        }

        public ChangeValue this[string property]
        {
            get
            {
                return this.ChangeValues[property];
            }
        }

        public T Origin { get; protected set; }
        public T Changed { get; protected set; }

        public int Counts { get; protected set; }
        public List<string> Properties { get; protected set; }

        protected Dictionary<string, ChangeValue> ChangeValues { get; set; }

        public IEnumerator<KeyValuePair<string, ChangeValue>> GetEnumerator()
        {
            return this.ChangeValues.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.ChangeValues.GetEnumerator();
        }

        protected virtual void Init()
        {

        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
