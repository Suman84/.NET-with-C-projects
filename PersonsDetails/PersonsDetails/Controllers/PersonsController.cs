using System.Diagnostics.Contracts;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer;

namespace PersonsDetails.Controllers
{
    [ApiController]
    [Route("api/Persons")]
    public class PersonsController : Controller
    {
        private readonly AppDbContext dbContext;

        public PersonsController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetPerson()
        {
            return Ok(dbContext.Persons.ToList());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetPerson([FromRoute] Guid id)
        {
            var contract = await dbContext.Persons.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            return Ok(contract);
        }


        [HttpPost]
        public async Task<IActionResult> AddPerson(AddPersonRequest addPersonRequest)
        {
            var Persons = new Person()
            {
                ID = Guid.NewGuid(),
                Name = addPersonRequest.Name,
                Email = addPersonRequest.Email,
                Age = addPersonRequest.Age,
                Phonenumber = addPersonRequest.Phonenumber
            };

            await dbContext.Persons.AddAsync(Persons);
            await dbContext.SaveChangesAsync();

            return Ok(Persons);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdatePerson([FromRoute] Guid id, updatePersonRequest updatePersonRequest)
        {
            var Person = await dbContext.Persons.FindAsync(id);
            if (Person != null)
            {
                Person.Name = updatePersonRequest.Name;
                Person.Age = updatePersonRequest.Age;
                Person.Phonenumber = updatePersonRequest.Phonenumber;
                Person.Email = updatePersonRequest.Email;

                await dbContext.SaveChangesAsync();

                return Ok(Person);
            }

            return NotFound();
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeletePerson([FromRoute] Guid id)
        {
            var Person = await dbContext.Persons.FindAsync(id);
            if (Person != null)
            {
                dbContext.Remove(Person);
                await dbContext.SaveChangesAsync();
                return Ok(Person);
            }
            return NotFound(nameof(Person));
        }

    }
}
