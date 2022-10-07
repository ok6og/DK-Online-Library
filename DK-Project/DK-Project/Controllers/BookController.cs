using AutoMapper;
using DK_Project.DL.Interfaces;
using DK_Project.Models.Mediatr.Commands;
using DK_Project.Models.Mediatr.Commands.BookCommands;
using DK_Project.Models.Models;
using DK_Project.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DK_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookService;
        private readonly ILogger<BookController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;


        public BookController(IBookRepository bookRepo, ILogger<BookController> logger, IMapper mapper, IMediator mediator)
        {
            _bookService = bookRepo;
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetNamesAndId")]
        public async Task <IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetAllBooksCommand()));
        }

        [HttpGet("GetByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByID(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest($"Parameter id:{Id} must be greater than 0 or 0");
            }
            var result = await _bookService.GetById(Id);
            if (result == null)
            {
                return NotFound("Book Doesn't Exists");                                                                                                                                
            }
            return Ok(await _mediator.Send(new GetByIdCommand(Id)));
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] AddUpdateBookRequest bookRequest)
        {
            if (bookRequest == null) return BadRequest(bookRequest);
            var bookExist = await _bookService.GetBookByName(bookRequest.Title);
            if (bookExist != null) return BadRequest("Book Already Exists");

            var book = _mapper.Map<Book>(bookRequest);
            return Ok(await _mediator.Send(new AddBookCommand(book)));
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] AddUpdateBookRequest bookRequest)
        {
            if (bookRequest == null) return BadRequest(bookRequest);
            var bookExist = await _bookService.GetById(bookRequest.Id);
            if (bookExist == null) return BadRequest("Book Doesn't Exists");

            var book = _mapper.Map<Book>(bookRequest);
            return Ok(await _mediator.Send(new UpdateBookCommand(book)));
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var bookExist = await _bookService.GetById(id);
            if (bookExist == null) return BadRequest("Book Doesn't Exists");
            return  Ok(await _mediator.Send(new DeleteBookCommand(id)));
        }
    }
}