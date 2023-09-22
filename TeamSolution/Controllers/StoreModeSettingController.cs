using Microsoft.AspNetCore.Mvc;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Controllers
{
    [Route("storemodesettingapi")]
    [ApiController]
    public class StoreModeSettingController : Controller
    {
        private readonly IStoreModeSettingRepository _storeModeSettingRepository;
        public StoreModeSettingController(IStoreModeSettingRepository storeModeSettingRepository)
        {
            _storeModeSettingRepository = storeModeSettingRepository;
        }
    }
}
