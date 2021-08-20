using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPetStore.Web.Services.Abstractions;
using MyPetStoreOnline.Entities;
using MyPetStoreOnline.Models;
using MyPetStoreOnline.Services.Abstractions;
using System;
using System.Threading.Tasks;

namespace MyPetStore.Web.Pages.Products
{
    // Cada vez que se haga una petición se va a volver a instanciar la clase
    [Authorize(Roles = "Administrator,Manager")]
    public class EditModel : PageModel
    {
        private readonly IShopService _shopService;
        private readonly IFileService _fileService;

        public EditModel(IShopService shopService, IFileService fileService)
        {
            _shopService = shopService;
            _fileService = fileService;
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
                    var imageUrl = string.Empty;
                    if (ProductDto.Image != null)
                    {
                        imageUrl = await _fileService.SaveImageAsync(ProductDto.Image);
                    }
                    await _shopService.UpdateProductAsync(ProductDto.Id, ProductDto.Name, ProductDto.Description, ProductDto.Price, imageUrl);

                    Message = "The product was updated successfully";
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }
    }
}