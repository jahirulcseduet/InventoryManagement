using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public ProductCategory Category { get; set; }
        public required decimal Price { get; set; }
        public required int Quantity { get; set; }

    }
    public enum ProductCategory
    {
        Food,
        Electronics,
        Clothing,
        HomeAppliances,
        Books,
        SportsEquipment
    }
}
