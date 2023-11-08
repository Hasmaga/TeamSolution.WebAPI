using TeamSolution.Model;
using TeamSolution.ViewModel.StoreService;

namespace TeamSolution.Service.Interface
{
    public interface IStoreServiceService
    {
        Task<List<GetStoreServiceReqDto>> GetStoreServiceByStoreIdServiceAsync(Guid id);
    }
}
