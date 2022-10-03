using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IBookService
    {
        Book? AddBook(Book book);
        Book? DeleteBook(int bookId);
        IEnumerable<Book> GetAllBooks();
        Book? GetById(int id);
        Book UpdateBook(Book user);
    }
}
