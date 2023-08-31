using Microsoft.AspNetCore.Mvc;
using veebipood.Data;
using veebipood.Models;

namespace veebipood.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet] //вывод на экран
        public List<Person> GetPerson()
        {
            var people = _context.Persons.ToList();
            return people;
        }
        [HttpPost] //добавление
        public List<Person> PostPerson([FromBody] Person person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();
            return _context.Persons.ToList();
        }
        [HttpDelete("/kustuta2/{id}")] //удалениe
        public IActionResult DeletePerson2(int id)
        {
            var person = _context.Persons.Find(id);

            if (person == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(person);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpGet("{id}")] //вывод на экран через Ид
        public ActionResult<Person> GetPerson(int id)
        {
            var person = _context.Persons.Find(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }
        [HttpPut("{id}")]//изменение
        public ActionResult<List<Person>> PutPerson(int id, [FromBody] Person updatedPerson)
        { 
            var person = _context.Persons.Find(id);

            if (person == null)
            {
                return NotFound();
            }

            person.Password = updatedPerson.Password;

            _context.Persons.Update(person);
            _context.SaveChanges();

            return Ok(_context.Persons);
        }
    }
}
