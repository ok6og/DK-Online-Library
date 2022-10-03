using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.BL.Interfaces;
using DK_Project.DL.Interfaces;
using DK_Project.DL.Repositories.InMemoryRepositories;
using DK_Project.Models.Models;
using Microsoft.Extensions.Logging;

namespace BookStore.BL.Services
{
    public class BookService : IBookService
    {
        public readonly IBookRepository _bookRepository;
        private readonly ILogger<AuthorService> _logger;


        public BookService(IBookRepository bookRepository, ILogger<AuthorService> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }

        public Book? AddBook(Book book)
        {
            return _bookRepository.AddBook(book);
        }

        public Book? DeleteBook(int bookId)
        {
            return _bookRepository.DeleteBook(bookId);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public Book? GetById(int id)
        {
            return _bookRepository.GetById(id);
        }

        public Book UpdateBook(Book book)
        {
            return _bookRepository.UpdateBook(book);
        }
        public Book GetBookByName(string book)
        {
            return _bookRepository.GetBookByName(book);
        }
    }
}
