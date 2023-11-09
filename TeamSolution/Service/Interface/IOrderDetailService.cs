using TeamSolution.Model;
using TeamSolution.ViewModel.OrderDetail;

namespace TeamSolution.Service.Interface
{
    public interface IOrderDetailService
    {
        Task<bool> CreateOrderDetailServiceAsync(OrderDetail orderDetail);
        Task<bool> UpdateOrderDetailStoreServiceIdAsync(Guid orderDetailId, Guid newStoreServiceId);
        Task<ICollection<GetOrderDetailByOrderIdReqDto>> GetOrderDetailByOrderIdServiceAsync(Guid orderId);
    }
}
