using Microsoft.EntityFrameworkCore;
using OnlineShoping.Domain.Entities;

namespace OnlineShoping.Infra.Data.SeedingData
{
    public static class SeedingData
    {
        public static void StartSeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.RolesSeedData();

        }

        static void RolesSeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                RoleEntityData(id: 1, description: "Admin"),
                RoleEntityData(id: 2, description: "User"));
        }
        static Role RoleEntityData(int id, string description) =>
         new Role
         {
             Id = id,
             Description = description
         };
    }
}
