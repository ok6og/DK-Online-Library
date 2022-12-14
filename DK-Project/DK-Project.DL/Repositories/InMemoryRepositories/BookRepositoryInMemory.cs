using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.DL.Interfaces;
using DK_Project.Models.Models;

namespace DK_Project.DL.Repositories.InMemoryRepositories
{
    public class BookRepositoryInMemory
    {
        private static List<Book> _books = new List<Book>()
        {
            new Book()
            {
                Id=1,
                Title="tom i jeri",
                AuthorId = 1
            },
            new Book()
            {
                Id=2,
                Title="tom i soyer",
                AuthorId = 1
            },
            new Book()
            {
                Id=3,
                Title="tom jones",
                AuthorId = 2
            }
        };
        public BookRepositoryInMemory()
        {

        }
        public async Task<Book?> AddBook(Book book)
        {
            try
            {
                _books.Add(book);

            }
            catch (Exception)
            {
                return null;
            }
            return book;
        }

        public async Task<Book?> DeleteBook(int bookId)
        {
            if (bookId <= 0) return null;

            var book = _books.FirstOrDefault(x => x.Id == bookId);
            _books.Remove(book);
            return book;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return _books;
        }

        public async Task<Book?> GetById(int id)
        {
            return _books.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Book> UpdateBook(Book book)
        {
            var existingBook = _books.FirstOrDefault(x => x.Id == book.Id);
            if (existingBook == null) return null;

            _books.Remove(existingBook);
            _books.Add(book);

            return book;
        }

        public async Task<Book> GetBookByName(string book)
        {
            var existingBook = _books.FirstOrDefault(x => x.Title == book);
            return existingBook;
        }
    }
}
