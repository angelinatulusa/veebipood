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
        new Product(1,"Koola", 1.5, true,3),
        new Product(2,"Fanta", 1.0, false,5),
        new Product(3,"Sprite", 1.7, true,5),
        new Product(4,"Vichy", 2.0, true,7),
        new Product(5,"Vitamin well", 2.5, true,2)
        };

        [HttpGet]
        public List<Product> Get()
        {
            return _products;
        }


        [HttpDelete("kustuta/{index}")]
        public List<Product> Delete(int index)
        {
            _products.RemoveAt(index);
            return _products;
        }
        [HttpPost("lisa")]
        public List<Product> Add([FromBody] Product toode)
        {
            _products.Add(toode);
            return _products;
        }
        [HttpPost("lisa/{id}/{nimi}/{hind}/{aktiivne}")]
        public List<Product> Add(int id, string nimi, double hind, bool aktiivne, int stock)
        {
            Product product = new Product(id, nimi, hind, aktiivne, stock);
            _products.Add(product);
            return _products;
        }

        [HttpPost("lisa2")]
        public List<Product> Add2(int id, string nimi, double hind, bool aktiivne, int stock)
        {
            Product toode = new Product(id, nimi, hind, aktiivne, stock);
            _products.Add(toode);
            return _products;
        }

        [HttpPatch("hind-dollaritesse/{kurss}")]
        public List<Product> UpdatePrices(double kurss)
        {
            for (int i = 0; i < _products.Count; i++)
            {
                _products[i].Price = _products[i].Price * kurss;
            }
            return _products;
        }

    }
}
