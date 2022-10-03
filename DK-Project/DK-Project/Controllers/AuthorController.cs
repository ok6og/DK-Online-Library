using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using DK_Project.DL.Interfaces;
using DK_Project.DL.Repositories.InMemoryRepositories;
using DK_Project.Models.Models;
using DK_Project.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace DK_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorController> _logger;
        private readonly IMapper _mapper;



        public AuthorController(IAuthorService userInMemoryRepository, ILogger<AuthorController> logger, IMapper mapper)
        {
            _authorService = userInMemoryRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetNamesAndId")]
        public IActionResult Get()
        {
            _logger.LogInformation("Information Test");
            _logger.LogWarning("Warning test");
            _logger.LogError("Error Test");
            _logger.LogCritical("Critical Test");



            return Ok(_authorService.GetAllUsers());
        }

        [HttpGet("GetByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByID(int Id)
        {
            if (Id<=0)
            {
                return BadRequest($"Parameter id:{Id} must be greater than 0 or 0");
            }
            var result = _authorService.GetById(Id);
            if (result == null)
            {
                return NotFound(Id);
            }
            return Ok(result);

        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody] AddUpdateAuthorRequest authorRequest)
        {
            if (authorRequest == null) return BadRequest(authorRequest);
            var authorExist = _authorService.GetAuthorByName(authorRequest.Name);
            if (authorExist != null) return BadRequest("Author Already Exists");

            var author = _mapper.Map<Author>(authorRequest);            
            return Ok(_authorService.AddUser(author));
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromBody] AddUpdateAuthorRequest authorRequest)
        {
            if (authorRequest == null) return BadRequest(authorRequest);
            var authorExist = _authorService.GetById(authorRequest.Id);
            if (authorExist == null) return BadRequest("Author Dosen't Exist");

            var newAuthor = _mapper.Map<Author>(authorRequest);
            return Ok(_authorService.UpdateUser(newAuthor));

        }

        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            var authorExist = _authorService.GetById(id);
            if (authorExist == null) return BadRequest("Author Doesn't Exists");

            return Ok(_authorService.DeleteUser(id));

        }

        [HttpGet("GetAuthorByName")]
        public IActionResult GetAuthor(string name)
        {
            var authorByName = _authorService.GetAuthorByName(name);
            if (authorByName == null) return BadRequest("Book Doesn't Exists");
            return Ok(authorByName);
        }
    }
}