using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.Repositories.EFRepository
{
    public class EFUserRepository:IRepository<User, string>
    {
        BMSContext context;
        public EFUserRepository(BMSContext context)
        {
            this.context = context;
        }



        public async Task<User> Add(User entity)
        {
            context.Users.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }



        public async Task Delete(string id)
        {
            var Users = await GetById(id);
            context.Users.Remove(Users);
            await context.SaveChangesAsync();
        }



        public async Task<List<User>> GetAll()
        {
            await Task.CompletedTask;
            return context.Users.ToList();
        }



        public async Task<List<User>> GetAll(Func<User, bool> predicate)
        {
            await Task.CompletedTask;
            var q = from Members in context.Users
                    where predicate(Members)
                    select Members;



            return q.ToList();
        }



        public async Task<User> GetById(string email)
        {
            await Task.CompletedTask;



            return context.Users.FirstOrDefault(a => a.Email == email);
        }



        public async Task<User> Update(User entity, Action<User, User> mergeOldNew)
        {
            var Users = await GetById(entity.Email);
            if (Users != null)
            {
                mergeOldNew(Users, entity);
                await context.SaveChangesAsync();
            }



            return entity;
        }
    }

}

