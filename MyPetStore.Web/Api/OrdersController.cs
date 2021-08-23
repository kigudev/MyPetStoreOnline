using Microsoft.AspNetCore.Mvc;
using MyPetStore.Web.Models;
using MyPetStoreOnline.Entities;
using MyPetStoreOnline.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPetStore.Web.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController: ControllerBase
    {
        private readonly IShopService _shopService;

        public OrdersController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet]
        public Task<IEnumerable<Order>> Get() => _shopService.GetOrdersAsync();

        [HttpPost]
        public IActionResult AddProduct(ProductOrderRequest request)
        {
            var product = _shopService.GetProductAsync(request.ProductId);

            if (product == null)
                return NotFound();

            try
            {
                _shopService.AddProductToOrderAsync(request.CustomerId, request.ProductId, request.Quantity);
                return NoContent();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Complete")]
        // complete?customerId=3
        public async Task<IActionResult> Complete([FromBody]int customerId)
        {
            try
            {
                await _shopService.CompleteOrderAsync(customerId);
                return NoContent();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _shopService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
