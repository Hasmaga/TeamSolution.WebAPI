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

                var result = await _orderService.GetAllOrderInProcess();
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
        public async Task<IActionResult> UpdateOrder(Guid id, CreateNewOrderReqDto order)
        {
            var result = await _orderService.UpdateOrderServiceAsync(id, order);
            if (result)
            {
                return StatusCode(200, SucessfulCode.UPDATE_ORDER_SUCCESSFULLY);
            }
            else
            {
                return StatusCode(500, ErrorCode.UPDATE_ORDER_FAIL);
            }
        }

        [HttpPut("CancelOrder")]
        [Authorize]
        public async Task<IActionResult> CancelOrder(Guid OrderId, Guid CustomerId)
        {
            var result = await _orderService.CancelOrder(OrderId, CustomerId);
            if (result)
            {
                return StatusCode(200, SucessfulCode.UPDATE_ORDER_SUCCESSFULLY);
            }
            else
            {
                return StatusCode(500, ErrorCode.UPDATE_ORDER_FAIL);
            }
        }

        [HttpPut("RecievedOrder")]
        [Authorize]
        public async Task<IActionResult> RecievedOrder(Guid OrderId, Guid CustomerId)
        {
            var result = await _orderService.RecievedOrder(OrderId, CustomerId);
            if (result)
            {
                return StatusCode(200, SucessfulCode.UPDATE_ORDER_SUCCESSFULLY);
            }
            else
            {
                return StatusCode(500, ErrorCode.UPDATE_ORDER_FAIL);
            }
        }

    }
}
