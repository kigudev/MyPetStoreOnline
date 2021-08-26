using MyPetStore.Client.Services.Interfaces;
using MyPetStore.Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MyPetStore.Client.Services.Implementations
{
    public class OrderApi : IOrderApi
    {
        private readonly HttpClient _client;

        public OrderApi(HttpClient client)
        {
            _client = client;
        }

        public async Task AddProductToOrder(int productId, int quantity = 1)
        {
            var dto = new ProductOrderRequest
            {
                ProductId = productId,
                Quantity = quantity
            };
            await _client.PostAsJsonAsync("api/orders", dto);
        }

        public Task<List<OrderDto>> GetMyOrders() => _client.GetFromJsonAsync<List<OrderDto>>("api/orders/mine");
    }
}