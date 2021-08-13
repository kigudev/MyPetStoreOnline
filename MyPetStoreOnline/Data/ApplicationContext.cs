﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPetStoreOnline.Entities;
using System;

namespace MyPetStoreOnline.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public ApplicationContext()
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().OwnsOne(c => c.Address);
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