using System;
using System.ComponentModel.DataAnnotations;

namespace MyPetStoreOnline.Entities
{
    public class Address
    {
        [MaxLength(200)]
        [Required]
        public string StreetAddress { get; private set; }

        [MaxLength(100)]
        [Required]
        public string StateOrProvinceAbbr { get; private set; }

        [MaxLength(3)]
        [Required]
        public string Country { get; private set; }

        [MaxLength(5)]
        [Required]
        public string PostalCode { get; private set; }

        [MaxLength(100)]
        [Required]
        public string City { get; private set; }

        public Customer Customer { get; private set; }

        public Address(string streetAddress, string stateOrProvinceAbbr, string country, string postalCode, string city)
        {
            StreetAddress = streetAddress ?? throw new ArgumentNullException(nameof(streetAddress));
            StateOrProvinceAbbr = stateOrProvinceAbbr ?? throw new ArgumentNullException(nameof(stateOrProvinceAbbr));
            Country = country ?? throw new ArgumentNullException(nameof(country));
            PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
            City = city ?? throw new ArgumentNullException(nameof(city));
        }

        public Address()
        {
            // Usado por EF
        }
    }
}