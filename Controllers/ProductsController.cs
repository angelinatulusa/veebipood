using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Xml.Linq;
using veebipood.Models;
using veebipood.Data;

namespace veebipood.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        [HttpDelete("kustuta/{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("lisa/{id}/{name}/{price}/{aktiivne}/{stock}")]
        public IActionResult Add([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Invalid product data.");
            }

            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        [HttpPatch("hind-dollaritesse/{kurss}")]
        public IActionResult Dollaritesse(double kurss)
        {
            var products = _context.Products.ToList();
            foreach (var product in products)
            {
                product.Price *= kurss;
            }
            _context.SaveChanges();
            return Ok(products);
        }

        [HttpPatch("hind-eurosse/{kurss}")]
        public IActionResult Eurosse(double kurss)
        {
            var products = _context.Products.ToList();
            foreach (var product in products)
            {
                product.Price /= kurss;
            }
            _context.SaveChanges();
            return Ok(products);
        }
        [HttpGet("pay/{sum}/{id}")]
        public async Task<IActionResult> MakePayment(string sum, int id)
        {
            var product = _context.Products.Find(id);

            if (product == null || product.Stock <= 0)
            {
                return BadRequest("Invalid product or out of stock.");
            }

            var paymentData = new
            {
                api_username = "e36eb40f5ec87fa2",
                account_name = "EUR3D1",
                amount = sum,
                order_reference = Math.Ceiling(new Random().NextDouble() * 999999),
                nonce = $"a9b7f7e7as{DateTime.Now}{new Random().NextDouble() * 999999}",
                timestamp = DateTime.Now,
                customer_url = "https://maksmine.web.app/makse"
            };

            var json = JsonSerializer.Serialize(paymentData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "ZTM2ZWI0MGY1ZWM4N2ZhMjo3YjkxYTNiOWU1Yjc0NTI0YzJlOWZjMjgyZjhhYzhjZA==");

            var response = await client.PostAsync("https://igw-demo.every-pay.com/api/v4/payments/oneoff", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonDoc = JsonDocument.Parse(responseContent);
                var paymentLink = jsonDoc.RootElement.GetProperty("payment_link");

                // Уменьшить количество товара в наличии
                product.Stock--;

                // Если товара больше нет в наличии, отметить его как неактивный
                if (product.Stock == 0)
                {
                    product.Active = false;
                }

                _context.SaveChanges();

                return Ok(paymentLink.GetString());
            }
            else
            {
                return BadRequest("Payment failed.");
            }
        }
    }
}
