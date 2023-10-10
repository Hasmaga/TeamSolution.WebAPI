using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamSolution.Enum;
using TeamSolution.Model.Dto;
using TeamSolution.Service.Interface;

namespace TeamSolution.Controllers
{
    [Route("roleapi")]
    [ApiController]
    public class RoleController : Controller
    {        
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // POST: roleapi/createrole
        [HttpPost("createrole"), Authorize]
        public async Task<IActionResult> CreateRoleAsync(NewRoleReqDto role)
        {
            try
            {
                await _roleService.CreateRoleAsync(role);
                return StatusCode(200, SucessfulCode.CREATE_ROLE_SUCCESSFULLY);
            }
            catch (Exception e)
            {
                if(e.Message == ErrorCode.ROLE_IS_EXIST)
                {
                    return StatusCode(409, e.Message);
                }
                else
                {
                    return StatusCode(500, ErrorCode.SERVER_ERROR);
                }
            }
        }

        [HttpGet("getallrole")]
        public async Task<IActionResult> GetAllRoleAsync()
        {
            try
            {
                return StatusCode(200, await _roleService.GetAllRoleAsync());
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorCode.SERVER_ERROR);
            }
        }
    }
}
