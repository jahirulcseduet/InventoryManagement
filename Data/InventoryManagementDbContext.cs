using Microsoft.EntityFrameworkCore;
using InventoryManagement.Models;

namespace InventoryManagement.Data
{
    public class InventoryManagementDbContext : DbContext
    {
        public InventoryManagementDbContext(DbContextOptions<InventoryManagementDbContext> options): base(options)
        {
        }
        public DbSet<Products> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id=1,
                Name="Energy Pluse",
                Category= ProductCategory.Food,
                Price =100,
                Quantity = 0
            },
            new Products
            {
                Id = 2,
                Name = "Shirt",
                Category = ProductCategory.Clothing,
                Price = 3900,
                Quantity = 9
            },
            new Products
            {
                Id = 3,
                Name = "Charger",
                Category = ProductCategory.Electronics,
                Price = 2100,
                Quantity = 29
            }
            );
        }

    }  
}
