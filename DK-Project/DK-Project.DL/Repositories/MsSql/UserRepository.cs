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
    public class UserRepository : IUserPasswordStore<UserInfo>, IUserRoleStore<UserInfo>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserRepository> _logger;
        private readonly IPasswordHasher<UserInfo> _passwordHasher;


        public UserRepository(IConfiguration configuration, ILogger<UserRepository> logger, IPasswordHasher<UserInfo> passwordHasher)
        {
            _configuration = configuration;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        public Task AddToRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> CreateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var query =
                        @"INSERT INTO [USERINFO] (DISPLAYNAME, USERNAME, EMAIL, PASSWORD, CREATEDDATE) VALUES(@DisplayName, @UserName, @Email, @Password, @CreatedDate)";
                    user.Password = _passwordHasher.HashPassword(user, user.Password);
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
                    var result = await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM USERINFO WITH(NOLOCK) WHERE USERNAME = @Name", new { Name = normalizedUserName });
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

        public async Task<string> GetPasswordHashAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await conn.OpenAsync(cancellationToken);

            return await conn.QueryFirstOrDefaultAsync<string>("SELECT Password FROM USERINFO WITH(NOLOCK) WHERE USERID = @UserId", new { user.UserId });
        }

        public async Task<IList<string>> GetRolesAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync(cancellationToken);
                    var result =
                        await conn.QueryAsync<string>("SELECT r.RoleName FROM Roles r WHERE r.Id IN (SELECT ur.Id FROM UserRoles ur WHERE ur.UserId = @UserId )", new { UserId = user.UserId });
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error in {nameof(UserRepository.GetRolesAsync)}:{ex.Message}");
                    return null;
                }
            }
        }

        public async Task<string?> GetUserIdAsync(UserInfo user, CancellationToken cancellationToken)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM USERINFO WITH(NOLOCK) WHERE UserId = @UserId", new { UserId = user.UserId });
                    return result?.UserId.ToString();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(FindByNameAsync)}: {ex.Message}", ex);
            }
            return null;
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

        public Task<IList<UserInfo>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> HasPasswordAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return !string.IsNullOrEmpty(await GetPasswordHashAsync(user, cancellationToken));
        }

        public Task<bool> IsInRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(UserInfo user, string normalizedName, CancellationToken cancellationToken)
        {
            user.UserName = normalizedName;
            return Task.CompletedTask;
        }

        public async Task SetPasswordHashAsync(UserInfo user, string passwordHash, CancellationToken cancellationToken)
        {
            await using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await conn.OpenAsync(cancellationToken);

            await conn.ExecuteAsync("UPDATE UserInfo SET Password = @PasswordHash WHERE UserId = @userId", new {user.UserId,passwordHash});
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
