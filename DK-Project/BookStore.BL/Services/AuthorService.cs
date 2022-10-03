using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.BL.Interfaces;
using DK_Project.DL.Interfaces;
using DK_Project.DL.Repositories.InMemoryRepositories;
using DK_Project.Models.Models;
using DK_Project.Models.Requests;
using Microsoft.Extensions.Logging;

namespace BookStore.BL.Services
{
    public class AuthorService : IAuthorService
    {
        public readonly IAuthorRepository _authorRepository;
        private readonly ILogger<AuthorService> _logger;


        public AuthorService(IAuthorRepository authorRepository, ILogger<AuthorService> logger)
        {
            _authorRepository = authorRepository;
            _logger = logger;
        }

        public Author? AddUser(Author user)
        {
            _logger.LogInformation("Adding author");
            return _authorRepository.AddUser(user);
        }

        public Author? DeleteUser(int userId)
        {
            try
            {
                _logger.LogInformation("Deleting user");
                throw new Exception();
                return _authorRepository.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError("You are dumb");
                return null;
            }

        }

        public IEnumerable<Author> GetAllUsers()
        {
            try
            {
                _logger.LogInformation("Getting all users");
                return _authorRepository.GetAllUsers();

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Deleting user");
                return null;
            }
        }

        public Author? GetById(int id)
        {
            return _authorRepository.GetById(id);
        }

        public Author UpdateUser(Author user)
        {
            return _authorRepository.UpdateUser(user);
        }
        public Author GetAuthorByName(string user)
        {
            return _authorRepository.GetAuthorByName(user);
        }
    }
}
