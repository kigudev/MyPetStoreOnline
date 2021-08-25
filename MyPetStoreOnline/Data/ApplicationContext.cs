using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPetStoreOnline.Entities;
using System;

namespace MyPetStoreOnline.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        // Solo se usa si tienen el proyecto de consola también funcionando
        public ApplicationContext()
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().OwnsOne(c => c.Address);
            modelBuilder.Entity<Product>().Property(c => c.CreatedAt).HasDefaultValueSql("getdate()");
        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyPetStoreOnlineDb;Integrated Security=True");

//#if DEBUG
//            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
//#endif
//        }
    }
}