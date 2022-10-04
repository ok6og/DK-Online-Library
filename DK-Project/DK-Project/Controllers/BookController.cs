using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using DK_Project.Models.Models;
using DK_Project.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DK_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;
        private readonly IMapper _mapper;


        public BookController(IBookService bookRepo, ILogger<BookController> logger, IMapper mapper)
        {
            _bookService = bookRepo;
            _logger = logger;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetNamesAndId")]
        public async Task <IActionResult> Get()
        {
            return Ok(await _bookService.GetAllBooks());
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
                return NotFound(Id);
            }
            return Ok(result);
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
            return Ok(await _bookService.AddBook(book));
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
            return Ok(await _bookService.UpdateBook(book));
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var bookExist = await _bookService.GetById(id);
            if (bookExist == null) return BadRequest("Book Doesn't Exists");
            return Ok(await _bookService.DeleteBook(id));
        }
    }
}