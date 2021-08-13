﻿using Microsoft.EntityFrameworkCore;
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

        public Task AddProductAsync(Product product)
        {
            _context.Add(product);
            return _context.SaveChangesAsync();
        }

        public Task AddCustomerAsync(Customer customer)
        {
            _context.Add(customer);
            return _context.SaveChangesAsync();
        }

        // TODO: recibir customer id de en vez de nombre
        public async Task AddAddressToCustomerAsync(string name, Address address)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.FirstName == name);

            if (customer == null)
            {
                throw new InvalidOperationException("El cliente no existe");
            }

            customer.Address = address;
            await _context.SaveChangesAsync();
        }

        public Task<bool> IsCustomerRegisteredAsync(string name)
        {
            return _context.Customers.AnyAsync(c => c.FirstName == name);
        }

        public Task<bool> HasProductAsync()
        {
            return _context.Products.AnyAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public Task DeleteProductAsync(int productId)
        {
            var product = _context.Products.FirstOrDefaultAsync(c => c.Id == productId);

            _context.Remove(product);
            return _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(int id, string name, string description, decimal price)
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (product == null)
                return;

            product.Update(name, description, price);
            await _context.SaveChangesAsync();
        }
    }
}