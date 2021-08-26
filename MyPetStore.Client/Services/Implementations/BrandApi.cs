using MyPetStore.Client.Services.Interfaces;
using MyPetStore.Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MyPetStore.Client.Services.Implementations
{
    public class BrandApi : IBrandApi
    {
        private readonly HttpClient _client;

        public BrandApi(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<BrandDto>> GetAllAsync()
        {
            return await _client.GetFromJsonAsync<List<BrandDto>>("api/brands");
        }
    }
}