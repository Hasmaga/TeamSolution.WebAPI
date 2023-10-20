using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamSolution.Enum;
using TeamSolution.Model;
using TeamSolution.Service;
using TeamSolution.Service.Interface;
using TeamSolution.ViewModel.Order;
using TeamSolution.ViewModel.Store;

namespace TeamSolution.Controllers
{
    [Route("storeapi")]
    [ApiController]
    public class StoreController : Controller
    {
        private readonly IStoreService _storeService;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateStore(StoreModel store)
        {
            try
            {
                if (await _storeService.CreateStoreAsync(store)!=Guid.Empty)
                {
                    return StatusCode(200, SucessfulCode.CREATE_ORDER_SUCCESSFULLY);
                }
                else
                {
                    return StatusCode(500, ErrorCode.CREATE_ORDER_FAIL);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, ErrorCode.SERVER_ERROR);
            }
        }
    }
}
