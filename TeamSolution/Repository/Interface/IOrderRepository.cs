using TeamSolution.Model;
using TeamSolution.ViewModel.Order;

namespace TeamSolution.Repository.Interface
{
    public interface IOrderRepository
    {
        Task<bool> CreateOrderRepositoryAsync(Order order);
        Task<Order> GetOrderByIdRepositoryAsync(Guid id);
        Task<ICollection<Order>> GetAllOrdersRepositoryAsync(bool includeIsDeleted = false);
        Task<ICollection<Order>> GetOrdersByCustomerIdRepositoryAsync(Guid id, bool includeIsDeleted = false);
        Task<ICollection<Order>> GetOrdersByStoreIdRepositoryAsync(Guid id, bool includeIsDeleted = false);
        Task<ICollection<Order>> GetOrdersByStoreIdAndStatusIdRepositoryAsync(Guid storeId, Guid statusId, bool includeIsDeleted = false);
        Task<Guid> UpdateOrderStateRepositoryAsync(Order order, CancellationToken cancellationToken = default);
        Task<Guid> UpdateOrderRepositoryAsync(Order order, CancellationToken cancellationToken = default);
        Task<Guid> DeleteOrderRepositoryAsync(Order order, CancellationToken cancellationToken = default);
        
        Task<Order?> GetOrderByOrderIdRepositoryAsync(Guid orderId);
    }
}
