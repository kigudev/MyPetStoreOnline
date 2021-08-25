using Microsoft.AspNetCore.Mvc;
using MyPetStore.Shared;
using MyPetStoreOnline.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPetStore.Web.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController : ControllerBase
    {
        private readonly IShopService _shopService;

        public BrandsController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet]
        public async Task<IEnumerable<BrandDto>> Get()
        {
            return (await _shopService.GetProductBrandsAsync()).Select(c => new BrandDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }
    }
}