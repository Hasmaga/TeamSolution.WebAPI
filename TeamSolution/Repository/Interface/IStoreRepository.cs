using TeamSolution.Model;
using TeamSolution.ViewModel.Store;

namespace TeamSolution.Repository.Interface
{
    public interface IStoreRepository
    {
        Task<Store> GetStoreByIdRepositoryAsync(Guid id);
        Task<ICollection<Store>> GetAll(bool includeIsDeleted = false);
        Task<Guid> CreateAsync(Store store, CancellationToken cancellationToken = default);
        Task<Guid> UpdateAsync(Store store, CancellationToken cancellationToken = default);
        Task<Guid> DeleteAsync(Store store, CancellationToken cancellationToken = default);
        Task<ICollection<Store>> GetFilterStoreByStoreNameRepositoryAsync(string storeName);
    }
}
