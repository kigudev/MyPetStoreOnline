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
    public class AddModel : PageModel
    {
        private readonly IShopService _shopService;

        public AddModel(IShopService shopService)
        {
            _shopService = shopService;
        }

        [BindProperty]
        public ProductDto Product { get; set; }

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product = new Product(Product.Name, Product.Description, Product.Price);
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
