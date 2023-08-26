using ConceptArchitect.Data;
using ConceptArchitect.Utils;
using System.Data;
using System.Net.Http;

namespace ConceptArchitect.BookManagement.Repositories.Ado
{
	public class AdoBookRepository: IBookRepository<Book, Favourites, string>
	{
		DbManager db;
		private object books;
		
		public AdoBookRepository(DbManager db)
		{
			this.db = db;
		}


		public async Task<Book> Add(Book book)
		{
			var query = $"insert into books(id,title,description,author_id,cover_photo) " +
							  $"values('{book.Id}','{book.Title}','{book.Description}','{book.AuthorId}','{book.Cover}')";

			await db.ExecuteUpdateAsync(query);

			return book;
		}

		public async Task Delete(string id)
		{
			await db.ExecuteUpdateAsync($"delete from books where id='{id}'");
		}

		private Book BookExtractor(IDataReader reader)
		{
            Author author = new Author();
            return new Book()
			{
				
				Id = reader["id"].ToString(),
				Title = reader["title"].ToString(),
				Description = reader["description"].ToString(),

				
                 AuthorId = reader["author_id"].ToString() ,
            
           
				Cover = reader["cover_photo"].ToString()

			};
		}

		public async Task<List<Book>> GetAll()
		{
			return await db.QueryAsync("select * from books", BookExtractor);
		}

		public async Task<List<Book>> GetAll(Func<Book, bool> predicate)
		{
			var books = await GetAll();

			return (from book in books
					where predicate(book)
					select book).ToList();

		}

		public async Task<Book> GetById(string id)
		{
			return await db.QueryOneAsync($"select * from books where id='{id}'", BookExtractor);
		}

		public async Task<Book> Update(Book entity, Action<Book, Book> mergeOldNew)
		{
			var oldBook = await GetById(entity.Id);
			if (oldBook != null)
			{
				mergeOldNew(oldBook, entity);
				var query = $"update books set " +
							$"Title='{oldBook.Title}', " +
							$"Description='{oldBook.Description}', " +
							$"Author_Id='{oldBook.AuthorId}', " +
							$"Cover_Photo='{oldBook.Cover}' " +
							$"where id='{oldBook.Id}'";

				await db.ExecuteUpdateAsync(query);
			}

			return entity;
		}

        public async Task<Book> Fav(Book book, string userId)
        {
            var query = $"insert into favorites(BookId, UserEmail) " +
                              $"values('{book.Id}','{userId}')";

            await db.ExecuteUpdateAsync(query);

            return book;
        }

        public async Task<List<Book>> GetAllFav(string userId)
        {
            return await db.QueryAsync("Select * from Books where id in(Select BookId from Favorites);", BookExtractor);
        }

        public async Task<List<Book>> GetAllFav(string userId, Func<Book, bool> predicate)
        {
            var books = await GetAllFav(userId);

            return (from book in books
                    where predicate(book)
                    select book).ToList();

        }

        public async Task DeleteFav(string id, string user_id)
        {
            await db.ExecuteUpdateAsync($"delete from favorites where BookId='{id}' and UserEmail='{user_id}'");
        }
    }
}
