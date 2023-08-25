using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public interface IUserService
    {
        Task<List<User>> GetAllUser();
        Task<User> GetUserById(string id);

        Task<User> AddUser(User user);

        Task<User> UpdateUser(User user);

        Task DeleteUser(string userId);

        Task<List<User>> SearchUser(string term);
    }
}
