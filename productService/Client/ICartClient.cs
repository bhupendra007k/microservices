using productservice.Models;
using System.Threading.Tasks;

namespace productservice.Client
{
    public interface ICartClient
    {
        Task<bool> SendProductToCartService(Product product);
    }
}