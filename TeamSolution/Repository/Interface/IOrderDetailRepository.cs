using TeamSolution.Model;

namespace TeamSolution.Repository.Interface
{
    public interface IOrderDetailRepository
    {
        Task<bool> CreateOrderDetailRepositoryAsync(OrderDetail orderDetail);               
        Task<OrderDetail?> GetOrderDetailByOrderDetailIdRepositoryAsync(Guid orderDetailId);
        Task<bool> UpdateOrderDetailByOrderServiceIdRepositoryAsync(Guid orderDetailId, Guid newStoreServiceId);
    }
}
