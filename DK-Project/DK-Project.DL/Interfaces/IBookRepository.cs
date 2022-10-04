using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.Models.Models;

namespace DK_Project.DL.Interfaces
{
    public interface IBookRepository
    {
        Task<Book?> AddBook(Book book);
        Task<Book?> DeleteBook(int bookId);
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book?> GetById(int id);
        Task<Book> UpdateBook(Book user);
        Task<Book> GetBookByName(string book);
        Task<IEnumerable<Book>> GetAuthorBooks(int authorId);
        Task<bool> DoesAuthorHaveBooks(int authorId);

    }
}
