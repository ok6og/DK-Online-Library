using BookStore.BL.Interfaces;
using DK_Project.DL.Interfaces;
using DK_Project.DL.Repositories.InMemoryRepositories;
using DK_Project.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace DK_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorInMemoryRepository;
        private readonly ILogger<AuthorController> _logger;

        

        public AuthorController(IAuthorService userInMemoryRepository, ILogger<AuthorController> logger)
        {
            _authorInMemoryRepository = userInMemoryRepository;
            _logger = logger;
        }



        [HttpGet("GetNamesAndId")]
        public IEnumerable<Author> Get()
        {
            return _authorInMemoryRepository.GetAllUsers();
        }

        [HttpGet("GetByID")]
        public Author? GetByID(int Id)
        {

            return _authorInMemoryRepository.GetById(Id);

        }

        [HttpPost("Add")]
        public Author? Add([FromBody] Author user)
        {
            return _authorInMemoryRepository.AddUser(user);

        }

        [HttpPut("Update")]
        public Author? Update([FromBody] Author user)
        {
            return _authorInMemoryRepository.UpdateUser(user);

        }

        [HttpDelete("Delete")]
        public Author? Delete(int id)
        {
            return _authorInMemoryRepository.DeleteUser(id);

        }
    }
}