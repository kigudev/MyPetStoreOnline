using System.ComponentModel.DataAnnotations;

namespace MyPetStoreOnline.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        [Range(0, 9999)]
        public decimal Price { get; set; }
    }
}