using Humanizer;
using InventoryManagement.Data;
using InventoryManagement.Models;
using InventoryManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Threading.Tasks;

namespace InventoryManagementCreate.Controllers
{
    public class InventoryManagementController : Controller
    {
        private readonly InventoryManagementDbContext _context;
        public InventoryManagementController(InventoryManagementDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string? ProductName, string? CategoryName)
        {
           
            IQueryable<Products> query = _context.Products;
            if(!string.IsNullOrEmpty(ProductName))
            {
                query = query.Where(p => p.Name.Contains(ProductName) && p.Name !=null);
            }
            if(!string.IsNullOrEmpty(CategoryName))
            {
                query = query.Where(p => p.Category.ToString() == CategoryName);
            }
            var product = query.Select(p => new ProductsListVM
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price,
                Quantity = p.Quantity
            }).ToList();

            return View(product);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductsInsertVM product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            var products = new Products
            {
                Name = product.Name,
                Category = product.Category,
                Price = product.Price,
                Quantity = product.Quantity
            };
            if (ModelState.IsValid)
            {
                _context.Products.Add(products);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(d => d.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            var productVM = new ProductsListVM
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                Quantity = product.Quantity,
                Price = product.Price
            };
            return View(productVM);
        }
        public IActionResult Edit(int id)
        {
            
            var product = _context.Products.FirstOrDefault(d => d.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var productVM = new ProductsInsertVM
            {
                Name = product.Name,
                Category = product.Category,
                Quantity = product.Quantity,
                Price = product.Price
            };
            return View(productVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductsListVM products)
        {
            if (!ModelState.IsValid)
            {
                return View(products);
            }
            var product = await _context.Products.FindAsync(products.Id);
            if(product==null)
            {
                return NotFound();
            }
            product.Name = products.Name;
            product.Category = products.Category;
            product.Quantity = products.Quantity;
            product.Price = products.Price;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(d => d.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var productVM = new ProductsListVM
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                Quantity = product.Quantity,
                Price = product.Price
            };

            return View(productVM);
        }
        [HttpPost]
        [ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.FirstOrDefault(d => d.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
