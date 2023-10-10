using Microsoft.AspNetCore.Mvc;
using TeamSolution.Service.Interface;

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
    }
}
