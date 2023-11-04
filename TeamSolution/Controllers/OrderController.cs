using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamSolution.Enum;
using TeamSolution.Model;
using TeamSolution.Service.Interface;
using TeamSolution.ViewModel.Order;

namespace TeamSolution.Controllers
{
    [Route("orderapi")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // post: orderapi/create
        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> CreateOrder(CreateNewOrderReqDto order)
        {
            try
            {
                if (await _orderService.CreateOrderServiceAsync(order))
                {
                    return StatusCode(200, SucessfulCode.CREATE_ORDER_SUCCESSFULLY);
                }
                else
                {
                    return StatusCode(500, ErrorCode.CREATE_ORDER_FAIL);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorCode.SERVER_ERROR);
            }
        }
        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAllOrder()
        {
            try
            {
                var result = await _orderService.GetAllOrderServiceAsync();
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
        [HttpGet("GetOrderById")]
        [Authorize]
        public async Task<IActionResult> GetSingleOrderById(Guid id)
        {
            try
            {
                var result = await _orderService.GetOrderByIdServiceAsync(id);
                if (result != null)
                {

                    return StatusCode(200, result);
                }
                else
                {
                    return StatusCode(404, ResponseCodeConstantsOrder.ORDER + "_" + ResponseCodeConstants.NOT_FOUND);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, ErrorCode.SERVER_ERROR);
            }
        }
        [HttpGet("GetListOrderByCustomerId")]
        [Authorize]
        public async Task<IActionResult> GetOrdersByCustomerId(Guid id)
        {
            try
            {
                var result = await _orderService.GetByCustomerIdServiceAsync(id);
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
        [HttpGet("GetListOrderByStoreId")]
        [Authorize]
        public async Task<IActionResult> GetOrdersByStoreId(Guid id)
        {
            try
            {
                var result = await _orderService.GetByStoreIdServiceAsync(id);
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
        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> UpdateOrder(UpdateOrderRequestModel updateOrderRequest)
        {
            try
            {
                if (await _orderService.UpdateOrderServiceAsync(updateOrderRequest) != Guid.Empty)
                {
                    return StatusCode(200, ResponseCodeConstantsOrder.UPDATE_ORDER_SUCCESSFULLY);
                }
                else
                {
                    return StatusCode(404, ResponseCodeConstantsOrder.ORDER + "_" + ResponseCodeConstants.NOT_FOUND);
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
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            try
            {
                if (await _orderService.DeleteOrderServiceAsync(id) != Guid.Empty)
                {
                    return StatusCode(200, ResponseCodeConstantsOrder.DELETE_ORDER_SUCCESSFULLY);
                }
                else
                {
                    return StatusCode(404, ResponseCodeConstantsOrder.ORDER + "_" + ResponseCodeConstants.NOT_FOUND);
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
