using TeamSolution.Model;
using TeamSolution.ViewModel.Order;
using TeamSolution.ViewModel.Store;

namespace TeamSolution.Service.Interface
{
    public interface IOrderService
    {
        Task<bool> CreateOrderServiceAsync(CreateNewOrderReqDto order);
        Task<Order> GetOrderByIdServiceAsync(Guid id);
        Task<ICollection<Order>> GetAllOrderServiceAsync();
        Task<ICollection<Order>> GetByCustomerIdServiceAsync(Guid id);
        Task<ICollection<Order>> GetByStoreIdServiceAsync(Guid id);
        Task<ICollection<Order>?> GetByStoreIdAndStatusServiceAsync(Guid storeId, string orderState);
        Task<Guid> UpdateOrderStatusForStoreServiceAsync(Guid orderId, string newState);
        Task<Guid> UpdateOrderStatusForShipperServiceAsync(Guid orderId, string newState);
        Task<Guid> UpdateOrderStatusForCustomerServiceAsync(Guid orderId, string newState);
        Task<Guid> UpdateOrderServiceAsync(UpdateOrderRequestModel request);
        Task<Guid> DeleteOrderServiceAsync(Guid id);
        Task<ICollection<StoreOrderViewDto>> GetListOrderByHttpsClientServiceAsync(string statusOrder);
    }
}
