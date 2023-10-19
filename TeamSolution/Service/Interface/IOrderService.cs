using TeamSolution.Model;
using TeamSolution.Model.Dto;

namespace TeamSolution.Service.Interface
{
    public interface IOrderService
    {
        Task<bool> CreateOrderServiceAsync(CreateNewOrderReqDto order);
    }
}
