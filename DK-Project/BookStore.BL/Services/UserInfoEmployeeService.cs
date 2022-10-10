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

        public Task AddEmployee(Employee employee)
        {
            return _employeeRepo.AddEmployee(employee);
        }

        public Task<bool> CheckEmployee(int id)
        {
            return _employeeRepo.CheckEmployee(id);
        }

        public Task DeleteEmployee(int id)
        {
            return _employeeRepo.DeleteEmployee(id);
        }

        public Task<IEnumerable<Employee>> GetEmployeeDetails()
        {
            return _employeeRepo.GetEmployeeDetails();
        }

        public Task<Employee?> GetEmployeeDetails(int id)
        {
            return _employeeRepo.GetEmployeeDetails(id);
        }

        public Task UpdateEmployee(Employee employee)
        {
            return _employeeRepo.UpdateEmployee(employee);
        }
    }

}
