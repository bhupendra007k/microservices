using Microsoft.AspNetCore.Mvc;
using shoppingCart.Models;
using shoppingCart.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shoppingCart.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController:ControllerBase
    {
        private readonly ICartRepository _repository;
        public CartController(ICartRepository repository)
        {
            _repository = repository;
        }


        [HttpPost("addtocart/{Id}")]
        public async Task<IActionResult> AddToCart(Guid Id)
        {
            try
            {
                var res = await _repository.AddToCart(Id);
                if (res == null)
                {
                    return Ok($"No product Found with id {Id}");
                }
                return Ok(res);

            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
          
        }

        [HttpPost("adduser")]

        public async Task<IActionResult> AddUserToDb(User user)
        {
            var result=await _repository.AddUserToDb(user);
           
            return Ok(result);
        }

        [HttpPost("addproduct")]

        public async Task<IActionResult> AddProducts(Product product)
        {
            var res = await _repository.AddProduct(product);
            return Ok(res);
        }

        [HttpDelete("removefromcart/{id}")]

        public async Task<IActionResult> RemoveProductFromCart(Guid Id)
        {
            var res = await _repository.RemoveProductFromCart(Id);
            return Ok(res);
        }

        [HttpGet("viewcart")]

        public async Task<IActionResult> ViewCart()
        {
            var res = await _repository.ViewUserCart();
            return Ok(res);
        }

       


    }
}
