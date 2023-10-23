using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamSolution.Enum;
using TeamSolution.Model;
using TeamSolution.Service.Interface;
using TeamSolution.ViewModel.Order;
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
        [HttpPost("create")]
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
            catch (Exception e)
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
                var result = await _orderService.GetAllOrder();
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
        [HttpGet("GetById")]
        [Authorize]
        public async Task<IActionResult> GetSigleOrderById(Guid id)
        {
            try
            {
                var result = await _orderService.GetOrderById(id);
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
        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> UpdateOrder(UpdateOrderRequestModel updateOrderRequest)
        {
            try
            {
                if (await _orderService.UpdateOrderAsync(updateOrderRequest) != Guid.Empty)
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
                if (await _orderService.DeleteOrderAsync(id) != Guid.Empty)
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
