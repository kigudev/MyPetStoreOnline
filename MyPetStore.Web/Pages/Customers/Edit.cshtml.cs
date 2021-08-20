using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPetStoreOnline.Models;
using MyPetStoreOnline.Services.Abstractions;

namespace MyPetStore.Web.Pages.Customers
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
        public CustomerDto Input { get; set; }

        public string Message { get; set; }

        public async Task OnGetAsync()
        {
            var product = await _shopService.GetCustomerAsync(Id);

            Input = new CustomerDto
            {
                Id = product.Id,
                FirstName = product.FirstName,
                LastName = product.LastName,
                Email = product.Email
            };
        }

        public async Task OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                  
                    await _shopService.UpdateCustomerAsync(Input.Id, Input.FirstName, Input.LastName, Input.Email);

                    Message = "The customer was updated successfully";
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }
    }
}
