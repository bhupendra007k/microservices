
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using productservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace productservice.NewFolder
{
    public class InventoryClient : IInventoryClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public InventoryClient(HttpClient httpClient,IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

        }

        public async Task<bool> SendProductToInventory(Product product)
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
            var response = await _httpClient.PostAsync($"{_configuration["InventoryService"]}/inventory/add", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("synced");
                return true;
            }
            else{
                return false;

            }
        }

        public async Task<bool> DeleteProductFromInventory(Guid Id)
        {
            /*string json = JsonConvert.SerializeObject(Id);
            var content = new StringContent(json, Encoding.UTF8, "application/json");*/
            var response = await _httpClient.DeleteAsync($"{_configuration["InventoryService"]}/inventory/delete/"+Id);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
