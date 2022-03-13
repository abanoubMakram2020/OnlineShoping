using Microsoft.EntityFrameworkCore;
using OnlineShoping.Domain.Entities;
using OnlineShoping.Infra.Data.SeedingData;

namespace OnlineShoping.Infra.Data.Context
{
    public class OnlineShopingDBContext : DbContext
    {
        public OnlineShopingDBContext(DbContextOptions options) : base(options)
        {
        }
        public OnlineShopingDBContext()
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Start Seed Data
            modelBuilder.StartSeedData();
            #endregion
        }
    }
}
