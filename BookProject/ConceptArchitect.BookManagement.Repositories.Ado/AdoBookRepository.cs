using ConceptArchitect.Data;
using ConceptArchitect.Utils;

using System.Data;

namespace ConceptArchitect.BookManagement.Repositories.Ado
{
	public class AdoBookRepository: IRepository<Book, string>
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
							  $"values('{book.Id}','{book.Title}','{book.Description}','{book.Author.Id}','{book.Cover}')";

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

				Author= new Author
                { Id = reader["author_id"].ToString() },
            
           
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
							$"Author_Id='{oldBook.Author.Id}', " +
							$"Cover_Photo='{oldBook.Cover}' " +
							$"where id='{oldBook.Id}'";

				await db.ExecuteUpdateAsync(query);
			}

			return entity;
		}
	}
}
