using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPetStoreOnline.Models;
using MyPetStoreOnline.Services.Abstractions;

namespace MyPetStore.Web.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IShopService _shopService;

        public EditModel(IShopService shopService)
        {
            _shopService = shopService;
        }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty]
        public ProductDto Product { get; set; }
        public async Task OnGetAsync()
        {
            var product = await _shopService.GetProductAsync(Id);

            Product = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        public async Task OnPostAsync()
        {
            await _shopService.UpdateProductAsync(Product.Id, Product.Name, Product.Description, Product.Price);
        }
    }
}
