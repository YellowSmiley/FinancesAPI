using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinancesAPI.Models;

namespace FinancesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PeopleContext _context;

        public PeopleController(PeopleContext context)
        {
            _context = context;
        }

        // GET: api/People
        [HttpGet]
        public IEnumerable<Person> GetPeople()
        {
            return _context.People;
        }

        // POST: api/People
        [HttpPost]
        public async Task<IActionResult> PostPerson([FromBody] IList<Person> people)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Delete
            if (_context.People.Count() > people.Count())
            {
                foreach (Person person in _context.People)
                {
                    var existingPerson = people.Where(per => per.Id == person.Id);
                    if (!existingPerson.Any())
                    {
                        _context.Remove(_context.People.Single(a => a.Id == person.Id));
                    }
                }
            }

            foreach (Person person in people)
            {
                var existingPerson = _context.People.Where(per => per.Id == person.Id);
                // Add
                if (!existingPerson.Any())
                {
                    _context.People.Add(person);
                }
                // Modify
                else
                {
                    var ep = _context.People.First(per => per.Id == person.Id);
                    ep = person;
                }
            }

            await _context.SaveChangesAsync();
            
            return Ok(_context.People);
        }
    }
}