using TeamSolution.Model;
using TeamSolution.ViewModel.Order;

namespace TeamSolution.Repository.Interface
{
    public interface IOrderRepository
    {
        Task<bool> CreateOrderRepositoryAsync(Order order);
        Task<Order> GetByIdAsync(Guid id);
        Task<ICollection<Order>> GetAllAsync(bool includeIsDeleted = false);
        Task<Guid> UpdateAsync(UpdateOrderRequestModel request, CancellationToken cancellationToken = default);
        Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
