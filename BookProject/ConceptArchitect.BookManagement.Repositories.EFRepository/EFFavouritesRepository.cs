using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.Repositories.EFRepository
{
    public abstract class EFFavouritesRepository : IRepository<Favourites,string>
    {
        BMSContext context;
        public EFFavouritesRepository(BMSContext context)
        {
            this.context = context;
        }



        public async Task<Favourites> Add(Favourites entity)
        {
            context.Favourites.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }



        public async Task Delete(string id)
        {
            var Favourites = await GetById(id);
            context.Favourites.Remove(Favourites);
            await context.SaveChangesAsync();
        }



        public async Task<List<Favourites>> GetAll()
        {
            await Task.CompletedTask;
            return context.Favourites.ToList();
        }



        public async Task<List<Favourites>> GetAll(Func<Favourites, bool> predicate)
        {
            await Task.CompletedTask;
            var q = from Favourites in context.Favourites
                    where predicate(Favourites)
                    select Favourites;



            return q.ToList();
        }



        public async Task<Favourites> GetById(string id)
        {
            await Task.CompletedTask;



            return context.Favourites.FirstOrDefault(a => a.Id == id);
        }


        public async Task<Favourites> Update(Favourites entity, Action<Favourites, Favourites> mergeOldNew)
        { 
        //{
        //    var Book = await GetById(entity.Id);
        //    if (Book != null)
        //    {
        //        mergeOldNew(Book, entity);
        //        await context.SaveChangesAsync();
        //    }



            return null;
        }

    }
}

