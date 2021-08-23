using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyPetStore.Web.Models
{
    public class AddressRequest
    {
        [MaxLength(200)]
        [Required]
        public string StreetAddress { get; set; }

        [MaxLength(100)]
        [Required]
        public string StateOrProvinceAbbr { get; set; }

        [MaxLength(3)]
        [Required]
        public string Country { get; set; }

        [MaxLength(5)]
        [Required]
        public string PostalCode { get; set; }

        [MaxLength(100)]
        [Required]
        public string City { get; set; }

        public int CustomerId { get; set; }
    }
}
