using shoppingCart.Models;
using System;
using System.Threading.Tasks;

namespace shoppingCart.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> AddToCart(Guid Id);
        Task<User> AddUserToDb(User request);
        Task<Product> AddProduct(Product product);
        Task<string> RemoveProductFromCart(Guid Id);
        Task<Cart> ViewUserCart();
    }
}