using System.Collections.Generic;
using System.Threading.Tasks;
using ShopsRUs.Domain.Entity;

namespace ShopsRUs.Infrastructure.Services.UserService
{
    public interface IUsersService
    {
        Task<List<User>> GetUsers();
        Task<User> GetUserById(long id);
        Task<User> GetUserByName(string name);
        Task<User> GetUserByNamAndPhone(string name, string phoneNumber);
        Task CreateUser(User user);
        Task DeActivateUserById(long id);
    }
}