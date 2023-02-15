
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

        public InventoryClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task SendProductToInventory(Product product)
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
            var response = await _httpClient.PostAsync("https://localhost:3001/inventory/add", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("synced");
            }
        }
    }
}
