using System.Collections.Generic;
using System.Threading.Tasks;
using userService.Models;

namespace userService.Repositories
{
    public interface IUserRepository
    {
        Task<IList<User>> GetAllUsers();
        Task<string> Login(User request);
    }
}