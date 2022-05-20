using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.RequestModel;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/order-detail")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailRepository repo;

        public OrderDetailController(IOrderDetailRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet("{id}/order")]
        public async Task<IActionResult> GetByOrderId(int id)
        {
            try
            {
                var orderDetails = repo.GetOrderDetailByOrderId(id);
                return Ok(orderDetails);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDetailCreateModel createModel)
        {
            try
            {
                repo.CreateOrderDetail(createModel);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int orderId, [FromQuery] int productId)
        {
            try
            {
                repo.DeleteOrderDetail(orderId, productId);
                return NoContent();
            }
            catch (Exception e)
            {
                return e.Message.Equals("This detail doesn't exist.") 
                    ? NotFound() 
                    : StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}