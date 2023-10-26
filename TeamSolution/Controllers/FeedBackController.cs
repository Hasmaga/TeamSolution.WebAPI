using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamSolution.Enum;
using TeamSolution.Service.Interface;
using TeamSolution.ViewModel.Feedback;

namespace TeamSolution.Controllers
{
    [Route("feedbackapi")]
    [ApiController]
    public class FeedBackController : Controller
    {
        private readonly IFeedBackService _feedBackService;
        public FeedBackController(IFeedBackService feedBackService)
        {
            _feedBackService = feedBackService;
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> CreateFeedback(CreateNewFeedbackReqDto feedback)
        {
            try
            {
                if (await _feedBackService.CreateFeedbackServiceAsync(feedback))
                {
                    return StatusCode(200, SucessfulCode.CREATE_FEEDBACK_SUCCESSFULLY);
                }
                else
                {
                    return StatusCode(500, ErrorCode.CREATE_FEEDBACK_FAIL);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, ErrorCode.SERVER_ERROR);
            }
        }
    }
}
