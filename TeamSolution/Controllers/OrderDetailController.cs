using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamSolution.Enum;
using TeamSolution.Service.Interface;

namespace TeamSolution.Controllers
{
    [Route("orderdetailrapi")]
    [ApiController]
    public class OrderDetailController : Controller
    {
        private readonly IOrderDetailService _orderDetailService;
        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpPut("updateWashingMode")]
        [Authorize]
        public async Task<IActionResult> UpdateStoreServiceId(Guid orderDetailId, Guid newStoreServiceId)
        {
            try
            {
                if (await _orderDetailService.UpdateOrderDetailStoreServiceIdAsync(orderDetailId, newStoreServiceId))
                {
                    return Ok("Cập nhật StoreServiceId cho OrderDetail thành công.");
                }
                else
                {
                    return BadRequest("Không thể cập nhật StoreServiceId cho OrderDetail.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, ErrorCode.SERVER_ERROR);
            }
        }
    }
}
