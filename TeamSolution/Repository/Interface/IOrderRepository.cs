using TeamSolution.Model;

namespace TeamSolution.Repository.Interface
{
    public interface IOrderRepository
    {
        Task<bool> CreateOrderRepositoryAsync(Order order);
        Task<Order?> GetOrderByOrderIdRepositoryAsync(Guid orderId);
    }
}
