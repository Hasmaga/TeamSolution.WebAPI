using TeamSolution.Model;

namespace TeamSolution.Service.Interface
{
    public interface IOrderDetailService
    {
        Task<bool> CreateOrderDetailServiceAsync(OrderDetail orderDetail);
    }
}
