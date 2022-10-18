using DK_Project.Models.Models;

namespace DK_Project.DL.Repositories.InMemoryRepositories
{
    public interface IPersonRepository
    {
        Person? AddUser(Person user);
        Person? DeleteUser(int userId);
        IEnumerable<Person> GetAllUsers();
        Person? GetById(int id);
        Person UpdateUser(Person user);
    }
}