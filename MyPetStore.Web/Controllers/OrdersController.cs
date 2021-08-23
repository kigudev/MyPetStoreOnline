using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPetStore.Web.Models;
using MyPetStoreOnline.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPetStore.Web.Controllers
{
    [Authorize]
    public class OrdersController: Controller
    {
        private readonly IShopService _shopService;

        public OrdersController(IShopService shopService)
        {
            _shopService = shopService;
        }

        public async Task<IActionResult> Index(string search)
        {
            var orders = await _shopService.GetOrdersAsync();

            if (!string.IsNullOrEmpty(search))
                orders = orders.Where(c => c.Id.ToString() == search || c.Customer.FirstName.Contains(search) || c.Customer.LastName.Contains(search) || c.Customer.Email.Contains(search));

            var vm = new OrdersViewModel
            {
                Orders = orders.ToList()
            };
            return View(vm);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult ChangeStatus()
        {
            return Ok();
        }
    }
}
