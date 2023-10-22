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
                if(result?.Count==0)
                {
                    return StatusCode(200, ResponseCodeConstants.EMPTY);
                }
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
                if (result != null)
                {

                    return StatusCode(200, result);
                }
                else
                {
                    return StatusCode(404, ResponseCodeConstantsStore.STORE + "_" + ResponseCodeConstants.NOT_FOUND);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, ErrorCode.SERVER_ERROR);
            }
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> CreateStore(StoreModel store)
        {
            try
            {
                if (await _storeService.CreateStoreAsync(store) != Guid.Empty)
                {
                    return StatusCode(200, ResponseCodeConstantsStore.CREATE_STORE_SUCCESSFULLY);
                }
                else
                {
                    //Never happen - If something wrong, it will throw exception.
                    return StatusCode(500, ResponseCodeConstantsStore.CREATE_STORE_FAILED);
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
                if (await _storeService.UpdateStoreAsync(updateStoreRequest) != Guid.Empty)
                {
                    return StatusCode(200, ResponseCodeConstantsStore.UPDATE_STORE_SUCCESSFULLY);
                }
                else
                {
                    return StatusCode(404, ResponseCodeConstantsStore.STORE + "_" + ResponseCodeConstants.NOT_FOUND);
                }
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
                if(await _storeService.DeleteStoreAsync(id) != Guid.Empty)
                {
                    return StatusCode(200, ResponseCodeConstantsStore.DELETE_STORE_SUCCESSFULLY);
                }
                else
                {
                    return StatusCode(404, ResponseCodeConstantsStore.STORE + "_" + ResponseCodeConstants.NOT_FOUND);
                }

            }
            catch (Exception ex)
            {
                if (ex.Message == ResponseCodeConstants.IS_DELETED)
                {
                    // Undefine status code
                    return StatusCode(500, ResponseCodeConstants.IS_DELETED);
                }
                return StatusCode(500, ErrorCode.SERVER_ERROR);
            }
        }

    }
}
