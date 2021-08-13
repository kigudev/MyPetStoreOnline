using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MyPetStoreOnline.Entities;
using MyPetStoreOnline.Models;
using MyPetStoreOnline.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPetStore.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IShopService _shopService;

        public IndexModel(ILogger<IndexModel> logger, IShopService shopService)
        {
            _logger = logger;
            _shopService = shopService;
        }

        [BindProperty]
        public int ProductId { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public string Errors { get; set; }

        public async Task OnGetAsync() => Products = await _shopService.GetProductsAsync();

        public async Task OnPostAsync()
        {
            try
            {
                await _shopService.DeleteProductAsync(ProductId);
            }catch(Exception ex)
            {
                Errors = ex.Message;
            }
            Products = await _shopService.GetProductsAsync();
        }
    }
}
