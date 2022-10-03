using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using DK_Project.DL.Interfaces;
using DK_Project.DL.Repositories.InMemoryRepositories;

namespace DK_Project.Extensions
{
    public static class RepoExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IPersonRepository, PersonRepository>();
            services.AddSingleton<IAuthorRepository, AuthorRepository>();
            services.AddSingleton<IBookRepository, BookRepository>();
            return services;
        }
    }
}
