using Microsoft.EntityFrameworkCore;
using MyPetStoreOnline.Data;
using MyPetStoreOnline.Entities;
using MyPetStoreOnline.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPetStoreOnline.Services.Implementations
{
    /// <summary>
    /// Se va a encargar de realizar las operaciones en la base de datos
    /// </summary>
    public class ShopService : IShopService
    {
        private readonly ApplicationContext _context;

        public ShopService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task AddAddressToCustomerAsync(int id, Address address)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                throw new InvalidOperationException("El cliente no existe");
            }

            customer.Address = address;
            await _context.SaveChangesAsync();
        }

        public Task<bool> IsCustomerRegisteredAsync(int id)
        {
            return _context.Customers.AnyAsync(c => c.Id == id);
        }

        public Task<bool> HasProductAsync()
        {
            return _context.Products.AnyAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Customer>> GetCustomersWithAddressesAsync()
        {
            return await _context.Customers.Include(c => c.Address).ToListAsync();
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == productId);

            _context.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == customerId);

            _context.Remove(customer);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateProductAsync(int id, string name, string description, decimal price, string imageUrl)
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (product == null)
                return;

            product.Update(name, description, price);

            if (!string.IsNullOrEmpty(imageUrl))
                product.AddOrUpdateImage(imageUrl);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(int id, string firstName, string lastName, string email)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null)
                return;

            customer.Update(firstName, lastName, email);

            await _context.SaveChangesAsync();
        }
    }
}