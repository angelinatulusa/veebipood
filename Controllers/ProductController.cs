using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veebipood.Data;
using veebipood.Models;

namespace veebipood.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet] //вывод на экран
        public List<Product> GetProducts()
        {
            var product = _context.Products.Include(a => a.catId).ToList();
            return product;
        }
        [HttpPost] //добавление
        public List<Product> PostProduct([FromBody] Product products)
        {
            _context.Categories.Add(products.catId);
            _context.SaveChanges();

            products.CategoryId = products.catId.Id;

            _context.Products.Add(products);
            _context.SaveChanges();
            return _context.Products.Include(a => a.catId).ToList();
        }
        [HttpDelete("{id}")] //удаление
        public List<Product> DeleteProduct(int id)
        {
            var products = _context.Products.Include(a => a.catId).FirstOrDefault(a => a.Id == id);

            if (products == null)
            {
                return _context.Products.Include(a => a.catId).ToList();
            }

            if (products.catId != null)
            {
                _context.Categories.Remove(products.catId);
            }

            _context.Products.Remove(products);
            _context.SaveChanges();
            return _context.Products.Include(a => a.catId).ToList();
        }
        [HttpGet("{id}")] //вывод на экран через Ид
        public ActionResult<Product> GetAuthor(int id)
        {
            var products = _context.Products.Include(a => a.catId).FirstOrDefault(a => a.Id == id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }
    }
}
