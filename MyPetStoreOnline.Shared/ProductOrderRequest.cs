using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPetStore.Shared
{
    public class ProductOrderRequest
    {
        // TODO: validar con filtros de rango
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
