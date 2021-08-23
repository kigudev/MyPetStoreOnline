using Microsoft.AspNetCore.Mvc;
using MyPetStore.Web.Models;
using MyPetStoreOnline.Entities;
using MyPetStoreOnline.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPetStore.Web.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IShopService _shopService;

        public CustomersController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress(AddressRequest request)
        {
            try
            {
                var address = new Address(request.StreetAddress, request.StateOrProvinceAbbr, request.Country, request.PostalCode, request.City);
                await _shopService.AddUpdateAddressAsync(request.CustomerId, address);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
