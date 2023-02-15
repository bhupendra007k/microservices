using inventory.Data;
using inventory.Models;
using inventory.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace inventory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
       
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryController(InventoryContext context,IInventoryRepository inventoryRepository)
        {

            _inventoryRepository = inventoryRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToInventory(Product product)
        {
            var result = await _inventoryRepository.AddProductToInventory(product);
            return Ok(result);
        }

        [HttpGet("get")]

        public async Task<IList<Inventory>> GetInventory()
        {
            return await _inventoryRepository.GetAllInventory();
            
        }
       
    }
}
