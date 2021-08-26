using Microsoft.AspNetCore.Mvc;
using MyPetStore.Shared;
using MyPetStoreOnline.Entities;
using MyPetStoreOnline.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
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

        //[HttpGet]
        //public async Task<IEnumerable<ProductDto>> Get()
        //{
        //    var products = await _shopService.GetProductsAsync();

        //    var model = products.Select(c => new ProductDto
        //    {
        //        Id = c.Id,
        //        Name = c.Name,
        //        Price = c.Price,
        //        ImageUrl = c.ImageUrl
        //    });

        //    return model;
        //}

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> Get(string search, int? brand, string order)
        {
            var products = await _shopService.GetProductsAsync();

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(c => c.Name.ToLower().Contains(search.ToLower()));
            }


            if (brand.HasValue)
            {
                products = products.Where(c => c.ProductBrandId == brand);
            }

            if (!string.IsNullOrEmpty(order))
            {
                products = order == "desc" ? products.OrderByDescending(c => c.Name) : products.OrderBy(c => c.Name);
            }

            var model = products.Select(c => new ProductDto
            {
                Id = c.Id,
                Name = c.Name,
                Price = c.Price,
                ImageUrl = c.ImageUrl
            });

            return model;
        }
    }
}