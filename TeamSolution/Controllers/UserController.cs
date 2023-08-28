using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamSolution.Enum;
using TeamSolution.Model.Dto;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Controllers
{
    [Route("userapi")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // POST: userapi/creatememberacc
        [HttpPost("creatememberacc")]
        public async Task<IActionResult> CreateMemberAccAsync(NewAccReqDto acc)
        {
            try
            {
                if (await _userRepository.CreateMemberAccAsync(acc))
                {
                    return StatusCode(200, "CREATE_USER_SUCCESSFULLY");
                }
                else
                {
                    return StatusCode(500, "CREATE_USER_FAIL");
                }                
            }
            catch (Exception e)
            {
                if (e.Message == ErrorCode.USER_IS_EXIST)
                {
                    return StatusCode(409, e.Message);
                } else
                {
                    return StatusCode(500, e);
                }
            }
        }

        // POST: userapi/createadminacc
        [HttpPost("createadminacc"), Authorize]        
        public async Task<IActionResult> CreateAdminAccAsync(NewAccReqDto acc)
        {
            try
            {
                if(await _userRepository.CreateAdminAccAsync(acc))
                {
                    return StatusCode(200, "CREATE_USER_SUCCESSFULLY");
                } else
                {
                    return StatusCode(500, "CREATE_USER_FAIL");
                }                
            }
            catch (Exception e)
            {
                if(e.Message == ErrorCode.NOT_AUTHORIZED)
                {
                    return StatusCode(401, e.Message);
                } else if (e.Message == ErrorCode.USER_IS_EXIST)
                {
                    return StatusCode(409, e.Message);
                } else
                {
                    return StatusCode(500, e);
                }                         
            }
        }

        // POST: userapi/login
        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAsync(LoginReqDto loginReqDto)
        {
            try
            {
                var token = await _userRepository.LoginAsync(loginReqDto);
                if (token == null)
                {
                    return StatusCode(200, "Bearer " + token);
                } else
                {
                    return StatusCode(500, "LOGIN_FAIL");
                }                                
            }
            catch (Exception e)
            {
                if (e.Message == ErrorCode.USER_NOT_FOUND)
                {
                    return StatusCode(404, e.Message);
                } else
                {
                    return StatusCode(500, e);
                }
            }
        }
    }
}
