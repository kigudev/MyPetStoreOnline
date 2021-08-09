using MyPetStoreOnline.Entities;
using System;
using System.Linq;

namespace MyPetStoreOnline.Data
{
    /// <summary>
    /// Se va a encargar de realizar las operaciones en la base de datos
    /// </summary>
    public class ShopService : IDisposable
    {
        private readonly ApplicationContext _context;

        public ShopService()
        {
            _context = new ApplicationContext();
        }

        public void AddProduct(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }

        public void AddCustomer(Customer customer)
        {
            _context.Add(customer);
            _context.SaveChanges();
        }

        public void AddAddressToCustomer(string name, Address address)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.FirstName == name);

            if (customer == null)
            {
                throw new InvalidOperationException("El cliente no existe");
            }

            customer.Address = address;
            _context.SaveChanges();
        }

        public bool IsCustomerRegistered(string name)
        {
            return _context.Customers.Any(c => c.FirstName == name);
        }

        public bool HasProduct()
        {
            return _context.Products.Any();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}