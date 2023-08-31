using Microsoft.AspNetCore.Mvc;
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
            _context.CartProducts.Add(cartproducts);
            _context.SaveChanges();
            return _context.CartProducts.ToList();
        }
        [HttpDelete("{id}")] //удаление
        public List<CartProduct> DeleteCartProduct(int id)
        {
            var cartproducts = _context.CartProducts.Find(id);

            if (cartproducts == null)
            {
                return _context.CartProducts.ToList();
            }

            _context.CartProducts.Remove(cartproducts);
            _context.SaveChanges();
            return _context.CartProducts.ToList();
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
        [HttpPut("{id}")]//изменение
        public ActionResult<List<CartProduct>> putCartProduct(int id, [FromBody] CartProduct updatedCartProduct)
        {
            var cartproducts = _context.CartProducts.Find(id);

            if (cartproducts == null)
            {
                return NotFound();
            }

            cartproducts.Quantity = updatedCartProduct.Quantity;

            _context.CartProducts.Update(cartproducts);
            _context.SaveChanges();

            return Ok(_context.Products);
        }
    }
}
