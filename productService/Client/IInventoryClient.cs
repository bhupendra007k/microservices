using productservice.Models;
using System.Threading.Tasks;

namespace productservice.NewFolder
{
    public interface IInventoryClient
    {
        Task SendProductToInventory(Product product);
    }
}