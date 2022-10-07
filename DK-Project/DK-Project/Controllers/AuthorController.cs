using AutoMapper;
using DK_Project.DL.Interfaces;
using DK_Project.DL.Repositories.InMemoryRepositories;
using DK_Project.Models.Mediatr.Commands.AuthorCommands;
using DK_Project.Models.Models;
using DK_Project.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using IAuthorRepository = DK_Project.DL.Interfaces.IAuthorRepository;

namespace DK_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorService;
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<AuthorController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AuthorController(IAuthorRepository authorService, ILogger<AuthorController> logger, IMapper mapper, IMediator mediator, IBookRepository bookRepository)
        {
            _authorService = authorService;
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
            _bookRepository = bookRepository;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetNamesAndId")]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Information Test");
            _logger.LogWarning("Warning test");
            _logger.LogError("Error Test");
            _logger.LogCritical("Critical Test");

            return Ok(await _mediator.Send(new GetAllAuthorsCommand()));
        }

        [HttpGet("GetByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByID(int Id)
        {
            if (Id<=0)
            {
                return BadRequest($"Parameter id:{Id} must be greater than 0 or 0");
            }
            var result = await _authorService.GetById(Id);
            if (result == null)
            {
                return NotFound("Book Doesn't Exists");
            }
            return Ok(await _mediator.Send(new GetByIdAuthorCommand(Id)));
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] AddUpdateAuthorRequest authorRequest)
        {
            if (authorRequest == null) return BadRequest(authorRequest);
            var authorExist = await _authorService.GetAuthorByName(authorRequest.Name);
            if (authorExist != null) return BadRequest("Author Already Exists");

            var author = _mapper.Map<Author>(authorRequest);
            return Ok(await _mediator.Send(new AddAuthorCommand(author)));
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] AddUpdateAuthorRequest authorRequest)
        {
            if (authorRequest == null) return BadRequest(authorRequest);
            var authorExist = await _authorService.GetById(authorRequest.Id);
            if (authorExist == null) return BadRequest("Author Dosen't Exist");

            var newAuthor = _mapper.Map<Author>(authorRequest);
            return Ok(await _mediator.Send(new UpdateAuthorCommand(newAuthor)));
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var authorExist = await _authorService.GetById(id);
            if (authorExist == null) return BadRequest("Author Doesn't Exists");
            if (await _bookRepository.DoesAuthorHaveBooks(id))
            {
                return BadRequest("This author has books and can't be deleted.");
            }
            return Ok(await _mediator.Send(new DeleteAuthorCommand(id)));

        }

        [HttpGet("GetAuthorByName")]
        public async Task<IActionResult> GetAuthor(string name)
        {
            var authorByName = await _authorService.GetAuthorByName(name);
            if (authorByName == null) return BadRequest("Author Doesn't Exists");
            return Ok(authorByName);
        }

        [HttpGet("GetAuthorsBooks")]
        public async Task<IActionResult> GetAuthorBooks(int authorId)
        {
            var authorByName = await _bookRepository.GetAuthorBooks(authorId);
            if (authorByName == null) return BadRequest("Author Doesn't Exists");
            return Ok(authorByName);
        }
    }
}