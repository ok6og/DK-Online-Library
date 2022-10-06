//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BookStore.BL.Interfaces;
//using DK_Project.DL.Interfaces;
//using DK_Project.DL.Repositories.InMemoryRepositories;
//using DK_Project.Models.Models;
//using Microsoft.Extensions.Logging;

//namespace BookStore.BL.Services
//{
//    public class BookService : Interfaces.IBookRepository
//    {
//        public readonly DK_Project.DL.Interfaces.IBookRepository _bookRepository;
//        public readonly DK_Project.DL.Interfaces.IAuthorRepository _authorRepository;
//        private readonly ILogger<BookService> _logger;


//        public BookService(DK_Project.DL.Interfaces.IBookRepository bookRepository, ILogger<BookService> logger, DK_Project.DL.Interfaces.IAuthorRepository authorRepository)
//        {

//            _bookRepository = bookRepository;
//            _logger = logger;
//            _authorRepository = authorRepository;
//        }

//        public async Task<Book?> AddBook(Book book)
//        {
//            if (await _authorRepository.GetById(book.AuthorId)== null)
//            {
//                _logger.LogInformation("There is no such author");
//                return null;
//            }
//            return await _bookRepository.AddBook(book);
//        }

//        public async Task<Book?> DeleteBook(int bookId)
//        {
//            return await _bookRepository.DeleteBook(bookId);
//        }

//        public async Task<IEnumerable<Book>> GetAllBooks()
//        {
//            return await _bookRepository.GetAllBooks();
//        }

//        public async Task<Book?> GetById(int id)
//        {
//            return await _bookRepository.GetById(id);
//        }

//        public async Task<Book> UpdateBook(Book book)
//        {
//            return await _bookRepository.UpdateBook(book);
//        }
//        public async Task<Book> GetBookByName(string book)
//        {
//            return await _bookRepository.GetBookByName(book);
//        }
//    }
//}
