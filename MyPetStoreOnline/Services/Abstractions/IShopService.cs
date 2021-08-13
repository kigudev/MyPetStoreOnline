using MyPetStoreOnline.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyPetStoreOnline.Services.Abstractions
{
    public interface IShopService
    {
        Task AddProductAsync(Product product);

        Task AddCustomerAsync(Customer customer);

        Task AddAddressToCustomerAsync(string name, Address address);

        Task<bool> IsCustomerRegisteredAsync(string name);

        Task<bool> HasProductAsync();
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task DeleteProductAsync(int productId);
        Task<Product> GetProductAsync(int id);
        Task UpdateProductAsync(int id, string name, string description, decimal price);
    }
}