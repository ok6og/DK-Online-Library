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

        public async Task<Book?> AddBook(Book book)
        {
            if (!(await _bookRepository.DoesAuthorHaveBooks(book.AuthorId)))
            {
                _logger.LogInformation("There is no such author");
                return null;
            }
            return await _bookRepository.AddBook(book);
        }

        public async Task<Book?> DeleteBook(int bookId)
        {
            return await _bookRepository.DeleteBook(bookId);
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _bookRepository.GetAllBooks();
        }

        public async Task<Book?> GetById(int id)
        {
            return await _bookRepository.GetById(id);
        }

        public async Task<Book> UpdateBook(Book book)
        {
            return await _bookRepository.UpdateBook(book);
        }
        public async Task<Book> GetBookByName(string book)
        {
            return await _bookRepository.GetBookByName(book);
        }
    }
}
