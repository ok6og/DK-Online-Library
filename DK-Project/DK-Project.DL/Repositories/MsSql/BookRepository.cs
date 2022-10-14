using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class BookRepository : IBookRepository
    {
        private readonly ILogger<BookRepository> _logger;
        private readonly IConfiguration _configuration;


        public BookRepository(ILogger<BookRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Book?> AddBook(Book book)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.ExecuteScalarAsync("INSERT INTO [Books] (AUTHORID, TITLE, LASTUPDATED, PRICE,QUANTITY) VALUES (@AuthorId, @Title, @LastUpdated, @Price,@Quantity)",
                        new { Title = book.Title, AuthorId = book.AuthorId, LastUpdated = DateTime.Now, Price = book.Price, Quantity = book.Quantity });
                    return book;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddBook)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<Book?> DeleteBook(int bookId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var deletedBook = await GetById(bookId);
                    var result = await conn.ExecuteAsync("DELETE FROM BOOKS WHERE ID = @Id",
                        new { Id = bookId });
                    return deletedBook;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DeleteBook)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT * FROM BOOKS WITH(NOLOCK)";
                    await conn.OpenAsync();
                    return await conn.QueryAsync<Book>(query);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllBooks)}: {ex.Message}", ex);
            }
            return Enumerable.Empty<Book>();
        }

        public async Task<Book> GetBookByName(string book)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstOrDefaultAsync<Book>("SELECT * FROM BOOKS WITH(NOLOCK) WHERE Title = @Name", new { Name = book });
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetBookByName)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<Book?> GetById(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstOrDefaultAsync<Book>("SELECT * FROM BOOKS WITH(NOLOCK) WHERE ID = @Id", new { Id = id });
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetById)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<Book> UpdateBook(Book book)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.ExecuteAsync("UPDATE BOOKS SET AUTHORID = @AuthorId, TITLE = @Title, LASTUPDATED = @LastUpdated, QUANTITY = @Quantity, PRICE = @Price WHERE ID = @Id",
                        new { Title = book.Title, AuthorId = book.AuthorId, LastUpdated = book.LastUpdated, Price = book.Price, Quantity = book.Quantity, Id = book.Id });
                    return book;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateBook)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<IEnumerable<Book>> GetAuthorBooks(int authorId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryAsync<Book>("SELECT * FROM BOOKS WITH(NOLOCK) WHERE AUTHORID = @Id", new { Id = authorId });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAuthorBooks)}: {ex.Message}", ex);
            }
            return Enumerable.Empty<Book>();
        }

        public async Task<bool> DoesAuthorHaveBooks(int authorId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var books = await conn.QueryAsync<Book>("SELECT * FROM BOOKS WITH(NOLOCK) WHERE AUTHORID = @Id", new { Id = authorId });
                    return books.Any();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DoesAuthorHaveBooks)}: {ex.Message}", ex);
            }
            return false;
        }
    }
}
