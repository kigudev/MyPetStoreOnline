using MyPetStoreOnline.Entities;
using MyPetStoreOnline.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyPetStoreOnline.Services.Abstractions
{
    public interface IReportService
    {
        Task<List<OrderDto>> GetOrdersOfTheMonthAsync();

        Task<Customer> GetCustomerWithMostBuysAsync();

        Task<Product> GetMostSoldProductAsync();

        Task<string> GetCountryWithMostSellsAsync();
    }
}