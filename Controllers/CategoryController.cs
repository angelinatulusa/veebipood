using Microsoft.AspNetCore.Mvc;
using veebipood.Data;
using veebipood.Models;

namespace veebipood.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryControlller : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryControlller(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet] //вывод на экран
        public List<Category> GetCategory()
        {
            var cats = _context.Categories.ToList();
            return cats;
        }
        [HttpPost] //добавление
        public List<Category> PostCategory([FromBody] Category cat)
        {
            _context.Categories.Add(cat);
            _context.SaveChanges();
            return _context.Categories.ToList();
        }
        [HttpDelete("{id}")] //удаление
        public List<Category> DeleteCategory(int id)
        {
            var cat = _context.Categories.Find(id);

            if (cat == null)
            {
                return _context.Categories.ToList();
            }

            _context.Categories.Remove(cat);
            _context.SaveChanges();
            return _context.Categories.ToList();
        }
        [HttpGet("{id}")] //вывод на экран через Ид
        public ActionResult<Category> getCategory(int id)
        {
            var cat = _context.Categories.Find(id);

            if (cat == null)
            {
                return NotFound();
            }

            return cat;
        }
        [HttpPut("{id}")]//изменение
        public ActionResult<List<Category>> PutCategory(int id, [FromBody] Category updatedCategory)
        {
            var cat = _context.Categories.Find(id);

            if (cat == null)
            {
                return NotFound();
            }

            cat.Name = updatedCategory.Name;

            _context.Categories.Update(cat);
            _context.SaveChanges();

            return Ok(_context.Categories);
        }
    }
}
