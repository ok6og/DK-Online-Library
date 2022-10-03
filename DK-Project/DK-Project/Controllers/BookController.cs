using BookStore.BL.Interfaces;
using DK_Project.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace DK_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookRepo, ILogger<BookController> logger)
        {
            _bookService = bookRepo;
            _logger = logger;
        }

        [HttpGet("GetNamesAndId")]
        public IEnumerable<Book> Get()
        {
            return _bookService.GetAllBooks();
        }

        [HttpGet("GetByID")]
        public Book? GetByID(int Id)
        {

            return _bookService.GetById(Id);

        }

        [HttpPost("Add")]
        public Book? Add([FromBody] Book user)
        {
            return _bookService.AddBook(user);

        }

        [HttpPut("Update")]
        public Book? Update([FromBody] Book user)
        {
            return _bookService.UpdateBook(user);

        }

        [HttpDelete("Delete")]
        public Book? Delete(int id)
        {
            return _bookService.DeleteBook(id);

        }
    }
}