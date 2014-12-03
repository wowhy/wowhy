namespace Program01
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class Program
    {
        public class Test
        {
            [Key]
            public int Id { get; set; }

            public string Name { get; set; }

            public override string ToString()
            {
                return string.Format("{{ Id: {0}, Name: {1}}}", this.Id, this.Name);
            }
        }

        public class TestDbContext : DbContext
        {
            public TestDbContext(string configuration)
                : base(configuration)
            {
            }

            public DbSet<Test> Tests { get; set; }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Test>().HasKey(k => k.Id)
                            .Property(k => k.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                base.OnModelCreating(modelBuilder);
            }
        }

        [ThreadStatic]
        private static TestDbContext context;

        public static TestDbContext Context
        {
            get 
            {
                if (context == null)
                {
                    context = new TestDbContext("name=test");
                }

                return context; 
            }
        }



        static void Main(string[] args)
        {
            Context.Database.CreateIfNotExists();

            Parallel.For(1, 20,
                (i) =>
                {
                    Console.WriteLine("Thread: " + Thread.CurrentThread.ManagedThreadId);
                    Context.Tests.Add(new Test { Name = "test" + i });
                    Context.SaveChanges();
                });

            Parallel.For(1, 5, (i) => 
            {
                Format(Context.Tests.ToList());
            });
        }

        static object state = new object();

        private static void Format(List<Test> list)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('[');
            foreach (var it in list)
            {
                builder.Append(it.ToString());
            }

            builder.Append(']');
            lock (state)
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Thread: " + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine(list.Count);
                // Console.WriteLine(builder.ToString());
                Console.WriteLine("------------------------------------");
            }
        }
    }
}