using BookStore.BL.Interfaces;
using DK_Project.DL.Repositories.InMemoryRepositories;
using DK_Project.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace DK_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(IPersonService userInMemoryRepository, ILogger<PersonController> logger)
        {
            _personService = userInMemoryRepository;
            _logger = logger;
        }

      

        [HttpGet(Name = "GetNamesAndId")]
        public IEnumerable<Person> Get()
        {
            return _personService.GetAllUsers();
        }

        [HttpGet("GetByID")]
        public Person? GetByID(int Id)
        {
            return _personService.GetById(Id);
        }

        [HttpPost("Add")]
        public Person? Add([FromBody] Person user)
        {
            return _personService.AddUser(user);
        }

        [HttpPut("Update")]
        public Person? Update([FromBody] Person user)
        {
            return _personService.UpdateUser(user);
        }

        [HttpDelete("Delete")]
        public Person? Delete(int id)
        {
            return _personService.DeleteUser(id);
        }
    }
}