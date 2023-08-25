using System;
using ConceptArchitect.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.Repositories.EFRepository
{
    public class EFReviewsRepository : IRepository<Reviews, string>
    {
        BMSContext context;
        public EFReviewsRepository(BMSContext context)
        {
            this.context = context;
        }



        public async Task<Reviews> Add(Reviews entity)
        {
            context.Reviews.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }



        public async Task Delete(string id)
        {
            var Reviews = await GetById(id);
            context.Reviews.Remove(Reviews);
            await context.SaveChangesAsync();
        }



        public async Task<List<Reviews>> GetAll()
        {
            await Task.CompletedTask;
            return context.Reviews.ToList();
        }



        public async Task<List<Reviews>> GetAll(Func<Reviews, bool> predicate)
        {
            await Task.CompletedTask;
            var q = from Reviews in context.Reviews
                    where predicate(Reviews)
                    select Reviews;



            return q.ToList();
        }



        public async Task<Reviews> GetById(string id)
        {
            await Task.CompletedTask;



            return context.Reviews.FirstOrDefault(a => a.Id == id);
        }



        public async Task<Reviews> Update(Reviews entity, Action<Reviews, Reviews> mergeOldNew)
        {
            var Reviews = await GetById(entity.Id);
            if (Reviews != null)
            {
                mergeOldNew(Reviews, entity);
                await context.SaveChangesAsync();
            }



            return entity;
        }
    }
}
