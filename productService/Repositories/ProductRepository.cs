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


    /*    public List<Product> products = new List<Product>
        {
            new Product
            {
                Name="Denim Shirt",
                Description="askjdnasklndlk",
                ProductCategory=new ProductCategory{Name="Shirt",Description="Men's Shirt"},
                Price=20.10,
                Discount=new Discount
                {
                     Name="Red black sale",
                     Description="adkmasldm",
                     DiscountPercent=20,
                     Active=true
                }
            },
            new Product
            {
                Name="Denim jacket",
                Description="askjdnasklndlk",
                ProductCategory=new ProductCategory{Name="Jacket",Description="Men's Shirt"},
                Price=20.10,
                Discount=new Discount
                {
                    Name="Red black sale",
                    Description="adkmasldm",
                    DiscountPercent=20,
                    Active=true
                }
            }

        };*/



        public async Task<List<Product>> GetProducts()
        {
          
            var list = await _context.Products.ToListAsync();
            /*_logger.LogInformation($"helu:{list.ToArray()[0].ProductCategory}");*/
            return list;
        }

        public async Task<Product> AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
           /* await _context.ProductCategory.AddAsync(product.Category);
            await _context.Discount.AddAsync(product.Discount);*/
            await _context.SaveChangesAsync();
            return product;

        }


        public async Task<Product> UpdateProduct(Guid productId,Product product)
        {
            var res = _context.Products.Where(x => x.Id == productId);
            await _context.Products.FindAsync(productId);
            if (res!=null)
            {
                 _context.Products.Update(product);
            }
            await _context.SaveChangesAsync();
            return product;
        }






    }
}
