namespace Program01
{
    using System;
    using System.Data.Entity;

    public class Product 
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }
    }

    public class ProductCheck : Product
    {
        public DateTime CheckedOn { get; set; }

        public string Reason { get; set; }

        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }
    }

    public class TestDbContext : DbContext
    {
        public TestDbContext(string configuration)
            : base(configuration)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCheck> ProductChecks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                        .ToTable("Products");

            modelBuilder.Entity<ProductCheck>()
                        .Map(k => k.MapInheritedProperties())
                        .ToTable("ProductChecks")
                        .HasRequired(k => k.Product)
                        .WithMany()
                        .HasForeignKey(k => k.ProductId);

            base.OnModelCreating(modelBuilder);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new TestDbContext("name=test"))
            {
                context.Database.CreateIfNotExists();
            }
        }
    }
}