using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic;
using BusinessLogic.RequestModel;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository repo;
        private readonly IMapper mapper;

        public ProductController(IProductRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]ProductSearchModel searchModel)
        {
            try
            {
                var products = repo.GetProductList(searchModel);
                return Ok(products);
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
                var product = repo.GetProductById(id);
                return Ok(product);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateModel createModel)
        {
            if (createModel.UnitPrice < 0
                || createModel.UnitsInStock < 0)
            {
                return BadRequest();
            }
            try
            {
                repo.CreateProduct(createModel);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductCreateModel requestModel)
        {
            if (requestModel.UnitPrice < 0
                || requestModel.UnitsInStock < 0)
            {
                return BadRequest();
            }
            try
            {
                repo.UpdateProduct(id, requestModel);
                return NoContent();
            }
            catch (Exception e)
            {
                return e.Message.Equals("This product doesn't exist.") 
                    ? NotFound() 
                    : StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                repo.DeleteProduct(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return e.Message.Equals("This product doesn't exist.") 
                    ? NotFound() 
                    : StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}