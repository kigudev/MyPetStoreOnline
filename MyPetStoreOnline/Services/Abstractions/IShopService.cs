using MyPetStoreOnline.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyPetStoreOnline.Services.Abstractions
{
    public interface IShopService
    {
        Task AddProductAsync(Product product);

        Task AddCustomerAsync(Customer customer);

        Task AddAddressToCustomerAsync(int id, Address address);

        Task<bool> IsCustomerRegisteredAsync(int id);

        Task<bool> HasProductAsync();
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerAsync(int id);
        Task DeleteProductAsync(int productId);
        Task<Product> GetProductAsync(int id);
        Task UpdateProductAsync(int id, string name, string description, decimal price,  string imageUrl = null);
        Task DeleteCustomerAsync(int customerId);
        Task<IEnumerable<Customer>> GetCustomersWithAddressesAsync();
        Task UpdateCustomerAsync(int id, string firstName, string lastName, string email);
    }
}