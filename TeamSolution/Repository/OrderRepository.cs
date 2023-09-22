using TeamSolution.DAO.Interface;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IOrderDAO _orderDAO;
        public OrderRepository(IOrderDAO orderDAO)
        {
            _orderDAO = orderDAO;
        }
    }
}
