using productservice.Models;
using System;
using System.Threading.Tasks;

namespace productservice.NewFolder
{
    public interface IInventoryClient
    {
        Task<bool> SendProductToInventory(Product product);
        Task<bool> DeleteProductFromInventory(Guid Id);
    }
}