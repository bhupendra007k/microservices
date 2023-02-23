using System.Threading.Tasks;
using userService.Models;

namespace userService.Client
{
    public interface ICartClient
    {
        Task<bool> SendUSerDetails(User user);
    }
}