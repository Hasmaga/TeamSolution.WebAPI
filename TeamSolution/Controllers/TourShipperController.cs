using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamSolution.Enum;
using TeamSolution.Service.Interface;
using TeamSolution.ViewModel.TourShipper;

namespace TeamSolution.Controllers
{
    [Route("storeserviceapi")]
    [ApiController]
    public class TourShipperController : Controller
    {
        private readonly ITourShipperService _tourShipperService;
        public TourShipperController(ITourShipperService tourShipperService)
        {
            _tourShipperService = tourShipperService;
        }

        // post: tourShipperapi/create
        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> CreatetourShipper(TourShipperModel tourShipper)
        {
            try
            {
                if (await _tourShipperService.CreateTourServiceAsync(tourShipper))
                {
                    return StatusCode(200, ResponseCodeConstantsTourShipper.CREATE_TOURSHIPPER_SUCCESSFULLY);
                }
                else
                {
                    return StatusCode(500, ResponseCodeConstantsTourShipper.CREATE_TOURSHIPPER_FAILED);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorCode.SERVER_ERROR);
            }
        }
        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAlltourShipper()
        {
            try
            {
                var result = await _tourShipperService.GetAllToursServiceAsync();
                if (result?.Count == 0)
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
        [HttpGet("GetTourShipperById")]
        [Authorize]
        public async Task<IActionResult> GetSingleTourShipperById(Guid id)
        {
            try
            {
                var result = await _tourShipperService.GetTourByTourIdServiceAsync(id);
                if (result != null)
                {

                    return StatusCode(200, result);
                }
                else
                {
                    return StatusCode(404, ResponseCodeConstantsTourShipper.TOURSHIPPER + "_" + ResponseCodeConstants.NOT_FOUND);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, ErrorCode.SERVER_ERROR);
            }
        }
        [HttpGet("GetTourShipperByIdIncludeOrders")]
        [Authorize]
        public async Task<IActionResult> GetTourShipperByIdIncludeOrders(Guid id)
        {
            try
            {
                var result = await _tourShipperService.GetTourByTourIdIncludeOrderServiceAsync(id);
                if (result != null)
                {

                    return StatusCode(200, result);
                }
                else
                {
                    return StatusCode(404, ResponseCodeConstantsTourShipper.TOURSHIPPER + "_" + ResponseCodeConstants.NOT_FOUND);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, ErrorCode.SERVER_ERROR);
            }
        }
        /*
        [HttpGet("GetListTourShipperByStoreIdAndStatus")]
        [Authorize]
        public async Task<IActionResult> GetTourShippersByStoreIdAndStatus(Guid tourId, string status)
        {
            try
            {
                var result = await _tourShipperService.GetByTourIdAndStatusServiceAsync(tourId, status);
                if (result?.Count == 0)
                {
                    return StatusCode(200, ResponseCodeConstants.EMPTY);
                }

                return StatusCode(200, result);
            }
            catch (Exception e)
            {
                return StatusCode(500, ErrorCode.SERVER_ERROR);
            }
        }*/
        /*[HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> UpdatetourShipper(UpdateTourShipperRequestModel updatetourShipperRequest)
        {
            try
            {
                if (await _tourShipperService.UpdateTourServiceAsync(updatetourShipperRequest) != Guid.Empty)
                {
                    return StatusCode(200, ResponseCodeConstantsTourShipper.UPDATE_TOURSHIPPER_SUCCESSFULLY);
                }
                else
                {
                    return StatusCode(404, ResponseCodeConstantsTourShipper.TOURSHIPPER + "_" + ResponseCodeConstants.NOT_FOUND);
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
        }*/
        [HttpPut("ChangeTourStatusAcceptForShipper")]
        [Authorize]
        public async Task<IActionResult> ChangeTourStatusAcceptForShipper(UpdateTourShipperRequestModel model)
        {
            try
            {
                if (await _tourShipperService.ChangeTourStatusAcceptServiceAsync(model) != Guid.Empty)
                {
                    return StatusCode(200, ResponseCodeConstantsTourShipper.UPDATE_TOURSHIPPER_SUCCESSFULLY);
                }
                else
                {
                    return StatusCode(404, ResponseCodeConstantsTourShipper.TOURSHIPPER + "_" + ResponseCodeConstants.NOT_FOUND);
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
        [HttpPut("ChangeTourStatusDoneForShipper")]
        [Authorize]
        public async Task<IActionResult> ChangeTourStatusDoneForShipper(UpdateTourShipperRequestModel model)
        {
            try
            {
                if (await _tourShipperService.ChangeTourStatusDoneServiceAsync(model) != Guid.Empty)
                {
                    return StatusCode(200, ResponseCodeConstantsTourShipper.UPDATE_TOURSHIPPER_SUCCESSFULLY);
                }
                else
                {
                    return StatusCode(404, ResponseCodeConstantsTourShipper.TOURSHIPPER + "_" + ResponseCodeConstants.NOT_FOUND);
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
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> DeletetourShipper(Guid id)
        {
            try
            {
                if (await _tourShipperService.DeleteTourServiceAsync(id) != Guid.Empty)
                {
                    return StatusCode(200, ResponseCodeConstantsTourShipper.DELETE_TOURSHIPPER_SUCCESSFULLY);
                }
                else
                {
                    return StatusCode(404, ResponseCodeConstantsTourShipper.TOURSHIPPER + "_" + ResponseCodeConstants.NOT_FOUND);
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
