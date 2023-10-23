using TeamSolution.Model;
using TeamSolution.ViewModel.Order;
using TeamSolution.ViewModel.Store;

namespace TeamSolution.Service.Interface
{
    public interface IOrderService
    {
        Task<bool> CreateOrderServiceAsync(CreateNewOrderReqDto order);
        Task<Order> GetOrderById(Guid id);
        Task<ICollection<Order>> GetAllOrder();
        Task<Guid> UpdateOrderAsync(UpdateOrderRequestModel request);
        Task<Guid> DeleteOrderAsync(Guid id);
    }
}
