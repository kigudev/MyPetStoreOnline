using MyPetStoreOnline.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPetStoreOnline.Data
{
    /// <summary>
    /// Se va a encargar de realizar las operaciones en la base de datos
    /// </summary>
    public class ShopService
    {
        private readonly ApplicationContext _context;

        public ShopService(ApplicationContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();

            Console.WriteLine($"El producto {product.Id} {product.Name} con precio {product.Price} ha sido agregado");
        }

        public void AddCustomer(Customer customer)
        {
            _context.Add(customer);
            _context.SaveChanges();

            Console.WriteLine($"El cliente {customer.FirstName} {customer.LastName} se registró");
        }

        public void AddAddressToCustomer(string name, Address address)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.FirstName == name);

            if(customer == null)
            {
                Console.WriteLine("El cliente no existe");
                return;
            }

            customer.Address = address;
            _context.SaveChanges();

            Console.WriteLine($"La dirección para el cliente {name} ha sido agregada");
        }

        public bool IsClientRegistered(string name)
        {
            return _context.Customers.Any(c => c.FirstName == name);
        }
    }
}
