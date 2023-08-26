using ConceptArchitect.BookManagement;
using ConceptArchitect.BookManagement.Repositories.EFRepository;
using ConceptArchitect.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookProject.Extensions
{
    public static  class EFRepositoryExtensions
    {
        public static IServiceCollection AddEFBmsRepository(this IServiceCollection services)
        {
            //Add the EF Context (BMSContext)
            //WE don't add context object using AddSingleton
            //services.AddSingleton<DbContext, BMSContext>();


            services.AddDbContext<BMSContext>((serviceProvider, contextBuilder) =>
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                var connectionString = config.GetConnectionString("EFContext");
                contextBuilder.UseSqlServer(connectionString);
            });

            services.AddTransient<IRepository<Author, string>, EFAuthorRepository>();

            services.AddTransient<IRepository<User,string>,EFUserRepository>();

            services.AddTransient<IBookRepository<Book, Favourites, string>,EFBookRepository>();

            return services;

        }
    }
}
