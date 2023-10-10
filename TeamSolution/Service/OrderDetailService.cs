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

        public async Task<OrderDetail> GetOrderDetailById(int id)
        {
            throw new NotImplementedException();
        }

        Task<OrderDetail> IOrderDetailService.GetOrderDetailById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
