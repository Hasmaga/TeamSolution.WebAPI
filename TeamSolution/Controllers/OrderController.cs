using Microsoft.AspNetCore.Mvc;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Controllers
{
    [Route("orderapi")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
    }
}
