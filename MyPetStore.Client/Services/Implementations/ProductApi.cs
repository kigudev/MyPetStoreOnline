using MyPetStore.Client.Services.Interfaces;
using MyPetStore.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyPetStore.Client.Services.Implementations
{
    public class ProductApi : IProductApi
    {
        private readonly HttpClient _client;

        public ProductApi(HttpClient client)
        {
            _client = client;
        }

        public Task<List<ProductDto>> GetAllAsync()
        {
            return _client.GetFromJsonAsync<List<ProductDto>>("api/products");
        }
    }
}
