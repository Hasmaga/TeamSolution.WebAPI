using TeamSolution.Model;

namespace TeamSolution.Repository.Interface
{
    public interface IOrderDetailRepository
    {
        Task<OrderDetail> GetOrderDetailById(int id);
    }
}
