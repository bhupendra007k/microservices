using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using productservice.Data;
using productservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace productservice.Repositories
{
    public class ProductRepository :IProductRepository 
    {
        private readonly DataContext _context;
        

        private readonly ILogger<ProductRepository> _logger;


        public ProductRepository(DataContext context,ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;   
            
        }
        public async Task<dynamic> GetProducts()
        {
          
            var list = await _context.Products.ToListAsync();
            var count= list.Count();

            return new { list,count};
        }

        public async Task<Product> AddProduct(Guid Id,Product product)
        {
 
            var category = _context.ProductCategory.Where(x => x.Name == product.ProductCategory).FirstOrDefault();
            var sale = _context.Discount.Where(x => x.Name == product.Sale).FirstOrDefault();
            if (sale != null)
            {
                product.DiscountId = sale.Id;
            }
            product.ProductCategoryId = category.Id;

            if (product.Id == null || product.Id == Guid.Empty)
            {
                _context.Products.Add(product);

            }

            else
            {
                _context.Products.Update(product);
            }

            await _context.SaveChangesAsync();
            return product;

        }


       /* public async Task<Product> UpdateProduct(Guid productId,Product product)
        {
            var res = _context.Products.Where(x => x.Id == productId).FirstOrDefault();
            await _context.Products.FindAsync(productId);
            if (res!=null)
            {
                 _context.Products.Update(product);
            }
            await _context.SaveChangesAsync();
            return product;
        }*/


        public async Task<ProductCategory> GetAllProductsByCategory(Guid Id)
        {
           var category=_context.ProductCategory.Where(x => x.Id == Id).FirstOrDefault();
           return category;
        }

        public bool RemoveProduct(Guid Id)
        {
            var product = _context.Products.Find(Id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<Product> GetProductById(Guid Id)
        {
            var product = await _context.Products.FindAsync(Id);
            return product;
        }






    }
}
