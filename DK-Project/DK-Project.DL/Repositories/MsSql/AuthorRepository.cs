using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DK_Project.DL.Interfaces;
using DK_Project.Models.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DK_Project.DL.Repositories.MsSql
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ILogger<AuthorRepository> _logger;
        private readonly IConfiguration _configuration;
        public AuthorRepository(ILogger<AuthorRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<Author?> AddUser(Author user)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.ExecuteScalarAsync("INSERT INTO [Authors] (NAME, AGE, DATEOFBIRTH, NICKNAME) VALUES (@Name, @Age, @DateOfBirth, @Nickname)",
                        new {Name = user.Name, Age = user.Age, DateOfBirth = user.DateOfBirth, Nickname = user.Nickname});
                    return user;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddUser)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<Author?> DeleteUser(int userId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var deletedUser = await GetById(userId);
                    var result = await conn.ExecuteAsync("DELETE FROM AUTHORS WHERE ID = @Id",
                        new {Id = userId});
                    return deletedUser;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DeleteUser)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<IEnumerable<Author>> GetAllUsers()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT * FROM Authors WITH(NOLOCK)";
                    await conn.OpenAsync();
                    return await conn.QueryAsync<Author>(query);                   
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllUsers)}: {ex.Message}", ex);
            }
            return Enumerable.Empty<Author>();
        }

        public async Task<Author> GetAuthorByName(string user)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstOrDefaultAsync<Author>("SELECT * FROM Authors WITH(NOLOCK) WHERE NAME = @Name", new { Name = user });
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAuthorByName)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<Author?> GetById(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstOrDefaultAsync<Author>("SELECT * FROM Authors WITH(NOLOCK) WHERE ID = @Id", new { Id = id });
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetById)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<Author> UpdateUser(Author user)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.ExecuteAsync("UPDATE AUTHORS SET NAME = @Name, AGE = @Age, DATEOFBIRTH = @DateOfBirth, NICKNAME = @NickName WHERE ID = @Id",
                        new {Id = user.Id, Name = user.Name, Age = user.Age, DateOfBirth = user.DateOfBirth, Nickname = user.Nickname });
                    return user;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateUser)}: {ex.Message}", ex);
            }
            return null;
        }   
    }
}
