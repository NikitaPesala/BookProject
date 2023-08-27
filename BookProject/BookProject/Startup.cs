using ConceptArchitect.BookManagement;

using BookProject.Extensions;
using BookProject;
using Microsoft.AspNetCore.Authentication;
using ConceptArchitect.BookManagement.Repositories.EFRepository;
using Microsoft.EntityFrameworkCore;

namespace BookProject
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {

            services.AddControllersWithViews();

            services.AddEFBmsRepository();

            services.AddTransient<IAuthorService, PersistentAuthorService>();

            services.AddTransient<IBookService, PersistentBookService>();

            services.AddTransient<IUserService, PersistentUserService>();

            services.AddTransient<IReviewsService, PersistentReviewsService>();

            return services;
        }

        public static IApplicationBuilder ConfigureMiddlewares(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            else
            {
                app.UseOnUrl("/admin/createdb", async context =>
                {
                    var bmsContext = context.RequestServices.GetService<BMSContext>();
                    await bmsContext.Database.EnsureCreatedAsync();
                    context.Response.Redirect("/");
                });
            }

            app.UseOnUrl("/admin/seed", async context =>
            {
                var authorService = context.RequestServices.GetService<IAuthorService>();
                var bookService = context.RequestServices.GetService<IBookService>();

                
                await authorService.AddAuthor(new Author()
                {
                    Id = "dinkar",
                    Name = "Ramdhari Singh Dinkar",
                    Biography = "The National Poet of India",
                    Photo = "https://pbs.twimg.com/profile_images/1269658848777785345/2bY35KV0_400x400.jpg",
                    Tags = "poet, historian",
                    BirthDate = new DateTime(1906, 1, 1),
                    DeathDate = new DateTime(1976, 1, 1)
                });
                await authorService.AddAuthor(new Author()
                {
                    Id = "mahatma-gandhi",
                    Name = "Mahamta Gandhi",
                    Biography = "The Father of the Nation for India",
                    Photo = "https://pbs.twimg.com/media/FAqPzrrUYAM8pCu.jpg",
                    BirthDate = new DateTime(1869, 10, 2),
                    Tags = "freedom fighter, social reformer",
                    DeathDate = new DateTime(1948, 1, 30)
                });
                

            });


            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            app.MapControllerRoute(
               name: "default",
               pattern: "",
               defaults: new { controller = "Home", action = "Home" }

            );


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
           name: "Add",
           pattern: "author/add",
           defaults: new { controller = "Author", action = "Add" }
           );

            return app;
        }
    }
}



