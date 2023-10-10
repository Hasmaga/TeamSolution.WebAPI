using TeamSolution.Service.Interface;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
    }
}
