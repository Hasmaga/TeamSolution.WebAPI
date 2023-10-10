using Microsoft.AspNetCore.Mvc;
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
    }
}
