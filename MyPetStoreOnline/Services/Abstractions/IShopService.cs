using MyPetStore.Shared;
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
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<IEnumerable<OrderDto>> GetOrdersAsync(int customerId);
        Task<Customer> GetCustomerAsync(int id);
        Task DeleteProductAsync(int productId);
        Task AddUpdateAddressAsync(int id, Address address);
        Task<Product> GetProductAsync(int id);
        Task UpdateProductAsync(int id, string name, string description, decimal price,  string imageUrl = null);
        Task DeleteCustomerAsync(int customerId);
        Task<IEnumerable<Customer>> GetCustomersWithAddressesAsync();
        Task UpdateCustomerAsync(int id, string firstName, string lastName, string email);
        Task AddProductToOrderAsync(int customerId, int productId, int quantity);
        Task CompleteOrderAsync(int customerId);
        Task DeleteOrderAsync(int id);

        Task<IEnumerable<ProductBrand>> GetProductBrandsAsync();
        Task<IEnumerable<ProductType>> GetProductTypesAsync();
    }
}