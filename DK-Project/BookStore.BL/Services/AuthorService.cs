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
        public readonly IBookRepository _bookRepository;


        public AuthorService(IAuthorRepository authorRepository, ILogger<AuthorService> logger, IBookRepository bookRepository)
        {
            _authorRepository = authorRepository;
            _logger = logger;
            _bookRepository = bookRepository;
        }

        public async Task<Author?> AddUser(Author user)
        {
            _logger.LogInformation("Adding author");
            var result = await _authorRepository.AddUser(user);
            return result;
        }

        public async Task<Author?> DeleteUser(int userId)
        {
            if (await _bookRepository.DoesAuthorHaveBooks(userId))
            {
                _logger.LogInformation("You cant delete an author with existing books.");
                return null;
            }
            _logger.LogInformation("Deleting author.");
            return await _authorRepository.DeleteUser(userId);
        }

        public async Task<IEnumerable<Author>> GetAllUsers()
        {
            _logger.LogInformation("Getting all authors.");
            return await _authorRepository.GetAllUsers();
        }

        public async Task<Author?> GetById(int id)
        {
            return await _authorRepository.GetById(id);
        }

        public async Task<Author> UpdateUser(Author user)
        {
            return await _authorRepository.UpdateUser(user);
        }
        public async Task<Author> GetAuthorByName(string user)
        {
            return await _authorRepository.GetAuthorByName(user);
        }
        public async Task<IEnumerable<Book>> GetAuthorBooks(int authorId)
        { 
            _logger.LogInformation($"Getting all author{authorId} books");
            if (await _bookRepository.DoesAuthorHaveBooks(authorId))
            {
                return await _bookRepository.GetAuthorBooks(authorId);
            }
            _logger.LogInformation("This Author has no books");
            return null;
        }
    }
}
