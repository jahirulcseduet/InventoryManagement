using InventoryManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.ViewModel
{
    public class ProductsListVM
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public ProductCategory Category { get; set; }
        [Range(10.01, double.MaxValue, ErrorMessage = "Price must be greater than 50.")]
        [Display(Name = "Price(tk)")]
        public required decimal Price { get; set; }
        public required int Quantity { get; set; }
    }
}
