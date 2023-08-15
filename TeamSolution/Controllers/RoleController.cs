using Microsoft.AspNetCore.Mvc;
using TeamSolution.Model.Dto;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Controllers
{
    [Route("roleapi")]
    [ApiController]
    public class RoleController : Controller
    {        
        private readonly IRoleReposotory _roleRepository;

        public RoleController(IRoleReposotory roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // POST: roleapi/createrole
        [HttpPost("createrole")]
        public async Task<IActionResult> CreateRoleAsync(NewRoleReqDto role)
        {
            try
            {
                return Ok(await _roleRepository.CreateRoleAsync(role));
            }
            catch (Exception)
            {
                // return StatusCode(500, "Internal server error");
                return StatusCode(500);
            }
        }
    }
}
