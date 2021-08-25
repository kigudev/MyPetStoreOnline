﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPetStoreOnline.Entities
{
    public class ProductType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
