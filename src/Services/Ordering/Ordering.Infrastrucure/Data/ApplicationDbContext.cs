﻿using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;
using System.Reflection;

namespace Ordering.Infrastrucure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options){ }


        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem>  OrderItems => Set<OrderItem>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products => Set<Product>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  
            base.OnModelCreating(modelBuilder);
        }
    }
}
