using TeamSolution.ViewModel.Order;

namespace TeamSolution.Service.Interface
{
    public interface IOrderService
    {
        Task<bool> CreateOrderServiceAsync(CreateNewOrderReqDto order);
    }
}
