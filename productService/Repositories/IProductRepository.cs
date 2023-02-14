using productservice.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace productservice.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();

        Task<Product> AddProduct(Product product);

        Task<Product> UpdateProduct(Guid productId, Product product);

        Task<ProductCategory> GetAllProductsByCategory(Guid Id);

        bool RemoveProduct(Guid Id);
        Task<Product> GetProductById(Guid Id);
    }
}