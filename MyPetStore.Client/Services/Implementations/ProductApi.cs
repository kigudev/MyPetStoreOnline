using MyPetStore.Client.Services.Interfaces;
using MyPetStore.Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
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

        public Task<List<ProductDto>> GetAllAsync(string search, string brand, string order)
        {
            return _client.GetFromJsonAsync<List<ProductDto>>($"api/products?search={search}&brand={brand}&order={order}");
        }
    }
}