using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
	public interface IBookService
	{
		Task<List<Book>> GetAllBooks();
		Task<Book> GetBookById(string id);

		Task<Book> AddBook(Book book);

		Task<Book> UpdateBook(Book book);

		Task DeleteBook(string bookId);

		Task<List<Book>> SearchBooks(string term);

		Task<List<Book>> GetAllFavs(string userId);

		Task<Book> AddFavs(Book book, string userId);

		Task<bool> IsBookInUserFavorites(string bookId, string userId);

		Task DeleteFav(string bookId, string userId);
	}
}
