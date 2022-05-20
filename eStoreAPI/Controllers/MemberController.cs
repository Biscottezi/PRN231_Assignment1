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
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;

namespace eStoreAPI.Controllers
{
    [Route("api/member")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository repo;
        private readonly IMapper mapper;
        
        public MemberController(IMemberRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var members = repo.GetMemberList();
                return Ok(members);
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
                var member = repo.GetMemberById(id);
                return Ok(member);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MemberCreateModel createModel)
        {
            try
            {
                if (createModel == null)
                {
                    return BadRequest();
                }
                repo.CreateMember(createModel);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MemberCreateModel requestModel)
        {
            try
            {
                if (requestModel == null)
                {
                    return BadRequest();
                }
                repo.UpdateMember(id, requestModel);
                return NoContent();
            }
            catch (Exception e)
            {
                return e.Message.Equals("This member doesn't exist.") 
                    ? NotFound() 
                    : StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                repo.DeleteMember(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return e.Message.Equals("This member doesn't exist.") 
                    ? NotFound() 
                    : StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                if (loginModel == null)
                {
                    return BadRequest();
                }
                var email = loginModel.Email;
                var password = loginModel.Password;
                var isAdmin = repo.LoginAdmin(email, password);
                if (isAdmin)
                {
                    return Ok(new LoginViewModel()
                    {
                        Role = 1,
                        Member = null,
                    });
                }
                else
                {
                    var member = repo.LoginMember(email, password);
                    if (member != null)
                    {
                        return Ok(new LoginViewModel()
                        {
                            Role = 2,
                            Member = member,
                        });
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
    }
}