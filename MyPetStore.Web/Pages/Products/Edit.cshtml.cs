using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPetStoreOnline.Entities;
using MyPetStoreOnline.Models;
using MyPetStoreOnline.Services.Abstractions;

namespace MyPetStore.Web.Pages.Products
{
    // Cada vez que se haga una petición se va a volver a instanciar la clase
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
        public ProductDto ProductDto { get; set; }
        public string Message { get; set; }
        public async Task OnGetAsync()
        {
            Product product = await _shopService.GetProductAsync(Id);

            ProductDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        public async Task OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _shopService.UpdateProductAsync(ProductDto.Id, ProductDto.Name, ProductDto.Description, ProductDto.Price);
                    Message = "The product was updated successfully";
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }
    }
}
