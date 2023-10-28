using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamSolution.Enum;
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
            catch (Exception)
            {
                return StatusCode(500, ErrorCode.SERVER_ERROR);
            }
        }
    }
}
