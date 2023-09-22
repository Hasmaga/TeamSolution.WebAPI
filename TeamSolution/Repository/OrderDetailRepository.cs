using TeamSolution.DAO.Interface;
using TeamSolution.Model;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly IOrderDetailDAO _orderDetailDAO;
        public OrderDetailRepository(IOrderDetailDAO orderDetailDAO)
        {
            _orderDetailDAO = orderDetailDAO;
        }

        public async Task<OrderDetail> GetOrderDetailById(int id)
        {
            throw new NotImplementedException();
        }

        Task<OrderDetail> IOrderDetailRepository.GetOrderDetailById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
