using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.BL.Interfaces;
using DK_Project.DL.Interfaces;
using DK_Project.DL.Repositories.InMemoryRepositories;
using DK_Project.Models.Models;

namespace BookStore.BL.Services
{
    public class AuthorService : IAuthorService
    {
        public readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public Author? AddUser(Author user)
        {
            return _authorRepository.AddUser(user);
        }

        public Author? DeleteUser(int userId)
        {
            return _authorRepository.DeleteUser(userId);
        }

        public IEnumerable<Author> GetAllUsers()
        {
            return _authorRepository.GetAllUsers();
        }

        public Author? GetById(int id)
        {
            return _authorRepository.GetById(id);
        }

        public Author UpdateUser(Author user)
        {
            return _authorRepository.UpdateUser(user);
        }
    }
}
