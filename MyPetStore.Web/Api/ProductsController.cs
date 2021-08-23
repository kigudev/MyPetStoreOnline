using Microsoft.AspNetCore.Mvc;
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
    public class ProductsController : ControllerBase
    {
        private readonly IShopService _shopService;

        public ProductsController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get(string search, string order) {
            var products = await _shopService.GetProductsAsync();

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(c => c.Name.Contains(search));
            }

            if (!string.IsNullOrEmpty(order))
            {

                products = order == "desc" ? products.OrderByDescending(c => c.Name) : products.OrderBy(c => c.Name);
            }

            return products;
        }

    }
}
