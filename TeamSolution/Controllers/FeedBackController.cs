using Microsoft.AspNetCore.Mvc;
using TeamSolution.Service.Interface;

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
    }
}
