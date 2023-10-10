using TeamSolution.Model;

namespace TeamSolution.Service.Interface
{
    public interface IOrderDetailService
    {
        Task<OrderDetail> GetOrderDetailById(int id);
    }
}
