using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TeamSolution.Enum;
using TeamSolution.Model;
using TeamSolution.Service.Interface;
using TeamSolution.ViewModel.StoreService;

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

        [HttpGet("getservicebyhttpcontext")]
        [Authorize]
        public async Task<IActionResult> GetStoreServiceByHttpContext()
        {
            try
            {
                var result = await _storeServiceService.GetStoreServiceByHttpContextServiceAsync();
                return StatusCode(200, result);
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorCode.SERVER_ERROR);
            }
        }

        [HttpPost("createstoreservice")]
        [Authorize]
        public async Task<IActionResult> CreateStoreService(CreateStoreServiceReqDto createStoreServiceReqDto)
        {
            try
            {
                if (await _storeServiceService.CreateStoreServiceServiceAsync(createStoreServiceReqDto))
                {
                    return StatusCode(200);
                }
                else
                {
                    return StatusCode(500, ErrorCode.SERVER_ERROR);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorCode.SERVER_ERROR);
            }
        }
    }
}
