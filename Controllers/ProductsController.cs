using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using veebipood.Models;

namespace veebipood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> _products = new()
        {
        new Product(1,"Koola", 1.5, "Koola.png", true, 5, 1),
        new Product(2,"Fanta", 1.0, "Fanta.png", false, 1, 1),
        new Product(3,"Sprite", 1.7, "Sprite.png",true, 7, 1),
        new Product(4,"Vichy", 2.0, "Vichy.png", true, 14, 1),
        new Product(5,"Vitamin well", 2.5, "Vitamin_well.png", true , 3, 1)
        };

        // https://localhost:4444/tooted
        [HttpGet]
        public List<Product> Get()
        {
            return _products;
        }

        // https://localhost:4444/tooted/kustuta/0
        [HttpDelete("kustuta/{index}")]
        public List<Product> Delete(int index)
        {
            _products.RemoveAt(index);
            return _products;
        }
    }
}
