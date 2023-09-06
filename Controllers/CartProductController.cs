using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veebipood.Data;
using veebipood.Models;

namespace veebipood.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CartProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet] //вывод на экран
        public List<CartProduct> GetCartProduct()
        {
            var cartproduct = _context.CartProducts.ToList();
            return cartproduct;
        }
        [HttpPost] //добавление
        public List<CartProduct> PostProduct([FromBody] CartProduct cartproducts)
        {
            _context.Products.Add(cartproducts.prodId);
            _context.SaveChanges();

            cartproducts.ProductId = cartproducts.prodId.Id;

            _context.CartProducts.Add(cartproducts);
            _context.SaveChanges();
            return _context.CartProducts.Include(a => a.prodId).ToList();
        }
        [HttpDelete("{id}")] //удаление
        public List<CartProduct> DeleteProduct(int id)
        {
            var cartproducts = _context.CartProducts.Include(a => a.prodId).FirstOrDefault(a => a.Id == id);

            if (cartproducts == null)
            {
                return _context.CartProducts.Include(a => a.prodId).ToList();
            }

            if (cartproducts.prodId != null)
            {
                _context.Products.Remove(cartproducts.prodId);
            }

            _context.CartProducts.Remove(cartproducts);
            _context.SaveChanges();
            return _context.CartProducts.Include(a => a.prodId).ToList();
        }
        [HttpGet("{id}")] //вывод на экран через Ид
        public ActionResult<CartProduct> getCartProduct(int id)
        {
            var cartproducts = _context.CartProducts.Find(id);

            if (cartproducts == null)
            {
                return NotFound();
            }

            return cartproducts;
        }
        //[HttpPut("{id}")]//изменение
        //public ActionResult<List<CartProduct>> putCartProduct(int id, [FromBody] CartProduct updatedCartProduct)
        //{
        //    var cartproducts = _context.CartProducts.Find(id);

        //    if (cartproducts == null)
        //    {
        //        return NotFound();
        //    }

        //    cartproducts.Quantity = updatedCartProduct.Quantity;

        //    _context.CartProducts.Update(cartproducts);
        //    _context.SaveChanges();

        //    return Ok(_context.Products);
        //}
    }
}
