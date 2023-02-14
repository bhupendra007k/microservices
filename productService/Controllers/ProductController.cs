using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using productservice.Models;
using productservice.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace productservice.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductController:ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductRepository productRepository,ILogger<ProductController> logger)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpGet("getall")]
        public  async Task<IList<Product>> Get()
        {

            var result =await _productRepository.GetProducts();
            return result;

        }

        [HttpGet("cat/{id}")]
        public async Task<IActionResult> GetAllProductsByCategory(Guid Id)
        {
            var res = await _productRepository.GetAllProductsByCategory(Id);

            return Ok(res);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetProductById(Guid Id)
        {
            var res = await _productRepository.GetProductById(Id);
            return Ok(res);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromForm] Product product)
        {
            var res=await _productRepository.AddProduct(Guid.Empty,product);
            return Ok(res);

        }

        [HttpPost("update/{id}")]

        public async Task<IActionResult> UpdateProduct([FromForm] Product product,Guid id)
        {
            var res = await _productRepository.AddProduct(id, product);
            return Ok(res);
        }

        [HttpDelete("delete/{id}")]

        public IActionResult RemoveProduct(Guid Id)
        {
            var res = _productRepository.RemoveProduct(Id);
            if (res == true)
            {
                return Ok($"product with id:{Id} is removed successfully");
            }
            return NotFound($"No item with id:{Id} found");
        }







    }
}
