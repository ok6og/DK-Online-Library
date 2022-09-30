using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IPersonService
    {
        Person? AddUser(Person user);
        Person? DeleteUser(int userId);
        IEnumerable<Person> GetAllUsers();
        Person? GetById(int id);
        Person UpdateUser(Person user);
    }
}
