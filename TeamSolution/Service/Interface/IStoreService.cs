using TeamSolution.Model;
using TeamSolution.ViewModel.Store;

namespace TeamSolution.Service.Interface 
{
    public interface IStoreService
    {
        Task<Store> GetAllStore();
        Task<ICollection<Store>> GetAllStoreEntity();
        Task<Guid> CreateStoreAsync(StoreModel store);
    }
}
