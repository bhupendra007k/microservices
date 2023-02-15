using inventory.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inventory.Repositories
{
    public interface IInventoryRepository
    {
        Task<Product> AddProductToInventory(Product product);
        Task<IList<Inventory>> GetAllInventory();
    }
}