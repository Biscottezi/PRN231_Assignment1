using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.RequestModel;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository repo;
        private readonly IMapper mapper;
        
        public OrderController(IOrderRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]OrderSortModel sortDate)
        {
            try
            {
                var orders = repo.GetOrderList(sortDate);
                return Ok(orders);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var order = repo.GetOrderById(id);
                return Ok(order);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/member")]
        public async Task<IActionResult> GetByMemberId(int id)
        {
            try
            {
                var orders = repo.GetOrdersByMemberId(id);
                return Ok(orders);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateModel createModel)
        {
            try
            {
                if (createModel == null)
                {
                    return BadRequest();
                }
                repo.CreateOrder(createModel);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderCreateModel requestModel)
        {
            try
            {
                if (requestModel == null)
                {
                    return BadRequest();
                }
                repo.UpdateOrder(id, requestModel);
                return NoContent();
            }
            catch (Exception e)
            {
                return e.Message.Equals("This order doesn't exist.") 
                    ? NotFound() 
                    : StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                repo.DeleteOrder(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return e.Message.Equals("This order doesn't exist.") 
                    ? NotFound() 
                    : StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}