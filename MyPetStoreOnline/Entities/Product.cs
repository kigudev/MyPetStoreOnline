using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPetStoreOnline.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; private set; }
        [MaxLength(1000)]
        public string Description { get; private set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; private set; }
        public DateTime CreatedAt { get; private set; }
        [MaxLength(100)]
        public string ImageUrl { get; private set; }

        public Product()
        {
            // Usado por EF
        }

        // Solo para probar los metodos con datos en memoria
        //private static int consecutiveNumber = 1;

        public Product(string name, string description, decimal price)
        {
            //Id = consecutiveNumber++;

            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));

            if (price < 0)
                throw new InvalidOperationException("El precio no puede ser menor a cero");

            Price = price;
        }

        public void Update(string name, string description, decimal price)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));

            if (price < 0)
                throw new InvalidOperationException("El precio no puede ser menor a cero");

            Price = price;
        }

        public void AddOrUpdateImage(string url)
        {
            if(string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            ImageUrl = url;
        }
    }
}