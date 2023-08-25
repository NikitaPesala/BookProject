using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class PersistentUserService : IUserService
    {
        IRepository<User, string> repository;

        //constructor based DI
        public PersistentUserService(IRepository<User, string> repository)
        {
            this.repository = repository;
        }


        public async Task<User> AddUser(User entity)
        {
            //perform some validation if needed
            if (entity == null)
                throw new InvalidDataException("User can't be null");


            return await repository.Add(entity);
        }


        public async Task DeleteUser(string entityId)
        {
            await repository.Delete(entityId);
        }

        public async Task<List<User>> GetAllUser()
        {
            return await repository.GetAll();
        }

        public async Task<User> GetUserById(string id)
        {
            return await repository.GetById(id);
        }

        public async Task<List<User>> SearchUser(string term)
        {
            term = term.ToLower();

            return await repository.GetAll(a => a.Email.ToLower().Contains(term) || a.Name.ToLower().Contains(term));
        }

        public async Task<User> UpdateUser(User entity)
        {

            return await repository.Update(entity, (old, newDetails) =>
            {
                old.Email = newDetails.Email;
                old.Password = newDetails.Password;
                old.Name = newDetails.Name;
                old.Photo = newDetails.Photo;
            });
        }
    }
}
