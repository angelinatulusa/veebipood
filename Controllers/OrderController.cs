using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using veebipood.Data;
using veebipood.Models;

namespace veebipood.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet] //вывод на экран
        public List<Order> GetOrders()
        {
            var order = _context.Orders.ToList();
            return order;
        }
        [HttpPost] //добавление
        public List<Order> PostOrder([FromBody] Order orders)
        {
            _context.Orders.Add(orders);
            _context.SaveChanges();
            return _context.Orders.ToList();
        }
        [HttpDelete("{id}")] //удаление
        public List<Order> DeleteOrder(int id)
        {
            var orders = _context.Orders.Find(id);

            if (orders == null)
            {
                return _context.Orders.ToList();
            }

            _context.Orders.Remove(orders);
            _context.SaveChanges();
            return _context.Orders.ToList();
        }
        [HttpGet("{id}")] //вывод на экран через Ид
        public ActionResult<Order> getOrder(int id)
        {
            var orders = _context.Orders.Find(id);

            if (orders == null)
            {
                return NotFound();
            }

            return orders;
        }
        [HttpPut("{id}")]//изменение
        public ActionResult<List<Order>> putOrder(int id, [FromBody] Order updatedOrder)
        {
            var orders = _context.Orders.Find(id);

            if (orders == null)
            {
                return NotFound();
            }

            orders.CartProduct = updatedOrder.CartProduct;

            _context.Orders.Update(orders);
            _context.SaveChanges();

            return Ok(_context.Orders);
        }
    }
}
