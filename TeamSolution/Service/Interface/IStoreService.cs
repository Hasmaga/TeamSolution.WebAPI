using TeamSolution.Model;
using TeamSolution.ViewModel.Store;

namespace TeamSolution.Service.Interface 
{
    public interface IStoreService
    {
        Task<Store> GetStoreById(Guid id);
        Task<ICollection<Store>> GetAllStore();
        Task<Guid> CreateStoreAsync(StoreModel store);
        Task<Guid> UpdateStoreAsync(UpdateStoreRequestModel updateStoreRequest);
        Task<Guid> DeleteStoreAsync(Guid id);
        Task<ICollection<Store>> GetFilterStoreByStoreNameServiceAsync(string storeName);
        Task<GetStoreAndStoreServiceReqDto> GetStoreInformationAndStoreServiceByStoreIdServiceAsync(Guid id);
    }
}
