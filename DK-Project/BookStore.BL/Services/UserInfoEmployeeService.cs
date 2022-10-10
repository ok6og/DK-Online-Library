using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.BL.Interfaces;
using DK_Project.DL.Interfaces;
using DK_Project.DL.Repositories.MsSql;
using DK_Project.Models.Models.Users;

namespace BookStore.BL.Services
{
    public class UserInfoEmployeeService : IEmployeeService
    {
        public readonly IEmployeesRepository _employeeRepo;

        public UserInfoEmployeeService(IEmployeesRepository employeeService)
        {
            _employeeRepo = employeeService;
        }

        public async Task AddEmployee(Employee employee)
        {
            await _employeeRepo.AddEmployee(employee);
        }

        public async Task<bool> CheckEmployee(int id)
        {
            return await _employeeRepo.CheckEmployee(id);
        }

        public async Task DeleteEmployee(int id)
        {
            await _employeeRepo.DeleteEmployee(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeeDetails()
        {
            return await _employeeRepo.GetEmployeeDetails();
        }

        public async Task<Employee?> GetEmployeeDetails(int id)
        {
            return await _employeeRepo.GetEmployeeDetails(id);
        }

        public async Task UpdateEmployee(Employee employee)
        {
            await _employeeRepo.UpdateEmployee(employee);
        }
    }

}
