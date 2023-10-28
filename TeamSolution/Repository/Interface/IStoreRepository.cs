using TeamSolution.Model;

namespace TeamSolution.Repository.Interface
{
    public interface IStoreRepository
    {
        Task<Store> GetStoreByIdRepositoryAsync(Guid id);
        Task<bool> CreateStoreRepositoryAsync(Store store);
    }
}
