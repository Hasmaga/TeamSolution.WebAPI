using TeamSolution.Service.Interface;
using TeamSolution.Model;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Service
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public Task<bool> CreateOrderDetailServiceAsync(OrderDetail orderDetail)
        {
            throw new NotImplementedException();
        }
    }
}
