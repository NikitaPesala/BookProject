using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
	public class PersistentBookService: IBookService
	{
		IBookRepository<Book, Favourites, string> repository;
        IRepository<Favourites, string> repositoryFav;

        //constructor based DI
        public PersistentBookService(IBookRepository<Book, Favourites, string> repository)
		{
			this.repository = repository;
		}


		public async Task<Book> AddBook(Book book)
		{
			//perform some validation if needed
			if (book == null)
				throw new InvalidDataException("Book can't be null");

			if (string.IsNullOrEmpty(book.Id))
			{
				book.Id = await GenerateId(book.Title);
			}

			return await repository.Add(book);
		}

		private async Task<string> GenerateId(string title)
		{
			var id = title.ToLower().Replace(" ", "-");

			if (await repository.GetById(id) == null)
				return id;

			int d = 1;
			while (await repository.GetById($"{id}-{d}") != null)
				d++;

			return $"{id}-{d}";

		}

		public async Task DeleteBook(string id)
		{
			await repository.Delete(id);
		}

		public async Task<List<Book>> GetAllBooks()
		{
			return await repository.GetAll();
		}

		public async Task<Book> GetBookById(string id)
		{
			return await repository.GetById(id);
		}

		public async Task<List<Book>> SearchBooks(string term)
		{
			term = term.ToLower();

			return await repository.GetAll(a => a.Title.ToLower().Contains(term) || a.Description.ToLower().Contains(term));
		}

		public async Task<Book> UpdateBook(Book book)
		{

			return await repository.Update(book, (old, newDetails) =>
			{
				old.Id = newDetails.Id;
				old.Title = newDetails.Title;
				old.Description = newDetails.Description;
				//old.Authorid= newDetails.Author.Id;
				old.Cover = newDetails.Cover;
			});
		}

        public async Task<Book> AddFavs(Book book, string userId)
        {
            if (book == null)
            {
                throw new ArgumentException("Book not found");
            }

            // You need to have a method in the repository to add the book to the user's favorites
            await repository.Fav(book, userId);

            return book;
        }

        public async Task<List<Book>> GetAllFavs(string userId)
        {
            
            return await repository.GetAllFav(userId);
        }

        public async Task DeleteFav(string bookId, string userId)
        {
			await repository.DeleteFav(bookId, userId);
        }

		public async Task<bool> IsBookInUserFavorites(string bookId, string userId)
		{
			var userFavorites = await repository.GetAllFav(userId);
			return userFavorites.Any(favorite => favorite.Id == bookId);
		}

	}
}

