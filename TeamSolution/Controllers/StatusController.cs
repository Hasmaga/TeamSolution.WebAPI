using Microsoft.AspNetCore.Mvc;
using TeamSolution.Repository.Interface;
using TeamSolution.Service.Interface;

namespace TeamSolution.Controllers
{
    [Route("statusapi")]
    [ApiController]
    public class StatusController : Controller
    {
        private readonly IStatusService _statusService;
        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }
    }
}
