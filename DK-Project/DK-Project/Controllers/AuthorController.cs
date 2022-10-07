using AutoMapper;
using DK_Project.DL.Interfaces;
using DK_Project.DL.Repositories.InMemoryRepositories;
using DK_Project.Models.Mediatr.Commands.AuthorCommands;
using DK_Project.Models.Mediatr.Commands.BookCommands;
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
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthorController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetNamesAndId")]
        public async Task<IActionResult> Get()
        {
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
            var result = await _mediator.Send(new GetByIdAuthorCommand(Id));
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
            var authorExist = await _mediator.Send(new GetAuthorByNameCommand(authorRequest.Name));
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
            var authorExist = await _mediator.Send(new GetByIdAuthorCommand(authorRequest.Id));
            if (authorExist == null) return BadRequest("Author Dosen't Exist");

            var newAuthor = _mapper.Map<Author>(authorRequest);
            return Ok(await _mediator.Send(new UpdateAuthorCommand(newAuthor)));
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var authorExist = await _mediator.Send(new GetByIdAuthorCommand(id));
            if (authorExist == null) return BadRequest("Author Doesn't Exists");
            if (await _mediator.Send(new DoesAuthorHaveBooksCommand(id))) 
            {
                return BadRequest("This author has books and can't be deleted.");
            }
            return Ok(await _mediator.Send(new DeleteAuthorCommand(id)));

        }

        [HttpGet("GetAuthorByName")]
        public async Task<IActionResult> GetAuthor(string name)
        {
            var authorByName = await _mediator.Send(new GetAuthorByNameCommand(name));
            if (authorByName == null) return BadRequest("Author Doesn't Exists");
            return Ok(authorByName);
        }

        [HttpGet("GetAuthorsBooks")]
        public async Task<IActionResult> GetAuthorBooks(int authorId)
        {
            var authorByName = await _mediator.Send(new GetAuthorBooksCommand(authorId));
            if (authorByName == null) return BadRequest("Author Doesn't Exists");
            return Ok(authorByName);
        }
    }
}