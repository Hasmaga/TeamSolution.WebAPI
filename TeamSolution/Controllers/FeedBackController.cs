using Microsoft.AspNetCore.Mvc;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Controllers
{
    [Route("feedbackapi")]
    [ApiController]
    public class FeedBackController : Controller
    {
        private readonly IFeedBackRepository _feedBackRepository;
        public FeedBackController(IFeedBackRepository feedBackRepository)
        {
            _feedBackRepository = feedBackRepository;
        }
    }
}
