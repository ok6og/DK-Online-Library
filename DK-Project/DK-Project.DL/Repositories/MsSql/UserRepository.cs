using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DK_Project.DL.Interfaces;
using DK_Project.Models.Models;
using DK_Project.Models.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DK_Project.DL.Repositories.MsSql
{
    public class UserRepository : IUserRepository,IUserPasswordStore<UserInfo>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthorRepository> _logger;


        public UserRepository(IConfiguration configuration, ILogger<AuthorRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IdentityResult> CreateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var query =
                        @"INSERT INTO [USERINFO] (USERID, DISPLAYNAME, USERNAME, EMAIL, PASSWORD, CREATEDDATE) VALUES(@UserId, @DisplayName, @UserName, @Email, @Password, @CreateDate)";
                    var result = await conn.ExecuteScalarAsync(query, user);
                    return IdentityResult.Success;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(CreateAsync)}: {ex.Message}", ex);
            }
            return IdentityResult.Failed();
        }

        public Task<IdentityResult> DeleteAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<UserInfo> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM USERINFO WITH(NOLOCK) WHERE USERID = @Id", new { Id = userId });
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(FindByIdAsync)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<UserInfo> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM USERINFO WITH(NOLOCK) WHERE DISPLAYNAME = @Name", new { Name = normalizedUserName });
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(FindByNameAsync)}: {ex.Message}", ex);
            }
            return null;
        }

        public Task<string> GetNormalizedUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetUserIdAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.UserId.ToString());
        }

        public async Task<UserInfo?> GetUserInfo(string userName, string password)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM USERINFO WITH(NOLOCK) WHERE Email = @Username AND PASSWORD =@Password",
                        new { Username = userName,Password = password});
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetUserInfo)}: {ex.Message}", ex);
            }
            return null;
        }

        public Task<string> GetUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(UserInfo user, string normalizedName, CancellationToken cancellationToken)
        {
            user.UserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(UserInfo user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(UserInfo user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
