using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPetStoreOnline.Models
{
    public enum OrderStatus
    {
        Pending,
        Preparing,
        Sent,
        Completed,
        Cancelled
    }
}
