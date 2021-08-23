using MyPetStoreOnline.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPetStore.Web.Models
{
    public class OrdersViewModel
    {
        public List<Order> Orders { get; set; }
        public string Search { get; set; }
    }
}
