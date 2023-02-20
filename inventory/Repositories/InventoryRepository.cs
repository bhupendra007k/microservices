using inventory.Data;
using inventory.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
            /*if (_context.Products.Any())
            {
                var persist = _context.Products.Where(name => name.Name == product.Name).FirstOrDefault().Name;
            }*/
            if (product.Id != Guid.Empty)
            {
                Inventory productInventory=new Inventory 
                {
                    Id=_context.Products.Where(x=>x.Name==product.Name).Any()? _context.Products.Where(x=>x.Name==product.Name).FirstOrDefault().InventoryId:Guid.NewGuid(),
                    ProductName =product.Name,
                    Quantity=_context.Inventory.Where(x=>x.ProductName==product.Name).Any()?_context.Inventory.Where(x=>x.ProductName==product.Name).FirstOrDefault().Quantity+1:quantity+1,
                    /*Products = (IList<Product>)product*/

                };
               /* await _context.Inventory.AddAsync(productInventory);
                await _context.SaveChangesAsync();*/
                product.InventoryId = productInventory.Id;
                var flag = _context.Inventory.Where(x=>x.Id==productInventory.Id).Any();
                if (flag)
                {
                    //Inventory already exist
                    //First find the inventory from db
                    var inventoryToUpdate = _context.Inventory.FirstOrDefault(inventory => inventory.Id == productInventory.Id);
                    //Change the property
                    inventoryToUpdate.Quantity = productInventory.Quantity;
                    //_context.Inventory.Update(productInventory);
                    _context.SaveChanges();
                }
                else
                {
                    await _context.Inventory.AddAsync(productInventory);
                }

                await _context.SaveChangesAsync();
                await _context.Products.AddAsync(product);
            }
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<IList<Inventory>> GetAllInventory()
        {
            var inventories = await _context.Inventory.ToListAsync();
            return inventories;
        }

        public async Task<Inventory> GetInventoryById(Guid Id)
        {
            var inventory = await _context.Inventory.FindAsync(Id);
            return inventory;
        }

        public async Task<string> DeleteProductsFromInventory(Guid Id)
        {
            var product = _context.Products.Where(x=>x.Id==Id).FirstOrDefault();

            if (product != null)
            {
                var inventoryId = product.InventoryId;
                var inventory=_context.Inventory.Where(x=>x.Id==inventoryId).FirstOrDefault();
                inventory.Quantity--;
                if (inventory.Quantity == 0)
                {
                    _context.Inventory.Remove(inventory);

                }
               
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return ($"product with id {Id} is deleted successfully");

        }


    }
}
