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
        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAllStore()
        {
            try
            {
                var result = await _storeService.GetAllStore();
                return StatusCode(200, result);

            }
            catch (Exception e)
            {
                return StatusCode(500, ErrorCode.SERVER_ERROR);
            }
        }

        [HttpGet("GetById")]
        [Authorize]
        public async Task<IActionResult> GetSigleStoreById(Guid id)
        {
            try
            {
                var result = await _storeService.GetStoreById(id);
                return StatusCode(200, result);

            }
            catch (Exception e)
            {
                return StatusCode(500, ErrorCode.SERVER_ERROR);
            }
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateStore(StoreModel store)
        {
            try
            {
                if (await _storeService.CreateStoreAsync(store) != Guid.Empty)
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
        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> UpdateStore(UpdateStoreRequestModel updateStoreRequest)
        {
            try
            {
                await _storeService.UpdateStoreAsync(updateStoreRequest);
                return StatusCode(200, SucessfulCode.UPDATE_STORE_SUCCESSFULLY);
               
            }
            catch (Exception e)
            {
                return StatusCode(500, ErrorCode.SERVER_ERROR);
            }
        }
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteStore(Guid id)
        {
            try
            {
                await _storeService.DeleteStoreAsync(id);
                return StatusCode(200, SucessfulCode.DELETE_STORE_SUCCESSFULLY);

            }
            catch (Exception e)
            {
                return StatusCode(500, ErrorCode.SERVER_ERROR);
            }
        }

    }
}
