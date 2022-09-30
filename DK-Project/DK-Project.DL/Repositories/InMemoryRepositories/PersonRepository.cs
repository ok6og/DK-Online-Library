using DK_Project.Models;
using DK_Project.Models.Models;

namespace DK_Project.DL.Repositories.InMemoryRepositories
{

    public class PersonRepository : IPersonRepository
    {
        private static List<Person> _users = new List<Person>()
        {
            new Person()
            {
                Id=1,
                Name = "Avram",
                Age = 69,
            },
            new Person()
            {
                Id=2,
                Name = "Pulio",
                Age = 39,
            },
            new Person()
            {
                Id=3,
                Name = "Tervel",
                Age = 30,
            }
        };


        public PersonRepository()
        {
 
        }

        public IEnumerable<Person> GetAllUsers()
        {
            return _users;
        }

        public Person? GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        public Person? AddUser(Person user)
        {
            try
            {
                _users.Add(user);

            }
            catch (Exception)
            {
                return null;
            }
            return user;
        }

        public Person UpdateUser(Person user)
        {
            var existingUser = _users.FirstOrDefault(x => x.Id == user.Id);
            if (existingUser == null) return null;

            _users.Remove(existingUser);
            _users.Add(user);

            return user;
        }

        public Person? DeleteUser(int userId)
        {
            if (userId <= 0) return null;



            var user = _users.FirstOrDefault(x => x.Id == userId);
            _users.Remove(user);
            return user;



        }


    }
}