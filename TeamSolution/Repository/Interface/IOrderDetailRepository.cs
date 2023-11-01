using TeamSolution.Model;

namespace TeamSolution.Repository.Interface
{
    public interface IOrderDetailRepository
    {
        public Task<bool> CreateOrderDetailRepositoryAsync(OrderDetail orderDetail);
        Task<OrderDetail> GetOrderDetailByIdAsync(Guid orderDetailId);
        Task UpdateOrderDetailAsync(OrderDetail orderDetail);

        Task<Guid?> GetStatusOrderIdByOrderDetailId(Guid orderDetailId);
    }
}
