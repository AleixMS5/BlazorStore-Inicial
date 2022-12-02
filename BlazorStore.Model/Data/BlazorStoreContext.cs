using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BlazorStore.Model.Data
{
    public class BlazorStoreContext: DbContext
    {
        public BlazorStoreContext(DbContextOptions<BlazorStoreContext> opt) : base(opt)
        {


        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=.\\SQLEXPRESS;Database=DisiBlazorStore;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;", opt =>
            {
                opt.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(builder);
        }
        /*MultipleActiveResultSets=True*/
        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p=>p.CategoryId);
            model.Entity<Product>().HasIndex(u => u.LastUpdated).IsUnique(false);

            model.Entity<User>().HasIndex(u => u.Email).IsUnique(true);

            // Add some initial data...
            model.Entity<Category>().HasData(DataSeed.GetCategories());
            model.Entity<Product>().HasData(DataSeed.GetProducts());
            model.Entity<User>().HasData(DataSeed.GetUsers());
            model.Entity<Order>().HasData(DataSeed.GetOrders());
            model.Entity<OrderLine>().HasData(DataSeed.GetOrderLines());

            base.OnModelCreating(model);
        }
    }
}
