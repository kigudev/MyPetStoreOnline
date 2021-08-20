using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPetStoreOnline.Entities;
using MyPetStoreOnline.Models;
using MyPetStoreOnline.Services.Abstractions;
using System;
using System.Threading.Tasks;

namespace MyPetStore.Web.Pages.Customers
{
    public class AddModel : PageModel
    {
        private readonly IShopService _shopService;

        public AddModel(IShopService shopService)
        {
            _shopService = shopService;
        }

        [BindProperty]
        public CustomerDto Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var customer = new Customer(Input.FirstName, Input.LastName, Input.Email);

                    await _shopService.AddCustomerAsync(customer);
                    return RedirectToPage("/Customers");
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