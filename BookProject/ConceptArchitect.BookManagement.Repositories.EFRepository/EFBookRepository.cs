using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.Repositories.EFRepository
{
    public class EFBookRepository : IRepository<Book,string>
    {
        BMSContext context;
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
    }
}

