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
    public class AddModel : PageModel
    {
        private readonly IShopService _shopService;
        private readonly IFileService _fileService;

        public AddModel(IShopService shopService, IFileService fileService)
        {
            _shopService = shopService;
            _fileService = fileService;
        }

        [BindProperty]
        public ProductDto ProductDto { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product = new Product(ProductDto.Name, ProductDto.Description, ProductDto.Price);

                    if (ProductDto.Image != null)
                    {
                        var imageUrl = await _fileService.SaveImageAsync(ProductDto.Image);
                        product.AddOrUpdateImage(imageUrl);
                    }

                    await _shopService.AddProductAsync(product);
                    return RedirectToPage("/Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return Page();
                }
            }
            else
            {
                return Page();
            }
        }
    }
}