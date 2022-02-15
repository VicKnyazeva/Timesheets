using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using Timesheets.Domain;
using Timesheets.Domain.DTO;

namespace Timesheets.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonsController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        // GET: api/<PersonsController>?skip=&take=
        [HttpGet]
        public IEnumerable<IPerson> GetAll([FromQuery] int? skip, [FromQuery] int? take)
        {
            var q = _personRepository.GetAll();
            if (skip.HasValue && skip.Value > 0)
            {
                q = q.Skip(skip.Value);
            }
            if (take.HasValue && take.Value > 0)
            {
                q = q.Take(take.Value);
            }
            return q;
        }

        // GET: api/<PersonsController>/search?searchTerm={term}
        [HttpGet("search")]
        public IEnumerable<IPerson> Search([FromQuery] string searchTerm)
        {
            var q = _personRepository.GetAll();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                q = q.Where(p => p.FirstName.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) 
                || p.LastName.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase));
            }
            return q;
        }


        // GET api/<PersonsController>/5
        [HttpGet("{id}")]
        public IPerson GetById(int id)
        {
            return _personRepository.GetById(id);   
        }

        // POST api/<PersonsController>
        [HttpPost]
        public IActionResult Post([FromBody] PersonCreateDTO input)
        {
            var newPerson = _personRepository.Create(input);

            return CreatedAtAction(nameof(GetById), new { id = newPerson.Id }, newPerson);
        }

        // PUT api/<PersonsController>
        [HttpPut]
        public IActionResult Put([FromBody] PersonEditDTO input)
        {
            var result = _personRepository.Update(input);
            if (result == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE api/<PersonsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _personRepository.Delete(id);
            if (result == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
