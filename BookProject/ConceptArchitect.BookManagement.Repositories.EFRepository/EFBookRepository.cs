using ConceptArchitect.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.Repositories.EFRepository
{
    public class EFBookRepository : IBookRepository<Book, Favourites, string>
    {
        BMSContext context, context1;
        public EFBookRepository(BMSContext context)
        {
            this.context = context;
        }

        public async Task<Book> Add(Book entity)
        {
            context.Books.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }



        public async Task Delete(string id)
        {
            var Book = await GetById(id);
            context.Books.Remove(Book);
            await context.SaveChangesAsync();
        }



        public async Task<List<Book>> GetAll()
        {
            await Task.CompletedTask;
            return context.Books.ToList();
        }



        public async Task<List<Book>> GetAll(Func<Book, bool> predicate)
        {
            await Task.CompletedTask;
            var q = from Book in context.Books
                    where predicate(Book)
                    select Book;



            return q.ToList();
        }



        public async Task<Book> GetById(string id)
        {
            await Task.CompletedTask;



            return context.Books.FirstOrDefault(a => a.Id == id);
        }



        public async Task<Book> Update(Book entity, Action<Book, Book> mergeOldNew)
        {
            var Book = await GetById(entity.Id);
            if (Book != null)
            {
                mergeOldNew(Book, entity);
                await context.SaveChangesAsync();
            }



            return entity;
        }

        public async Task<Book> Fav(Book book, string userId)
        {
            var fav = new Favourites
            {
                Id = $"{book.Title}-{userId}",
                UserEmail = userId,
                Book = book,
            };
            context.Favourites.Add(fav);
            await context.SaveChangesAsync();

            return book;
        }

        public async Task<List<Book>> GetAllFav(string userId)
        {
            await Task.CompletedTask;
			/*var favoriteBooks = await context1.Favourites
                .Where(favorite => favorite.UserEmail == userId)
                .Select(favorite => favorite.Book)
                .ToListAsync();*/
			var favoriteBooks = await context.Favourites
				.Where(favorite => favorite.UserEmail == userId)
				.Select(favorite => favorite.Book)
				.ToListAsync();

			return favoriteBooks;
        }



        public async Task<List<Book>> GetAllFav(string userId, Func<Book, bool> predicate)
        {
            var favoriteBooks = await GetAllFav(userId);

            return favoriteBooks.Where(predicate).ToList();
        }

        public async Task DeleteFav(string id, string user_id)
        {
            var favorite = await context.Favourites
                .FirstOrDefaultAsync(f => f.Book.Id == id && f.UserEmail == user_id);

            if (favorite != null)
            {
                context.Favourites.Remove(favorite);
                await context.SaveChangesAsync();
            }
        }
    }
}

