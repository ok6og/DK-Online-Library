using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using DK_Project.DL.Interfaces;
using DK_Project.DL.Repositories.InMemoryRepositories;
using DK_Project.DL.Repositories.MsSql;

namespace DK_Project.Extensions
{
    public static class RepoExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            //services.AddSingleton<IPersonRepository, PersonRepository>();
            services.AddSingleton<IEmployeesRepository, EmployeeRepository>();
            services.AddSingleton<IUserRepository,UserRepository>();
            services.AddSingleton<IAuthorRepository, AuthorRepository>();
            services.AddSingleton<IBookRepository, BookRepository>();

            services.AddSingleton<IEmployeeService, UserInfoEmployeeService>();
            services.AddSingleton<IUserInfoService, UserInfoEmployeeService>();

            return services;
        }
    }
}
