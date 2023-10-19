using TeamSolution.Model;

namespace TeamSolution.Repository.Interface
{
    public interface IStoreServiceRepository
    {
        Task<StoreService> GetStoreServiceByIdRepositoryAsync(Guid id);
    }
}
