using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using veebipood.Models;

namespace veebipood.Controllers
{
    public class ProductsController : Controller
    {
        private static List<Product> _tooted = new()
        {
        new Product(1,"Koola", 1.5,"a", true,12,1),
        new Product(2,"Fanta", 1.0,"b", false,34,2),
        new Product(3,"Sprite", 1.7,"c", true,23,1),
        new Product(4,"Vichy", 2.0,"d", true,36,5),
        new Product(5,"Vitamin well", 2.5,"e", true,28,7)
        };

        // https://localhost:4444/tooted
        [HttpGet]
        public List<Product> Get()
        {
            return _tooted;
        }

        // https://localhost:4444/tooted/kustuta/0
        [HttpGet("kustuta/{index}")]
        public List<Product> Delete(int index)
        {
            _tooted.RemoveAt(index);
            return _tooted;
        }

        [HttpGet("kustuta2/{index}")]
        public string Delete2(int index)
        {
            _tooted.RemoveAt(index);
            return "Kustutatud!";
        }

        [HttpGet("lisa/{id}/{nimi}/{hind}/{aktiivne}")]
        public List<Product> Add(int id, string nimi, double hind, string pilt, bool aktiivne, int varu, int katId)
        {   
            Product toode = new Product(id, nimi, hind,pilt, aktiivne,varu,katId);
            _tooted.Add(toode);
            return _tooted;
        }
    }
}
