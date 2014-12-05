namespace Program01
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using HyLibrary.Lambda;


    public class A
    {
        public int Id { get; set; }

        public B BB { get; set; }
    }

    public class B
    {
        public int Id { get; set; }

        public C C { get; set; }
    }

    public class C
    {
        public int Id { get; set; }
    }

    public class TestA
    {
        public int Id { get; set; }

        public int BB_Id { get; set; }

        public int BB_C_Id { get; set; }
    }


    public class Program
    {
        static void Main(string[] args)
        {
            A a = new A
            {
                Id = 1,
                BB = new B
                {
                    Id = 2,
                    C = new C 
                    {
                        Id = 3
                    }
                }
            };

            List<A> list = new List<A> { a };

            foreach (var item in ProjectionExpression<A, TestA>.To(list))
            {
                Console.WriteLine("Id = {0}, B_Id = {1}, B_C_Id = {2}", item.Id, item.BB_Id, item.BB_C_Id);
            }
        }
    }
}