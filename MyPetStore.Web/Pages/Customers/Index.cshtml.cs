using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPetStoreOnline.Entities;
using MyPetStoreOnline.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyPetStore.Web.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly IShopService _shopService;

        public IndexModel(IShopService shopService)
        {
            _shopService = shopService;
        }

        [BindProperty]
        public int CustomerId { get; set; }

        public IEnumerable<Customer> Customers { get; set; }

        public string Errors { get; set; }

        public async Task OnGetAsync() => Customers = await _shopService.GetCustomersAsync();

        public async Task OnPostAsync()
        {
            try
            {
                await _shopService.DeleteCustomerAsync(CustomerId);
            }
            catch (Exception ex)
            {
                Errors = ex.Message;
            }
            Customers = await _shopService.GetCustomersAsync();
        }
    }
}