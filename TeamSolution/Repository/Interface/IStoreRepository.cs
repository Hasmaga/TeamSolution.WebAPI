using TeamSolution.Model;

namespace TeamSolution.Repository.Interface
{
    public interface IStoreRepository
    {
        Task<Store> GetStoreByIdRepositoryAsync(Guid id);
        Task<ICollection<Store>> GetAll(bool includeIsDeleted = false);
        Task<Guid> CreateAsync(Store store, CancellationToken cancellationToken = default);
    }
}
