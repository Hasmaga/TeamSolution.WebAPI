using TeamSolution.Model;

namespace TeamSolution.Repository.Interface
{
    public interface IStoreServiceRepository
    {
        Task<StoreService> GetStoreServiceByIdRepositoryAsync(Guid id);
        Task<List<StoreService>> GetStoreServiceByStoreIdRepositoryAsync(Guid id);
        Task<bool> CreateStoreServiceRepository(StoreService storeService);
    }
}
