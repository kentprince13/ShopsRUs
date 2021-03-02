using Microsoft.EntityFrameworkCore;
using ShopsRUs.Domain.Entity;
using ShopsRUs.Infrastructure.Configurations;
using ShopsRUs.Infrastructure.SeedData;

namespace ShopsRUs.Infrastructure
{
    public class ShopsRUsContext:DbContext
    {
        public ShopsRUsContext(DbContextOptions<ShopsRUsContext> options):base(options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new DiscountConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }


    }
}
