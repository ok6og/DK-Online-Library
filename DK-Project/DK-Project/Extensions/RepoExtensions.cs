using BookStore.BL.Interfaces;
using BookStore.BL.Kafka;
using BookStore.BL.Services;
using DK_Project.DL.Interfaces;
using DK_Project.DL.Repositories.InMemoryRepositories;
using DK_Project.DL.Repositories.MsSql;
using Microsoft.IdentityModel.Tokens;

namespace DK_Project.Extensions
{
    public static class RepoExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IEmployeesRepository, EmployeeRepository>();
            services.AddSingleton<IAuthorRepository, AuthorRepository>();
            services.AddSingleton<IBookRepository, BookRepository>();
            services.AddSingleton<IEmployeeService, UserInfoEmployeeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddHostedService<KafkaConsumer<int,string>>();

            return services;
        }
    }
}
