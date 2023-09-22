using Microsoft.AspNetCore.Mvc;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Controllers
{
    [Route("orderdetailrapi")]
    [ApiController]
    public class OrderDetailController : Controller
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public OrderDetailController(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }
    }
}
