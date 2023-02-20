using inventory.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inventory.Repositories
{
    public interface IInventoryRepository
    {
        Task<Product> AddProductToInventory(Product product);
        Task<IList<Inventory>> GetAllInventory();
        Task<Inventory> GetInventoryById(Guid Id);
        Task<string> DeleteProductsFromInventory(Guid Id);
    }
}