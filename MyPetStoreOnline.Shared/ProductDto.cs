using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPetStore.Shared
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        [MaxLength(100)]
        public string ImageUrl { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
