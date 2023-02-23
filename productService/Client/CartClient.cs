using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using productservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace productservice.Client
{
    public class CartClient : ICartClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CartClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

        }

        public async Task<bool> SendProductToCartService(Product product)
        {
            /*var options = new JsonSerializerOptions
            {
                MaxDepth = Int32.MaxValue, // set a higher max depth value
                ReferenceHandler = ReferenceHandler.Preserve,

            };*/
            var json = JsonConvert.SerializeObject(product, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            /*var json = JsonSerializer.Serialize(product, options);*/
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_configuration["CartService"]}/cart/addproduct", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("synced");
                return true;
            }
            else
            {
                return false;

            }
        }
    }
}
