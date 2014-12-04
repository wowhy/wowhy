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

            [Required]
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
            Task task = Task.Factory.StartNew(() => 
            {
                Console.WriteLine("ThreadId: " + Thread.CurrentThread.ManagedThreadId);
                try
                {
                    var test = Context.Tests.Where(k => k.Id == 1).FirstOrDefault();
                    Console.WriteLine("before: Name = " + test.Name);
                    test.Name = null;
                    Context.SaveChanges();
                }
                catch
                {
                    Console.WriteLine("Save Failed");
                }
            }).ContinueWith(t => 
            {
                Console.WriteLine("ThreadId: " + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("Cache Name = " + Context.Tests.Where(k => k.Id == 1).FirstOrDefault().Name);
            }, TaskContinuationOptions.ExecuteSynchronously);

            task.Wait();
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