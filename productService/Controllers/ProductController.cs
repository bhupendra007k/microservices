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

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromForm] Product product)
        {
            var res=await _productRepository.AddProduct(product);
            return Ok(res);

        }

        [HttpPost("update/{id}")]

        public async Task<IActionResult> UpdateProduct([FromForm] Product product,Guid id)
        {
            var res = await _productRepository.UpdateProduct(id, product);
            return Ok(res);
        }
        
            


    }
}
