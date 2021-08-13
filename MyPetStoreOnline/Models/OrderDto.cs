using System.Collections.Generic;

namespace MyPetStoreOnline.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
}