using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TeamSolution.Enum;
using TeamSolution.Model;
using TeamSolution.Service.Interface;

namespace TeamSolution.Controllers
{
    [Route("storeserviceapi")]
    [ApiController]
    public class StoreServiceController : Controller
    {        
        private readonly IStoreServiceService _storeServiceService;
        public StoreServiceController(IStoreServiceService storeServiceService)
        {
            _storeServiceService = storeServiceService;
        }

        [HttpGet("getservicebystore/{storeId}")]
        public async Task<IActionResult> GetStoreServiceByStoreId(Guid storeId)
        {
            try
            {
                var result = await _storeServiceService.GetStoreServiceByStoreIdServiceAsync(storeId);
                return StatusCode(200, result);
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorCode.SERVER_ERROR);
            }
        }
    }
}
