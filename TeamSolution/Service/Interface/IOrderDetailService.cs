using TeamSolution.Model;

namespace TeamSolution.Service.Interface
{
    public interface IOrderDetailService
    {
        Task<bool> CreateOrderDetailServiceAsync(OrderDetail orderDetail);
        Task<bool> UpdateOrderDetailStoreServiceIdAsync(Guid orderDetailId, Guid newStoreServiceId);
    }
}
