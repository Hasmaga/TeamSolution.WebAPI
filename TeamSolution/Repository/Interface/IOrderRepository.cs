using TeamSolution.Model;

namespace TeamSolution.Repository.Interface
{
    public interface IOrderRepository
    {
        Task<bool> CreateOrderRepositoryAsync(Order order);
        Task<Order> GetOrderByIdRepositoryAsync(Guid id);
        Task<Order> GetOrderByCustomerIdRepositoryAsync(Guid id);
        Task<bool> UpdateOrderRepositoryAsync(Order order);
        Task<List<Order>> GetAllOrdersRepositoryAsync();
    }
}
