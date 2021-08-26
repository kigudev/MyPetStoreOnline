using MyPetStore.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPetStore.Client.Services.Interfaces
{
    public interface IOrderApi
    {
        Task AddProductToOrder(int productId, int quantity = 1);
        Task<List<OrderDto>> GetMyOrders();
    }
}
