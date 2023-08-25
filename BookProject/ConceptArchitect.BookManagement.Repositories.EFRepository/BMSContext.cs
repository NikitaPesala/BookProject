using Microsoft.EntityFrameworkCore;



namespace ConceptArchitect.BookManagement.Repositories.EFRepository
{
    public class BMSContext : DbContext
    {
        public BMSContext(DbContextOptions options) : base(options)
        {
        }



        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Reviews> Reviews { get; set; }

        public DbSet<Favourites> Favourites { get; set; }
    }
}