using TeamSolution.Model;

namespace TeamSolution.Repository.Interface
{
    public interface IOrderDetailRepository
    {
        public Task<bool> CreateOrderDetailRepositoryAsync(OrderDetail orderDetail);
    }
}
