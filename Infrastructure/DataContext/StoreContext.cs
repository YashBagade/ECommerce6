using Core.Entitties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;


namespace Infrastructure.DataContext
{
    public class StoreContext :DbContext
    {
        public StoreContext(DbContextOptions options):base(options) 
        {
            
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<ProductBrand> ProductBrands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(e => e.Price).HasPrecision(18,4);
        }
    }
}
