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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DK_Project.DL.Repositories.MsSql
{
    public class EmployeeRepository : IEmployeesRepository
    {
        private readonly ILogger<EmployeeRepository> _logger;
        private readonly IConfiguration _configuration;

        public EmployeeRepository(IConfiguration configuration, ILogger<EmployeeRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task AddEmployee(Employee employee)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var query =
                        @"INSERT INTO [Employee] (EmployeeID, NationalIDNumber, EmployeeName, LoginID, JobTitle, BirthDate, MaritalStatus, Gender, HireDate, VacationHours, SickLeaveHours, rowguid, ModifiedDate) VALUES(@EmployeeID, @NationalIDNumber, @EmployeeName, @LoginID, @JobTitle, @BirthDate, @MaritalStatus, @Gender, @HireDate, @VacationHours, @SickLeaveHours, @rowguid, @ModifiedDate)";
                    var result = await conn.ExecuteScalarAsync(query,employee);
                    return;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddEmployee)}: {ex.Message}", ex);
            }
            return;
        }

        public async Task<bool> CheckEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteEmployee(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var deletedUser = await GetEmployeeDetails(id);
                    var result = await conn.ExecuteAsync("DELETE FROM EMPLOYEE WHERE EmployeeID = @Id",
                        new { Id = id });
                    return;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DeleteEmployee)}: {ex.Message}", ex);
            }
            return;

        }

        public async Task<IEnumerable<Employee>> GetEmployeeDetails()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT * FROM EMPLOYEE WITH(NOLOCK)";
                    await conn.OpenAsync();
                    return await conn.QueryAsync<Employee>(query);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetEmployeeDetails)}: {ex.Message}", ex);
            }
            return Enumerable.Empty<Employee>();
        }

        public async Task<Employee?> GetEmployeeDetails(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstOrDefaultAsync<Employee>("SELECT * FROM EMPLOYEE WITH(NOLOCK) WHERE EmployeeID = @Id", new { Id = id });
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetEmployeeDetails)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task UpdateEmployee(Employee employee)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    string query =
                       @"UPDATE [dbo].[Employee]
                       SET [EmployeeID] = @EmployeeID
                          ,[NationalIDNumber] = @NationalIDNumber
                          ,[EmployeeName] = @EmployeeName
                          ,[LoginID] = @LoginID
                          ,[JobTitle] = @JobTitle
                          ,[BirthDate] = @BirthDate
                          ,[MaritalStatus] = @MaritalStatus
                          ,[Gender] = @Gender
                          ,[HireDate] = @HireDate
                          ,[VacationHours] = @VacationHours
                          ,[SickLeaveHours] = @SickLeaveHours
                          ,[rowguid] = @rowguid
                          ,[ModifiedDate] = @ModifiedDate
                     WHERE EmployeeID = @EmployeeID";

                    var result = await conn.ExecuteAsync(query, employee);
                    return ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateEmployee)}: {ex.Message}", ex);
            }
            return;
        }
    }
}
