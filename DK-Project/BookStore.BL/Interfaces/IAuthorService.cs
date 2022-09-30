using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IAuthorService
    {
        Author? AddUser(Author user);
        Author? DeleteUser(int userId);
        IEnumerable<Author> GetAllUsers();
        Author? GetById(int id);
        Author UpdateUser(Author user);
    }
}
