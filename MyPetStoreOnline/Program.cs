using MyPetStoreOnline.Data;
using MyPetStoreOnline.Entities;
using MyPetStoreOnline.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPetStoreOnline
{
    internal class Program
    {
        private static readonly List<Product> shopProducts = new List<Product> {
                new Product("Product 1", "The description 1", 9.99m),
                new Product("Product 2", "The description 2", 19.99m),
                new Product("Product 3", "The description 3", 19.99m)
            };

        private static ShopService _shopService;
        private static ReportService _reportService;

        private static async Task Main(string[] args)
        {
            var context = new ApplicationContext();
            _shopService = new ShopService(context);
            _reportService = new ReportService(context);

            await AddProductsIfNeededAsync();

            var showMenu = true;
            while (showMenu)
            {
                showMenu = await MainMenuAsync();
            }
        }

        private static async Task<bool> MainMenuAsync()
        {
            Console.Clear();
            Console.WriteLine("Elige una opción:");
            Console.WriteLine("1. Registrar cliente");
            Console.WriteLine("2. Registrar dirección para un cliente");
            Console.WriteLine("3. Actualizar el teléfono de un cliente");
            Console.WriteLine("4. Ver productos");
            Console.WriteLine("5. Ver clientes");

            Console.WriteLine("6. Agregar un producto a la orden de un cliente");
            Console.WriteLine("7. Actualizar la cantidad de un producto en la orden de un cliente");
            Console.WriteLine("8. Terminar compra de un cliente");

            // ReportService.cs
            Console.WriteLine("9. Obtener las ordenes en el mes con productos, total y gran total");
            Console.WriteLine("10. Miembro con más compras");
            Console.WriteLine("11. Producto más vendido");
            Console.WriteLine("11. País con más ventas");

            Console.WriteLine("12. Salir");

            var showMenu = true;
            switch (Console.ReadLine())
            {
                case "1":
                    await RegisterCustomerAsync();
                    break;

                case "2":
                    await ChangeAddressForCustomerAsync();
                    break;

                case "3":
                    break;

                case "4":
                    break;

                case "5":
                    break;

                case "9":
                    await PrintOrdersOfTheMonthAsync();
                    break;

                case "10":
                    await PrintCustomerWithMostBuysAsync();
                    break;

                default:
                    showMenu = false;
                    break;
            }
            Console.ReadLine();

            return showMenu;
        }

        private static Task ChangeAddressForCustomerAsync()
        {
            throw new NotImplementedException();
        }

        private static Task PrintCustomerWithMostBuysAsync()
        {
            throw new NotImplementedException();
        }

        private static async Task PrintOrdersOfTheMonthAsync()
        {
            var orders = await _reportService.GetOrdersOfTheMonthAsync();

            foreach (var order in orders)
            {
                Console.WriteLine($"order id: {order.Id} customer: {order.Customer} total: {order.Total:C2}");

                foreach (var product in order.Products)
                {
                    Console.WriteLine($"product: {product.Name} quantity: {product.Quantity}");
                }
            }

            Console.WriteLine($"Gran total: {orders.Sum(order => order.Total):C2}");
        }

        private static async Task AddProductsIfNeededAsync()
        {
            // si no hay productos en la base de datos, agregarlos
            if (!await _shopService.HasProductAsync())
                foreach (var p in shopProducts)
                {
                    try
                    {
                        await _shopService.AddProductAsync(p);
                        Console.WriteLine($"El producto {p.Id} {p.Name} con precio {p.Price} ha sido agregado");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
        }

        private static async Task ChangeAddressForCustomer()
        {
            Console.WriteLine("Actualizar la dirección para el cliente con nombre:");
            var nameForAddressChange = Console.ReadLine();

            var exists = await _shopService.IsCustomerRegisteredAsync(nameForAddressChange);

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
                    await _shopService.AddAddressToCustomerAsync(nameForAddressChange, address);
                    Console.WriteLine($"La dirección para el cliente {nameForAddressChange} ha sido agregada");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private static async Task RegisterCustomerAsync()
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
                await _shopService.AddCustomerAsync(customer);
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