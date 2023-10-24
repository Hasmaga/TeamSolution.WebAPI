using TeamSolution.Model;
using TeamSolution.ViewModel.Order;

namespace TeamSolution.Repository.Interface
{
    public interface IOrderRepository
    {
        Task<bool> CreateOrderRepositoryAsync(Order order);
        Task<Order> GetByIdRepositoryAsync(Guid id);
        Task<ICollection<Order>> GetAllRepositoryAsync(bool includeIsDeleted = false);
        Task<ICollection<Order>> GetByCustomerIdRepositoryAsync(Guid id, bool includeIsDeleted = false);
        Task<ICollection<Order>> GetByStoreIdRepositoryAsync(Guid id, bool includeIsDeleted = false);
        Task<Guid> UpdateRepositoryAsync(UpdateOrderRequestModel request, CancellationToken cancellationToken = default);
        Task<Guid> DeleteRepositoryAsync(Guid id, CancellationToken cancellationToken = default);
        
    }
}
