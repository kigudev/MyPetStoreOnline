using MyPetStoreOnline.Data;
using MyPetStoreOnline.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MyPetStoreOnline
{
    internal class Program
    {
        private static readonly List<Product> shopProducts = new List<Product> {
                new Product("Product 1", "The description 1", 9.99m),
                new Product("Product 2", "The description 2", 19.99m),
                new Product("Product 3", "The description 3", 19.99m)
            };

        private static readonly ShopService _shopService = new();

        private static void Main(string[] args)
        {
            AddProductsIfNeeded();

            var showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Elige una opción:");
            Console.WriteLine("1. Registrar cliente");
            Console.WriteLine("2. Registrar dirección para un cliente");
            Console.WriteLine("3. Actualizar el teléfono de un cliente");
            Console.WriteLine("4. Ver productos");
            Console.WriteLine("5. Ver clientes");
            Console.WriteLine("6. Salir");

            var showMenu = true;
            switch (Console.ReadLine())
            {
                case "1":
                    RegisterCustomer();
                    break;
                case "2":
                    ChangeAddressForCustomer();
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
                default:
                    showMenu = false;
                    break;
            }
            Console.ReadLine();

            return showMenu;
        }


        private static void AddProductsIfNeeded()
        {
            // si no hay productos en la base de datos, agregarlos
            if (!_shopService.HasProduct())
                foreach (var p in shopProducts)
                {
                    try
                    {
                        _shopService.AddProduct(p);
                        Console.WriteLine($"El producto {p.Id} {p.Name} con precio {p.Price} ha sido agregado");
                    }catch(Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
        }

        private static void ChangeAddressForCustomer()
        {
            Console.WriteLine("Actualizar la dirección para el cliente con nombre:");
            var nameForAddressChange = Console.ReadLine();

            var exists = _shopService.IsCustomerRegistered(nameForAddressChange);

            if (exists)
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

                try
                {
                    var address = new Address(street, state, country, pc, city);
                    _shopService.AddAddressToCustomer(nameForAddressChange, address);
                    Console.WriteLine($"La dirección para el cliente {nameForAddressChange} ha sido agregada");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private static void RegisterCustomer()
        {
            Console.WriteLine("Ingresa el nombre del cliente:");
            var name = Console.ReadLine();
            Console.WriteLine("Ingresa el apellido del cliente:");
            var lastName = Console.ReadLine();
            Console.WriteLine("Ingresa el correo:");
            var email = Console.ReadLine();

            try
            {
                var customer = new Customer(name, lastName, email);
                _shopService.AddCustomer(customer);
                Console.WriteLine($"El cliente {customer.FirstName} {customer.LastName} se registró");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
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