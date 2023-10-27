using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Xml.Linq;
using veebipood.Models;

namespace veebipood.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController
    {
        private static List<Product> _products = new List<Product>{
        new Product(1,"Koola", 1.5, true,4),
        new Product(2,"Fanta", 1.0, false,5),
        new Product(3,"Sprite", 1.7, true,1),
        new Product(4,"Vichy", 2.0, true,7),
        new Product(5,"Vitamin well", 2.5, true,2)
        };


        // https://localhost:7052/tooted
        [HttpGet]
        public List<Product> Get()
        {
            return _products;
        }

        [HttpGet("kustuta/{index}")]
        public List<Product> Delete(int index)
        {
            _products.RemoveAt(index);
            return _products;
        }

        [HttpPost("lisa/{id}/{name}/{price}/{aktiivne}/{stock}")]
        public List<Product> Add(int id, string name, double price, bool aktiivne, int stock)
        {
            Product product = new Product(id, name, price, aktiivne, stock);
            _products.Add(product);
            return _products;
        }

        [HttpGet("hind-dollaritesse/{kurss}")] // GET /tooted/hind-dollaritesse/1.5
        public List<Product> Dollaritesse(double kurss)
        {
            for (int i = 0; i < _products.Count; i++)
            {
                _products[i].Price = _products[i].Price * kurss;
            }
            return _products;
        }

        // või foreachina:

        [HttpGet("hind-dollaritesse2/{kurss}")] // GET /tooted/hind-dollaritesse2/1.5
        public List<Product> Dollaritesse2(double kurss)
        {
            foreach (var t in _products)
            {
                t.Price = t.Price * kurss;
            }

            return _products;
        }
    }
}
