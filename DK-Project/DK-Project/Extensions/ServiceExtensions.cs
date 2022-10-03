using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using DK_Project.DL.Repositories.InMemoryRepositories;

namespace DK_Project.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IPersonService, PersonService>();
            services.AddSingleton<IAuthorService, AuthorService>();
            services.AddSingleton<IBookService, BookService>();
            return services;
        }
    }
}
