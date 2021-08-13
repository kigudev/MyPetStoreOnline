using Microsoft.EntityFrameworkCore;
using MyPetStoreOnline.Data;
using MyPetStoreOnline.Entities;
using MyPetStoreOnline.Models;
using MyPetStoreOnline.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPetStoreOnline.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly ApplicationContext _context;

        public ReportService(ApplicationContext context)
        {
            _context = context;
        }

        public Task<List<OrderDto>> GetOrdersOfTheMonthAsync()
        {
            return _context.Orders
                .Where(c => c.OrderPlaced > DateTime.Now.AddMonths(-1))
                .Select(c => new OrderDto
                {
                    Id = c.Id,
                    Customer = c.Customer.FirstName + " " + c.Customer.LastName,
                    Total = c.ProductOrders.Sum(p => p.Product.Price * p.Quantity),
                    Products = c.ProductOrders.Select(p => new ProductDto
                    {
                        Name = p.Product.Name,
                        Quantity = p.Quantity
                    })
                }).ToListAsync();
        }

        public Task<Customer> GetCustomerWithMostBuysAsync()
        {
            return _context.Customers
                .OrderByDescending(c => c.Orders.Count)
                .FirstOrDefaultAsync();
        }

        public async Task<Product> GetMostSoldProductAsync()
        {
            return (await _context.ProductOrders
                .GroupBy(c => c.Product)
                .OrderByDescending(c => c.Count())
                .FirstOrDefaultAsync())?
                .Key;

            // product 1 - lista de ProductOrders - 4
            // product 2 - lista de ProductOrders - 2
            // product 3 - lista de ProductOrders - 6
        }

        public async Task<string> GetCountryWithMostSellsAsync()
        {
            return (await _context.Customers
                .GroupBy(c => c.Address.Country)
                .OrderByDescending(c => c.Sum(d => d.Orders.Count))
                .FirstOrDefaultAsync())?
                .Key;

            // mxn - customers - total 10 - orders - 4
            // usa - customers - total 14 - orders - 6
        }
    }
}