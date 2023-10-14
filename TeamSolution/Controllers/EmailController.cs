using Microsoft.AspNetCore.Mvc;
using TeamSolution.Service.Interface;

namespace TeamSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        // Send email
        [HttpPost] 
        public async Task<IActionResult> SendEmail(string subject, string Body, string emailTo)
        {
            try
            {
                if(await _emailService.SendEmail(subject, Body, emailTo))
                {
                    return StatusCode(200, "Send email successfully");
                }
                else
                {
                    return StatusCode(500, "Some thing wrong???");
                }                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
