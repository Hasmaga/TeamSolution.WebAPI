using Microsoft.AspNetCore.Mvc;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Controllers
{
    [Route("statusapi")]
    [ApiController]
    public class StatusController : Controller
    {
        private readonly IStatusRepository _statusRepository;
        public StatusController(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }
    }
}
