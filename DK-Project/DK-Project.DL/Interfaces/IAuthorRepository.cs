using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.Models.Models;

namespace DK_Project.DL.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author?> AddUser(Author user);
        Task<Author?> DeleteUser(int userId);
        Task<IEnumerable<Author>> GetAllUsers();
        Task<Author?> GetById(int id);
        Task<Author> UpdateUser(Author user);
        Task<Author> GetAuthorByName(string user);
        
    }
}
