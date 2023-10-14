using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamSolution.Enum;
using TeamSolution.Model.Dto;
using TeamSolution.Service.Interface;

namespace TeamSolution.Controllers
{
    [Route("accountapi")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // POST: userapi/creatememberacc
        [HttpPost("creatememberacc")]
        public async Task<IActionResult> CreateMemberAccAsync(CreateNewCustomerReqDto acc)
        {
            try
            {
                var token = await _accountService.CreateMemberAccAsync(acc);
                if (token != null)
                {
                    return StatusCode(200, "Bearer " + token);
                }
                else
                {
                    return StatusCode(500, ErrorCode.CREATE_USER_FAIL);
                }
            }
            catch (Exception e)
            {
                if (e.Message == ErrorCode.USER_IS_EXIST)
                {
                    return StatusCode(409, ErrorCode.USER_IS_EXIST);
                } else
                {
                    return StatusCode(500, ErrorCode.SERVER_ERROR);
                }
            }
        }

        // POST: userapi/createadminacc
        [HttpPost("createadminacc"), Authorize]        
        public async Task<IActionResult> CreateAdminAccAsync(CreateNewCustomerReqDto acc)
        {
            try
            {
                if(await _accountService.CreateAdminAccAsync(acc))
                {
                    return StatusCode(200, SucessfulCode.CREATE_USER_SUCCESSFULLY);
                } else
                {
                    return StatusCode(500, ErrorCode.SERVER_ERROR);
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
                    return StatusCode(500, ErrorCode.SERVER_ERROR);
                }                         
            }
        }

        // POST: userapi/login
        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAsync(LoginReqDto loginReqDto)
        {
            try
            {
                var token = await _accountService.LoginAsync(loginReqDto);
                if (token != null)
                {
                    return StatusCode(200, "Bearer " + token);
                } else
                {
                    return StatusCode(500, ErrorCode.LOGIN_FAIL);
                }                                
            }
            catch (Exception e)
            {
                if (e.Message == ErrorCode.USER_NOT_FOUND)
                {
                    return StatusCode(404, e.Message);
                } else
                {
                    return StatusCode(500, e.Message);
                }
            }
        }

        // GET: userapi/validateotpcode
        [Authorize]
        [HttpGet("validateotpcode")]
        public async Task<IActionResult> ValidateAcccountByOtpCodeAsync(string otpCode)
        {
            try
            {
                if (await _accountService.ValidateAcccountByOtpCodeAsync(otpCode))
                {
                    return StatusCode(200, SucessfulCode.VALIDATE_ACCOUNT_SUCCESSFULLY);
                } else
                {
                    return StatusCode(500, ErrorCode.SERVER_ERROR);
                }                
            }
            catch (Exception e)
            {
                if (e.Message == ErrorCode.OTP_CODE_NOT_FOUND)
                {
                    return StatusCode(404, e.Message);
                } else if (e.Message == ErrorCode.OTP_CODE_EXPIRED)
                {
                    return StatusCode(401, e.Message);
                } else if (e.Message == ErrorCode.OTP_CODE_NOT_MATCH)
                {
                    return StatusCode(400, e.Message);
                } else
                {
                    return StatusCode(500, ErrorCode.SERVER_ERROR);
                }                
            }
        }

        [Authorize]
        [HttpGet("generateotp")]
        public async Task<IActionResult> GenerateOtpAccountAndSendToEmail()
        {
            try
            {
                if (await _accountService.GenerateOtpAccountAndSendToEmail())
                {
                    return StatusCode(200, "Generate OTP code successfully");
                } else
                {
                    return StatusCode(500, ErrorCode.SERVER_ERROR);
                }                
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
