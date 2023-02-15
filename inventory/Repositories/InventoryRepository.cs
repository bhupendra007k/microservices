using inventory.Data;
using inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly InventoryContext _context;

        public InventoryRepository(InventoryContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProductToInventory(Product product)
        {
            var quantity = 0;
            if (_context.Products.Any())
            {
                var persist = _context.Products.Where(name => name.Name == product.Name).FirstOrDefault().Name;
            }
            if (product.Id != Guid.Empty)
            {
                Inventory productInventory=new Inventory 
                {
                    Id=_context.Products.Any()? _context.Products.Where(name => name.Name == product.Name).FirstOrDefault().InventoryId:Guid.NewGuid(),
                    ProductName =product.Name,
                    Quantity=_context.Inventory.Any()?_context.Inventory.Where(name=>name.ProductName==product.Name).FirstOrDefault().Quantity+1:quantity+1,
                    /*Products = (IList<Product>)product*/

                };
                product.InventoryId = productInventory.Id;
                var flag = _context.Inventory.Where(x=>x.Id==productInventory.Id).Any();
                if (flag)
                {
                    _context.Inventory.Update(productInventory);
                    _context.SaveChanges();
                }
                else
                {
                    await _context.Inventory.AddAsync(productInventory);
                }

                await _context.SaveChangesAsync();

               /* var productName = _context.Inventory.Where(name => name.ProductName == product.Name).FirstOrDefault().ProductName;
                if (productName != null)
                {

                }*/

      

                
                await _context.Products.AddAsync(product);
            }
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<IList<Inventory>> GetAllInventory()
        {
            var Inventories = _context.Inventory.ToList();
            return Inventories;
        }
    }
}
