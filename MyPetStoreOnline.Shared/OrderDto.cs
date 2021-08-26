using MyPetStore.Shared;
using System.Collections.Generic;

namespace MyPetStore.Shared
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public decimal Total { get; set; }
        public string Estatus { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
}