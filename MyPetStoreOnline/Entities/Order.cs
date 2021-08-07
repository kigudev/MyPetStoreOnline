using System;
using System.Collections.Generic;
using System.Linq;

namespace MyPetStoreOnline.Entities
{
    public class Order
    {
        public int Id { get; private set; }
        public DateTime OrderPlaced { get; private set; }
        public DateTime? OrderFulfilled { get; private set; }
        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public ICollection<ProductOrder> ProductOrders { get; private set; }

        Order()
        {
            // Usado por EF
        }

        public Order(int customerId, int productId, int quantity)
        {
            if (quantity <= 0)
                throw new InvalidOperationException("La cantidad tiene que ser mayor a cero");

            OrderPlaced = DateTime.Now;
            CustomerId = customerId;

            ProductOrders = new List<ProductOrder>
            {
                new ProductOrder
                {
                    ProductId = productId,
                    Quantity = quantity
                }
            };
        }

        public void AddProduct(int productId, int quantity)
        {
            if (quantity <= 0)
                throw new InvalidOperationException("La cantidad tiene que ser mayor a cero");

            ProductOrders.Add(new ProductOrder
            {
                ProductId = productId,
                Quantity = quantity
            });
        }

        // Obtener total a pagar, regresa decimal
        public decimal GetTotal() => ProductOrders.Sum(c => c.Quantity * c.Product.Price);


        // Quitar producto de la orden, regresa void
        public void RemoveProduct(int productId)
        {
            ProductOrder product = ProductOrders.SingleOrDefault(c => c.ProductId == productId);

            if(product != null)
            {
                ProductOrders.Remove(product);
            }
            else
            {
                throw new ArgumentNullException("El producto no existe en la orden");
            }
        }

        // Completar orden, regresa void
        public void Complete()
        {
            if (OrderFulfilled.HasValue)
                throw new InvalidOperationException("Esta orden ya habia sido completada");

            OrderFulfilled = DateTime.Now;
        }

        // Actualizar la cantidad de un producto en la orden
        public void ChangeProductQuantity(int productId, int quantity)
        {
            if (quantity < 0)
                throw new InvalidOperationException("La cantidad tiene que ser mayor o igual a cero");

            ProductOrder productOrder = ProductOrders.SingleOrDefault(c => c.ProductId == productId);

            if (productOrder != null)
            {
                if (quantity == 0)
                    ProductOrders.Remove(productOrder);
                else
                    productOrder.Quantity = quantity;
            }
            else
            {
                throw new ArgumentNullException("El producto no existe en la orden");
            }
        }
    }
}