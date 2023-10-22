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
    }
}
