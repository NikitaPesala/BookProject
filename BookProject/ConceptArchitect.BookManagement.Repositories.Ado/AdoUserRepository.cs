using ConceptArchitect.Data;
using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.Repositories.Ado
{
    public class AdoUserRepository : IRepository<User, string>
    {
        DbManager db;
        public AdoUserRepository(DbManager db)
        {
            this.db = db;
        }


        public async Task<User> Add(User user)
        {
            var query = $"insert into users(email, password, name, photo) " +
                              $"values('{user.Email}','{user.Password}','{user.Name}','{user.Photo}')";

            await db.ExecuteUpdateAsync(query);

            return user;
        }

        public async Task Delete(string id)
        {
            await db.ExecuteUpdateAsync($"delete from users where email='{id}'");
        }

        private User userExtractor(IDataReader reader)
        {
            return new User()
            {
                Email = reader["email"].ToString(),
                Password = reader["password"].ToString(),
                Name = reader["name"].ToString(),
                Photo = reader["profile_photo"].ToString()

            };
        }

        public async Task<List<User>> GetAll()
        {
            return await db.QueryAsync("select * from users", userExtractor);
        }

        public async Task<List<User>> GetAll(Func<User, bool> predicate)
        {
            var users = await GetAll();

            return (from user in users
                    where predicate(user)
                    select user).ToList();

        }

        public async Task<User> GetById(string id)
        {
            return await db.QueryOneAsync($"select * from users where email='{id}'", userExtractor);
        }

        public async Task<User> Update(User entity, Action<User, User> mergeOldNew)
        {
            var olduser = await GetById(entity.Email);
            if (olduser != null)
            {
                mergeOldNew(olduser, entity);
                var query = $"update users set " +
                            $"Email='{olduser.Email}', " +
                            $"Password='{olduser.Password}', " +
                            $"Name='{olduser.Name}', " +
                            $"Photo='{olduser.Photo}'";

                await db.ExecuteUpdateAsync(query);
            }

            return entity;



        }
    }
}
