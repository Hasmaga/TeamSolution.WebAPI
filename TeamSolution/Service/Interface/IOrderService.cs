using TeamSolution.Model;
using TeamSolution.ViewModel.Order;

namespace TeamSolution.Service.Interface
{
    public interface IOrderService
    {
        Task<bool> CreateOrderServiceAsync(CreateNewOrderReqDto order);
        Task<bool> UpdateOrderServiceAsync(Guid id, CreateNewOrderReqDto order);
        Task<List<Order>> GetAllOrderInProcess();
        Task<bool> CancelOrder(Guid OrderId, Guid CustomerId);
        Task<bool> RecievedOrder(Guid OrderId, Guid CustomerId);
    }
}
