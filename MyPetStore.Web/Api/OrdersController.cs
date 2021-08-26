using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPetStore.Shared;
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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController: ControllerBase
    {
        private readonly IShopService _shopService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(IShopService shopService, UserManager<ApplicationUser> userManager)
        {
            _shopService = shopService;
            _userManager = userManager;
        }

        /// <summary>
        /// Obtiene todas las ordenes de la tienda
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<IEnumerable<Order>> Get() => _shopService.GetOrdersAsync();


        [HttpGet("mine")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetMine() {
            var user = await _userManager.GetUserAsync(User);

            if (!user.CustomerId.HasValue)
                return BadRequest("The customer doesn't exist");


            var orders = await _shopService.GetOrdersAsync(user.CustomerId.Value);

            return Ok(orders);
        }

        /// <summary>
        /// Agrega un producto a una orden.
        /// Si la orden no existe la crea.
        /// Si el producto ya existe en la orden actualiza la cantidad.
        /// </summary>
        /// <remarks>
        ///  POST /Orders
        ///  {
        ///     ProductId: [el id válido de un producto]
        ///     Quantity: [cantidad solo en positivo]
        ///     CustomerId: [el id del cliente]
        ///  }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductOrderRequest request)
        {
            //var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var user = await _userManager.GetUserAsync(User);

            if (!user.CustomerId.HasValue)
                return BadRequest("The customer doesn't exist");

            var product = await _shopService.GetProductAsync(request.ProductId);

            if (product == null)
                return NotFound();

            try
            {
                await _shopService.AddProductToOrderAsync(user.CustomerId.Value, request.ProductId, request.Quantity);
                return NoContent();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Completa la unica orden abierta de un cliente
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
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
