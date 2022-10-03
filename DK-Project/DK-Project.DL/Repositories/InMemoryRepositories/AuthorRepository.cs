using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DK_Project.DL.Interfaces;
using DK_Project.Models.Models;

namespace DK_Project.DL.Repositories.InMemoryRepositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private static List<Author> _users = new List<Author>()
        {
            new Author()
            {
                Id=1,
                Name = "Paolo",
                Age = 69,
                DateOfBirth= DateTime.Now,
                Nickname = "bezdomnika"
            },
            new Author()
            {
                Id=2,
                Name = "Koelio",
                Age = 39,
                DateOfBirth = DateTime.Now,
                Nickname = "valeri suhiq"
            },
            new Author()
            {
                Id=3,
                Name = "Paisii",
                Age = 30,
                DateOfBirth = DateTime.Now,
                Nickname = "boyang"
            }
        };
        public Author? AddUser(Author user)
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

        public Author? DeleteUser(int userId)
        {
            if (userId <= 0) return null;

            var user = _users.FirstOrDefault(x => x.Id == userId);
            _users.Remove(user);
            return user;
        }

        public IEnumerable<Author> GetAllUsers()
        {
            return _users;
        }

        public Author? GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        public Author UpdateUser(Author user)
        {
            var existingUser = _users.FirstOrDefault(x => x.Id == user.Id);
            if (existingUser == null) return null;

            _users.Remove(existingUser);
            _users.Add(user);

            return user;
        }

        public Author GetAuthorByName(string user)
        {
            var existingAuthor = _users.FirstOrDefault(x => x.Name == user);
            return existingAuthor;
        }
    }
}
