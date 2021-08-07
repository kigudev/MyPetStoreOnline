using MyPetStoreOnline.Data;
using MyPetStoreOnline.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyPetStoreOnline
{
    internal class Program
    {
        private static List<Product> shopProducts = new List<Product> {
                new Product("Product 1", "The description 1", 9.99m),
                new Product("Product 2", "The description 2", 19.99m),
                new Product("Product 3", "The description 3", 19.99m)
            };

        private static void Main(string[] args)
        {
            using var context = new ApplicationContext();
            var shopService = new ShopService(context);

            // si no hay productos en la base de datos, agregarlos
            if (!context.Products.Any())
                foreach (var p in shopProducts)
                {
                    shopService.AddProduct(p);
                }

            Console.WriteLine("Ingresa el nombre del cliente:");
            var name = Console.ReadLine();
            Console.WriteLine("Ingresa el apellido del cliente:");
            var lastName = Console.ReadLine();
            Console.WriteLine("Ingresa el correo:");
            var email = Console.ReadLine();

            var customer = new Customer(name, lastName, email);
            shopService.AddCustomer(customer);

            Console.WriteLine("Actualizar la dirección para el cliente con nombre:");
            var nameForAddressChange = Console.ReadLine();

            var exists = shopService.IsClientRegistered(nameForAddressChange);

            if(exists)
            {
                Console.WriteLine("Calle");
                var street = Console.ReadLine();
                Console.WriteLine("City");
                var city = Console.ReadLine();
                Console.WriteLine("Estado");
                var state = Console.ReadLine();
                Console.WriteLine("País");
                var country = Console.ReadLine();
                Console.WriteLine("Código postal");
                var pc = Console.ReadLine();
                var address = new Address(street, state, country, pc, city);

                shopService.AddAddressToCustomer(nameForAddressChange, address);
            }
           
        }

        private static void MyPetStoreInMemoryTests()
        {
            // registrarme como cliente
            var customer = new Customer("Rene", "Quiñones", "rene@gmail.com");
            // agregar mi direccion y telefono
            customer.Address = new Address("Islas Calles 123", "Baja California", "MXN", "21000", "Mexicali");
            customer.AddOrUpdatePhone("6861234567");
            // buscar producto en listado
            var theProductIWant = shopProducts.FirstOrDefault(c => c.Name == "Product 2");
            // agregar a orden producto 1 y cantidad
            var order = new Order(customer.Id, theProductIWant.Id, 3);
            // agregar producto 2
            var theOtherProductIWant = shopProducts.FirstOrDefault(c => c.Name == "Product 1");
            order.AddProduct(theOtherProductIWant.Id, 10);
            // modificar la cantidad del producto 1
            order.ChangeProductQuantity(theProductIWant.Id, 5);
            // orden realizada
            order.Complete();
        }
    }
}