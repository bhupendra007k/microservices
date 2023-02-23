using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using userService.Models;

namespace userService.Client
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

        public async Task<bool> SendUSerDetails(User user)
        {
            var json = JsonConvert.SerializeObject(user, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_configuration["CartService"]}/cart/adduser", httpContent);
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
